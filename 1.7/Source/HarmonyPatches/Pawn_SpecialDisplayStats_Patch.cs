using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn), nameof(Pawn.SpecialDisplayStats))]
    public static class Pawn_SpecialDisplayStats_Patch
    {
        public static void Postfix(ref IEnumerable<StatDrawEntry> __result, Pawn __instance)
        {
            if (__instance.RaceProps.Humanlike && !ModsConfig.IsActive("Arquebus.StagzMerfolk") && __instance.genes.HasActiveGene(DefsOf.Stagz_KeenReflexes))
            {
                __result = __result.Concat(new StatDrawEntry(StatCategoryDefOf.PawnCombat, "Goji_KeenReflexes".Translate(), "Goji_KeenReflexes_Value".Translate(), "Goji_KeenReflexes_Description".Translate(), 410000));
            }
            if (__instance.HasActiveGene(DefsOf.Goji_Imprint, out var gene) && gene is Gene_Imprint imprint)
            {
                string value = imprint.imprintedParent is not null
                    ? "Goji_ImprintedParent".Translate(imprint.imprintedParent.Named("PARENT")).Resolve()
                    : "Goji_NotYetImprinted".Translate().Resolve();
                __result = __result.Concat(new StatDrawEntry(StatCategoryDefOf.PawnSocial, DefsOf.Goji_Imprint.LabelCap, value, "Goji_Imprint_Desc".Translate(), 429000));
            }
        }
    }
}
