using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Verb_CastAbility), nameof(Verb_CastAbility.IsApplicableTo))]
    public static class Verb_CastAbility_IsApplicableTo_Patch
    {
        public static void Prefix(Verb_CastAbility __instance, ref ImposingPsyforceScope __state)
        {
            __state = new ImposingPsyforceScope(__instance.CasterPawn);
        }

        public static void Postfix(ImposingPsyforceScope __state)
        {
            __state.Dispose();
        }
    }
}
