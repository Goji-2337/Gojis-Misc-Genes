using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(CompAbility_RequiresCapacity), nameof(CompAbility_RequiresCapacity.GizmoDisabled))]
    public static class CompAbility_RequiresCapacity_GizmoDisabled_Patch
    {
        public static bool Prepare() => ModsConfig.IsActive("vanillaracesexpanded.fungoid") && GojisMiscGenesMod.settings.disableFungoidPatch is false;

        public static void Postfix(CompAbility_RequiresCapacity __instance, ref bool __result)
        {
            if (__result && __instance.Props.capacity == PawnCapacityDefOf.Talking && __instance.parent.pawn.HasActiveGene(DefsOf.VRE_Telepathy))
            {
                __result = false;
            }
        }
    }
}
