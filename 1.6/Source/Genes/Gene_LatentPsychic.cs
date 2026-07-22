using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    public class Gene_LatentPsychic : Gene
    {
        public override void PostAdd()
        {
            base.PostAdd();
            var existingTrait = pawn.story.traits.GetTrait(DefsOf.PsychicSensitivity);
            if (existingTrait == null || existingTrait.sourceGene != null)
            {
                if (existingTrait != null)
                {
                    pawn.story.traits.RemoveTrait(existingTrait);
                }
                var degree = Rand.Value < 0.666f ? 1 : 2;
                var trait = new Trait(DefsOf.PsychicSensitivity, degree) { sourceGene = this };
                pawn.story.traits.GainTrait(trait, suppressConflicts: false);
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            var trait = pawn.story.traits.allTraits.Find(t => t.sourceGene == this);
            if (trait != null)
            {
                pawn.story.traits.RemoveTrait(trait);
            }
        }
    }
}
