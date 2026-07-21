using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    public class Gene_LatentPsychic : Gene
    {
        public override void PostAdd()
        {
            base.PostAdd();
            if (!pawn.story.traits.HasTrait(DefsOf.PsychicSensitivity))
            {
                var value = Rand.Value;
                if (value < 0.666f)
                {
                    pawn.story.traits.GainTrait(new Trait(DefsOf.PsychicSensitivity, 1), suppressConflicts: false);
                }
                else
                {
                    pawn.story.traits.GainTrait(new Trait(DefsOf.PsychicSensitivity, 2), suppressConflicts: false);
                }
            }
        }
    }
}
