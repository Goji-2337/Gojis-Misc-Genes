using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    public class ThoughtWorker_SweetScent : ThoughtWorker
    {
        public override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn other)
        {
            if (p.RaceProps.Humanlike && other.RaceProps.Humanlike && other.HasActiveGene(DefsOf.Goji_Pollinator))
            {
                return ThoughtState.ActiveAtStage(0);
            }
            return ThoughtState.Inactive;
        }
    }
}
