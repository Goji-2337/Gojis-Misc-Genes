using HarmonyLib;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(HediffComp_Disappears), "CompShouldRemove", MethodType.Getter)]
    public static class HediffComp_Disappears_CompShouldRemove_Patch
    {
        public static void Postfix(HediffComp_Disappears __instance, ref bool __result)
        {
            if (__result && (__instance.parent.def == DefsOf.FibrousMechanites || __instance.parent.def == DefsOf.SensoryMechanites))
            {
                if (__instance.Pawn.HasActiveGene(DefsOf.Goji_PathogenHost))
                {
                    __result = false;
                }
            }
        }
    }

    [HarmonyPatch(typeof(HediffComp_TendDuration), nameof(HediffComp_TendDuration.CompTended))]
    public static class HediffComp_TendDuration_CompTended_Patch
    {
        public static void Prefix(HediffComp_TendDuration __instance, ref float quality, ref float maxQuality)
        {
            if (__instance.parent.def == DefsOf.GutWorms || __instance.parent.def == DefsOf.MuscleParasites)
            {
                if (__instance.Pawn.HasActiveGene(DefsOf.Goji_PathogenHost))
                {
                    quality = 0f;
                    maxQuality = 0f;
                }
            }
        }
    }
}
