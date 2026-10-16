using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(ShotReport), "GetTextReadout")]
    public static class ShotReport_GetTextReadout_Patch
    {
        public static bool Prepare() => !ModsConfig.IsActive("Arquebus.StagzMerfolk");

        private static void Postfix(ref string __result, ref TargetInfo ___target)
        {
            if (___target == null) return;

            if (___target.Thing is Pawn pawn && pawn.RaceProps.Humanlike && pawn.genes.HasActiveGene(DefsOf.Stagz_KeenReflexes))
            {
                __result += "   " + "Goji_KeenReflexes".Translate() + " " + (pawn.GetStatValue(StatDefOf.MeleeDodgeChance, true, -1) * 1f).ToStringPercent() + "\n";
            }
        }
    }
}
