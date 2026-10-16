using HarmonyLib;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(HediffComp_TendDuration), nameof(HediffComp_TendDuration.CompTended))]
    public static class HediffComp_TendDuration_CompTended_Patch
    {
        public static void Prefix(HediffComp_TendDuration __instance, ref float quality, ref float maxQuality)
        {
            if ((__instance.parent.def == DefsOf.GutWorms || __instance.parent.def == DefsOf.MuscleParasites) && __instance.Pawn.HasActiveGene(DefsOf.Goji_PathogenHost))
            {
                quality = 0f;
                maxQuality = 0f;
            }
            if (maxQuality > 0f && __instance.Pawn.HasActiveGene(DefsOf.Goji_PlaceboEffect))
            {
                quality = maxQuality + HediffComp_TendDuration.TendQualityRandomVariance;
            }
        }
    }
}
