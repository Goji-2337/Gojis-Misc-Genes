using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(MeditationFocusDef), nameof(MeditationFocusDef.CanPawnUse))]
    public static class MeditationFocusDef_CanPawnUse_Patch
    {
        public static void Postfix(MeditationFocusDef __instance, Pawn p, ref bool __result)
        {
            if (!__result && __instance == DefsOf.Natural && p.HasActiveGene(DefsOf.Goji_NatureRhythm))
            {
                __result = true;
            }
        }
    }
}
