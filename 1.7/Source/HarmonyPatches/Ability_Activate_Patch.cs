using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Ability), nameof(Ability.Activate), typeof(LocalTargetInfo), typeof(LocalTargetInfo))]
    public static class Ability_Activate_Patch
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
