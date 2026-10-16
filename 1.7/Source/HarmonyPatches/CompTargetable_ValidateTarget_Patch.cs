using System;
using HarmonyLib;
using RimWorld;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(CompTargetable), nameof(CompTargetable.ValidateTarget))]
    public static class CompTargetable_ValidateTarget_Patch
    {
        public static void Prefix(CompTargetable __instance, ref ImposingPsyforceScope __state)
        {
            __state = new ImposingPsyforceScope(__instance.CasterPawn);
        }

        public static void Postfix(ImposingPsyforceScope __state)
        {
            __state.Dispose();
        }
    }
}
