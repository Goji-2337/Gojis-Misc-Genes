using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Recipe_Surgery), nameof(Recipe_Surgery.AvailableOnNow))]
    public static class Recipe_Surgery_AvailableOnNow_Patch
    {
        public static bool Prepare() => ModsConfig.AnomalyActive;

        public static bool Prefix(Recipe_Surgery __instance, Thing thing, ref bool __result)
        {
            var prereqs = __instance.recipe.mutantPrerequisite;
            if (thing is Pawn pawn && pawn.HasActiveGene(DefsOf.Goji_PainFortitude) && pawn.IsMutant is false && prereqs != null && prereqs.Contains(DefsOf.Ghoul))
            {
                if (__instance.recipe.genderPrerequisite.HasValue && pawn.gender != __instance.recipe.genderPrerequisite.Value
                    || __instance.recipe.developmentalStageFilter.HasValue && !__instance.recipe.developmentalStageFilter.Value.Has(pawn.DevelopmentalStage)
                    || __instance.recipe.minAllowedAge > 0 && pawn.ageTracker.AgeBiologicalYears < __instance.recipe.minAllowedAge)
                {
                    return true;
                }
                __result = true;
                return false;
            }
            return true;
        }
    }
}
