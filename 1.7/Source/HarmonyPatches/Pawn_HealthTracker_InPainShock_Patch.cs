using HarmonyLib;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn_HealthTracker), nameof(Pawn_HealthTracker.InPainShock), MethodType.Getter)]
    public static class Pawn_HealthTracker_InPainShock_Patch
    {
        public static void Postfix(Pawn_HealthTracker __instance, ref bool __result)
        {
            if (__result && __instance.pawn.HasActiveGene(DefsOf.Goji_PainFortitude))
            {
                __result = false;
            }
        }
    }
}
