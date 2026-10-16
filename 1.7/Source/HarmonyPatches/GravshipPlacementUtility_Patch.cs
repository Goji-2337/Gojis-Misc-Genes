using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(GravshipPlacementUtility), nameof(GravshipPlacementUtility.PlaceGravshipInMap))]
    public static class GravshipPlacementUtility_PlaceGravshipInMap_Patch
    {
        public static bool Prepare() => ModsConfig.OdysseyActive;

        public static void Postfix(Gravship gravship)
        {
            foreach (var pawn in gravship.PawnPlacements.Keys)
            {
                if (pawn.HasActiveGene(DefsOf.Goji_MotionSickness))
                {
                    pawn.health.AddHediff(DefsOf.Goji_MotionSicknessHediff);
                }
            }
        }
    }
}
