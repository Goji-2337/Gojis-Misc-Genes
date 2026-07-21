using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Skyfaller), "Impact")]
    public static class Skyfaller_Impact_Patch
    {
        public static void Prefix(Skyfaller __instance)
        {
            foreach (var item in __instance.innerContainer)
            {
                if (item is Pawn pawn && pawn.HasActiveGene(DefsOf.Goji_MotionSickness))
                {
                    pawn.health.AddHediff(DefsOf.Goji_MotionSicknessHediff);
                }
            }
        }
    }
}
