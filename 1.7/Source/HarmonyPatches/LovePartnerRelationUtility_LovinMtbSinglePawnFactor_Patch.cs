using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(LovePartnerRelationUtility), "LovinMtbSinglePawnFactor")]
    public static class LovePartnerRelationUtility_LovinMtbSinglePawnFactor_Patch
    {
        public static void Postfix(Pawn pawn, ref float __result)
        {
            var pain = pawn.health.hediffSet.PainTotal;
            if (pawn.HasActiveGene(DefsOf.Goji_PainStimulated) && pain > 0f)
            {
                __result = __result * (1f - pain) / (1f + pain);
            }
        }
    }
}
