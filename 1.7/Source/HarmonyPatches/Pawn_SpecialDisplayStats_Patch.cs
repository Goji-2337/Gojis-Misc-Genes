using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn), "SpecialDisplayStats")]
    public static class Pawn_SpecialDisplayStats_Patch
    {
        public static bool Prepare() => !ModsConfig.IsActive("Arquebus.StagzMerfolk");

        private static IEnumerable<StatDrawEntry> Postfix(IEnumerable<StatDrawEntry> __result, Pawn __instance)
        {
            if (__instance != null && __instance.RaceProps.Humanlike && __instance.genes.HasActiveGene(DefsOf.Stagz_KeenReflexes))
            {
                var keenReflexesStatDrawEntry = new StatDrawEntry(StatCategoryDefOf.PawnCombat, "Goji_KeenReflexes".Translate(), "Goji_KeenReflexes_Value".Translate(), "Goji_KeenReflexes_Description".Translate(), 410000);
                return __result.Concat(keenReflexesStatDrawEntry);
            }

            return __result;
        }
    }
}
