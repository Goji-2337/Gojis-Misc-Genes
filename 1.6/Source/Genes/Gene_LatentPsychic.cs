using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    public class Gene_LatentPsychic : Gene
    {
        private bool addedTrait;

        public override void PostAdd()
        {
            base.PostAdd();
            if (!pawn.story.traits.HasTrait(DefsOf.PsychicSensitivity))
            {
                var degree = Rand.Value < 0.666f ? 1 : 2;
                pawn.story.traits.GainTrait(new Trait(DefsOf.PsychicSensitivity, degree), suppressConflicts: false);
                addedTrait = true;
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            if (addedTrait)
            {
                var trait = pawn.story.traits.GetTrait(DefsOf.PsychicSensitivity);
                if (trait != null)
                {
                    pawn.story.traits.RemoveTrait(trait);
                }
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref addedTrait, "addedTrait", false);
        }
    }
}
