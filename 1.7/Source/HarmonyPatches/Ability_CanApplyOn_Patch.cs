using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Ability), nameof(Ability.CanApplyOn), typeof(LocalTargetInfo))]
    public static class Ability_CanApplyOn_Patch
    {
        public static void Prefix(Ability __instance, ref ImposingPsyforceScope __state)
        {
            __state = new ImposingPsyforceScope(__instance.pawn);
        }

        public static void Postfix(ImposingPsyforceScope __state)
        {
            __state.Dispose();
        }
    }
}
