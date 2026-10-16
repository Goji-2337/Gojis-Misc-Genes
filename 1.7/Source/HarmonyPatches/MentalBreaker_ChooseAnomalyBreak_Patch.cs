using HarmonyLib;
using Verse;
using Verse.AI;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(MentalBreaker), "ChooseAnomalyBreak", MethodType.Getter)]
    public static class MentalBreaker_ChooseAnomalyBreak_Patch
    {
        public static bool Prepare() => ModsConfig.AnomalyActive;

        public static void Postfix(MentalBreaker __instance, ref bool __result)
        {
            if (__instance.pawn.HasActiveGene(DefsOf.Goji_JinxedBloodline))
            {
                __result = true;
            }
        }
    }
}
