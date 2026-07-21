using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(HediffSet), "CalculatePain")]
    public static class HediffSet_CalculatePain_Patch
    {
        public static void Postfix(HediffSet __instance, ref float __result)
        {
            if (__instance.pawn.HasActiveGene(DefsOf.Goji_PhantomPain))
            {
                var extraPain = __instance.GetMissingPartsCommonAncestors().Count * 0.04f;
                foreach (var hediff in __instance.hediffs)
                {
                    if (hediff is Hediff_AddedPart addedPart && !addedPart.def.organicAddedBodypart) extraPain += 0.04f;
                }
                __result = Mathf.Clamp(__result + extraPain, 0f, 1f);
            }
        }
    }
}
