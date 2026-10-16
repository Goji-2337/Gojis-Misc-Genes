using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Verb_CastAbility), nameof(Verb_CastAbility.ValidateTarget))]
    public static class Verb_CastAbility_ValidateTarget_Patch
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
