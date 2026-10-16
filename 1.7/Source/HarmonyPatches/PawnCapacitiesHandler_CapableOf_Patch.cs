using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(PawnCapacitiesHandler), nameof(PawnCapacitiesHandler.CapableOf))]
    public static class PawnCapacitiesHandler_CapableOf_Patch
    {
        public static bool Prepare() => ModsConfig.IsActive("vanillaracesexpanded.fungoid") && GojisMiscGenesMod.settings.disableFungoidPatch is false;

        public static bool Prefix(PawnCapacityDef capacity, ref bool __result)
        {
            if (SocialInteractionUtility_CanInitiateInteraction_Patch.isTelepathyInteraction > 0 && (capacity == PawnCapacityDefOf.Talking || capacity == PawnCapacityDefOf.Hearing))
            {
                __result = true;
                return false;
            }
            return true;
        }
    }
}
