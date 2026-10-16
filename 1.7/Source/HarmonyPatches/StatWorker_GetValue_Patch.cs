using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(StatWorker), nameof(StatWorker.GetValue), typeof(StatRequest), typeof(bool))]
    public static class StatWorker_GetValue_Patch
    {
        public static void Postfix(StatWorker __instance, StatRequest req, bool applyPostProcess, ref float __result)
        {
            if (__instance.stat == StatDefOf.PsychicSensitivity && ImposingPsyforceContext.CurrentCaster != null && req.Thing is Pawn targetPawn && targetPawn != ImposingPsyforceContext.CurrentCaster)
            {
                __result = ImposingPsyforceContext.CurrentCaster.GetStatValue(StatDefOf.PsychicSensitivity, applyPostProcess);
            }
        }
    }
}
