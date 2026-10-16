using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(ShotReport), "AimOnTargetChance_IgnoringPosture", MethodType.Getter)]
    public static class ShotReport_AimOnTargetChance_IgnoringPosture_Patch
    {
        public static bool Prepare() => !ModsConfig.IsActive("Arquebus.StagzMerfolk");

        private static float? _meleeToRangeCoefficient;

        private static float MeleeToRangeCoefficient => _meleeToRangeCoefficient ??= DefsOf.Stagz_KeenReflexes.HasModExtension<KeenReflexModExtension>() ? DefsOf.Stagz_KeenReflexes.GetModExtension<KeenReflexModExtension>().MeleeToRangeCoefficient : 1f;

        private static void Postfix(ref float __result, ref TargetInfo ___target)
        {
            if (___target == null) return;

            if (___target.Thing is Pawn pawn && pawn.RaceProps.Humanlike && pawn.genes.HasActiveGene(DefsOf.Stagz_KeenReflexes) && __result < 1f)
            {
                __result = Math.Max(__result - (pawn.GetStatValue(StatDefOf.MeleeDodgeChance, true, -1) * MeleeToRangeCoefficient), 0.02f);
            }
        }
    }
}
