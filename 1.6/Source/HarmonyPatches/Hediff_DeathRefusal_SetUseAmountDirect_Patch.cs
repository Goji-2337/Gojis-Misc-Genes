using HarmonyLib;
using UnityEngine;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Hediff_DeathRefusal), "SetUseAmountDirect")]
    public static class Hediff_DeathRefusal_SetUseAmountDirect_Patch
    {
        public static void Prefix(Hediff_DeathRefusal __instance, ref int amount)
        {
            if (__instance.pawn.HasActiveGene(DefsOf.Goji_NineLives))
            {
                amount = Mathf.Min(amount, 9);
            }
        }
    }
    [HarmonyPatch(typeof(Hediff_DeathRefusal), nameof(Hediff_DeathRefusal.MaxUses), MethodType.Getter)]
    public static class Hediff_DeathRefusal_MaxUses_Patch
    {
        public static void Postfix(Hediff_DeathRefusal __instance, ref int __result)
        {
            if (__instance.pawn.HasActiveGene(DefsOf.Goji_NineLives))
            {
                __result = 9;
            }
        }
    }
}
