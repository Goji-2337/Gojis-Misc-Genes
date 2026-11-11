using System.Linq;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    public class Gene_Imprint : Gene
    {
        public Pawn imprintedParent;
        public bool traitCopied;
        private TraitDef copiedTraitDef;
        private int copiedTraitDegree;

        private const int AgeThreshold = 3;
        private const int CheckIntervalTicks = 2500;

        public override void PostAdd()
        {
            base.PostAdd();
            if (pawn.DevelopmentalStage.Baby())
            {
                var directRelations = pawn.relations.DirectRelations;
                for (int i = directRelations.Count - 1; i >= 0; i--)
                {
                    var relation = directRelations[i];
                    if (relation.def == PawnRelationDefOf.Parent || relation.def == PawnRelationDefOf.ParentBirth)
                    {
                        pawn.relations.RemoveDirectRelation(relation);
                    }
                }
            }
            else
            {
                traitCopied = true;
            }
        }

        public override void Tick()
        {
            base.Tick();
            if (traitCopied is false && imprintedParent is not null && pawn.IsHashIntervalTick(CheckIntervalTicks) && pawn.ageTracker.AgeBiologicalYears >= AgeThreshold)
            {
                TryCopyParentTrait();
            }
        }

        public void Notify_FedBy(Pawn feeder)
        {
            if (imprintedParent is not null || !pawn.DevelopmentalStage.Baby())
            {
                return;
            }
            imprintedParent = feeder;
            var directRelations = pawn.relations.DirectRelations;
            for (int i = directRelations.Count - 1; i >= 0; i--)
            {
                var relation = directRelations[i];
                if (relation.def == PawnRelationDefOf.Parent || relation.def == PawnRelationDefOf.ParentBirth)
                {
                    pawn.relations.RemoveDirectRelation(relation);
                }
            }
            if (feeder.gender == Gender.Male)
            {
                pawn.SetFather(feeder);
            }
            else if (feeder.gender == Gender.Female)
            {
                pawn.SetMother(feeder);
            }
            else
            {
                pawn.relations.AddDirectRelation(PawnRelationDefOf.Parent, feeder);
            }
            if (PawnUtility.ShouldSendNotificationAbout(pawn))
            {
                Messages.Message("Goji_MessageImprintedOn".Translate(pawn.Named("BABY"), feeder.Named("FEEDER")), pawn, MessageTypeDefOf.PositiveEvent);
            }
        }

        public void TryCopyParentTrait()
        {
            if (imprintedParent is null)
            {
                return;
            }
            traitCopied = true;
            var eligibleTraits = imprintedParent.story.traits.allTraits.Where(t => t.Suppressed is false && t.sourceGene is null && !pawn.story.traits.HasTrait(t.def)).ToList();
            if (eligibleTraits.TryRandomElement(out Trait chosenTrait))
            {
                copiedTraitDef = chosenTrait.def;
                copiedTraitDegree = chosenTrait.Degree;
                GainCopiedTrait();
                if (PawnUtility.ShouldSendNotificationAbout(pawn))
                {
                    TaggedString traitLabel = copiedTraitDef.DataAtDegree(copiedTraitDegree).label;
                    Messages.Message("Goji_MessageImprintedTraitCopied".Translate(pawn.Named("CHILD"), imprintedParent.Named("PARENT"), traitLabel.Named("TRAIT")), pawn, MessageTypeDefOf.PositiveEvent);
                }
            }
        }

        public static void CheckImprint(Pawn baby, Pawn feeder)
        {
            if (baby.HasActiveGene(DefsOf.Goji_Imprint, out var gene) && gene is Gene_Imprint imprint)
            {
                imprint.Notify_FedBy(feeder);
            }
        }

        private void GainCopiedTrait()
        {
            var trait = new Trait(copiedTraitDef, copiedTraitDegree)
            {
                sourceGene = this
            };
            pawn.story.traits.GainTrait(trait, suppressConflicts: true);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look(ref imprintedParent, "imprintedParent", saveDestroyedThings: true);
            Scribe_Values.Look(ref traitCopied, "traitCopied");
            Scribe_Defs.Look(ref copiedTraitDef, "copiedTraitDef");
            Scribe_Values.Look(ref copiedTraitDegree, "copiedTraitDegree");
            if (Scribe.mode == LoadSaveMode.PostLoadInit && traitCopied && copiedTraitDef is not null && !pawn.story.traits.HasTrait(copiedTraitDef, copiedTraitDegree))
            {
                GainCopiedTrait();
            }
        }
    }
}
