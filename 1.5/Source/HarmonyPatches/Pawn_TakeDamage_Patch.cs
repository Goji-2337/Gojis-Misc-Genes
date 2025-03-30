using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Thing), nameof(Thing.TakeDamage))]
    public static class Thing_TakeDamage_Patch
    {
        public static bool Prefix(Thing __instance, DamageInfo dinfo)
        {
            if (__instance is Pawn pawn && dinfo.Def == DamageDefOf.Frostbite && pawn.HasActiveGene(DefsOf.Goji_Hibernation))
            {
                return false;
            }
            return true;
        }
    }
}