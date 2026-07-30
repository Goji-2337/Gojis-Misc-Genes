using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes;

[HarmonyPatch(typeof(Pawn), "SpecialDisplayStats")]
public static class Pawn_SpecialDisplayStats_Patch
{
    public static bool Prepare()
    {
        return !ModsConfig.IsActive("Arquebus.StagzMerfolk");
    }

    private static IEnumerable<StatDrawEntry> Postfix(IEnumerable<StatDrawEntry> __result, Pawn __instance)
    {
        if (__instance != null && __instance.RaceProps.Humanlike && __instance.genes.HasActiveGene(DefsOf.Stagz_KeenReflexes))
        {
            var keenReflexesStatDrawEntry = new StatDrawEntry(
                StatCategoryDefOf.PawnCombat,
                "Goji_KeenReflexes".Translate(),
                "Goji_KeenReflexes_Value".Translate(),
                "Goji_KeenReflexes_Description".Translate(),
                410000
            );
            return __result.Concat(keenReflexesStatDrawEntry);
        }

        return __result;
    }
}

[HarmonyPatch(typeof(ShotReport), "AimOnTargetChance_IgnoringPosture", MethodType.Getter)]
public static class ShotReport_AimOnTargetChance_IgnoringPosture_Patch
{
    public static bool Prepare()
    {
        return !ModsConfig.IsActive("Arquebus.StagzMerfolk");
    }

    private static float? _meleeToRangeCoefficient;

    private static float MeleeToRangeCoefficient => _meleeToRangeCoefficient ??= DefsOf.Stagz_KeenReflexes.HasModExtension<KeenReflexModExtension>() ? DefsOf.Stagz_KeenReflexes.GetModExtension<KeenReflexModExtension>().MeleeToRangeCoefficient : 1f;

    private static void Postfix(ref float __result, ref TargetInfo ___target)
    {
        if (___target == null) return;

        var pawn = ___target.Thing as Pawn;
        if (pawn != null && pawn.RaceProps.Humanlike && pawn.genes.HasActiveGene(DefsOf.Stagz_KeenReflexes) && __result < 1f)
        {
            __result = Math.Max(__result - (pawn.GetStatValue(StatDefOf.MeleeDodgeChance, true, -1) * MeleeToRangeCoefficient), 0.02f);
        }
    }
}

[HarmonyPatch(typeof(ShotReport), "GetTextReadout")]
public static class ShotReport_GetTextReadout_Patch
{
    public static bool Prepare()
    {
        return !ModsConfig.IsActive("Arquebus.StagzMerfolk");
    }

    private static void Postfix(ref string __result, ref TargetInfo ___target)
    {
        if (___target == null) return;

        var pawn = ___target.Thing as Pawn;
        if (pawn != null && pawn.RaceProps.Humanlike && pawn.genes.HasActiveGene(DefsOf.Stagz_KeenReflexes))
        {
            __result += "   " + "Goji_KeenReflexes".Translate() + " " + (pawn.GetStatValue(StatDefOf.MeleeDodgeChance, true, -1) * 1f).ToStringPercent() + "\n";
        }
    }
}
