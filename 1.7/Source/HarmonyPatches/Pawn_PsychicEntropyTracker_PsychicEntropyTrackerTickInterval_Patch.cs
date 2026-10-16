using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn_PsychicEntropyTracker), nameof(Pawn_PsychicEntropyTracker.PsychicEntropyTrackerTickInterval))]
    public static class Pawn_PsychicEntropyTracker_PsychicEntropyTrackerTickInterval_Patch
    {
        public static bool Prepare() => ModsConfig.RoyaltyActive;
        private const float EntropyToPsyfocusRatio = 0.001f;

        public static void Prefix(Pawn_PsychicEntropyTracker __instance, ref float __state)
        {
            __state = __instance.EntropyValue;
        }

        public static void Postfix(Pawn_PsychicEntropyTracker __instance, float __state)
        {
            var dissipated = __state - __instance.EntropyValue;
            if (__instance.Pawn.HasActiveGene(DefsOf.Goji_FocusLoop) && __state > 0f && dissipated > 0f)
            {
                __instance.OffsetPsyfocusDirectly(dissipated * EntropyToPsyfocusRatio);
            }
        }
    }
}
