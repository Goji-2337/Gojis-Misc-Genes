using HarmonyLib;
using RimWorld;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn_InteractionsTracker), nameof(Pawn_InteractionsTracker.CheckSocialFightStart))]
    public static class Pawn_InteractionsTracker_CheckSocialFightStart_Patch
    {
        public static bool Prefix(Pawn_InteractionsTracker __instance, ref bool __result)
        {
            if (__instance.pawn.HasActiveGene(DefsOf.Goji_PassiveAggressive))
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
