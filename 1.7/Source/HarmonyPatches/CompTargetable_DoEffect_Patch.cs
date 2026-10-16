using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(CompTargetable), nameof(CompTargetable.DoEffect))]
    public static class CompTargetable_DoEffect_Patch
    {
        public static void Prefix(Pawn usedBy, ref ImposingPsyforceScope __state)
        {
            __state = new ImposingPsyforceScope(usedBy);
        }

        public static void Postfix(ImposingPsyforceScope __state)
        {
            __state.Dispose();
        }
    }
}
