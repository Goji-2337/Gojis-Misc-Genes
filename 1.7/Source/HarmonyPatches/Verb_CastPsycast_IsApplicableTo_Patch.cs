using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Verb_CastPsycast), nameof(Verb_CastPsycast.IsApplicableTo))]
    public static class Verb_CastPsycast_IsApplicableTo_Patch
    {
        public static void Prefix(Verb_CastPsycast __instance, ref ImposingPsyforceScope __state)
        {
            __state = new ImposingPsyforceScope(__instance.CasterPawn);
        }

        public static void Postfix(ImposingPsyforceScope __state)
        {
            __state.Dispose();
        }
    }
}
