using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(HediffSet), nameof(HediffSet.AddDirect))]
    public static class HediffSet_AddDirect_Patch
    {
        public static bool Prefix(HediffSet __instance, ref Hediff hediff)
        {
            if (hediff.def == HediffDefOf.Hypothermia && __instance.pawn.genes.HasActiveGene(DefsOf.Goji_Hibernation))
            {
                var severity = hediff.Severity;
                Hediff firstHediffOfDef = __instance.pawn.health.hediffSet.GetFirstHediffOfDef(DefsOf.HypothermicSlowdown);
                if (firstHediffOfDef != null)
                {
                    firstHediffOfDef.Severity += severity;
                    return false;
                }
                else
                {
                    hediff = HediffMaker.MakeHediff(DefsOf.HypothermicSlowdown, __instance.pawn, hediff.Part);
                    hediff.Severity = severity;
                }
            }
            return true;
        }
    }
}