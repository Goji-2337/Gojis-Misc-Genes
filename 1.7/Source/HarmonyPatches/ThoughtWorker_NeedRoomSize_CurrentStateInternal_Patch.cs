using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(ThoughtWorker_NeedRoomSize), "CurrentStateInternal")]
    public static class ThoughtWorker_NeedRoomSize_CurrentStateInternal_Patch
    {
        public static void Postfix(Pawn p, ref ThoughtState __result)
        {
            if (__result.Active && p.HasActiveGene(DefsOf.Goji_Hoarder)) __result = ThoughtState.Inactive;
        }
    }
}
