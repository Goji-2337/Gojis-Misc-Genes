using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    public class ThoughtWorker_HoarderRoomSize : ThoughtWorker
    {
        public override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (!p.HasActiveGene(DefsOf.Goji_Hoarder)) return ThoughtState.Inactive;
            var curCategory = p.needs.roomsize.CurCategory;
            if (curCategory == RoomSizeCategory.VeryCramped) return ThoughtState.ActiveAtStage(0);
            if (curCategory == RoomSizeCategory.Cramped) return ThoughtState.ActiveAtStage(1);
            if (curCategory == RoomSizeCategory.Normal) return ThoughtState.Inactive;
            if (curCategory == RoomSizeCategory.Spacious) return ThoughtState.ActiveAtStage(2);
            return ThoughtState.Inactive;
        }
    }
}
