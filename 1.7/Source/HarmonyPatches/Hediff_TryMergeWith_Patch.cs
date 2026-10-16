using HarmonyLib;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Hediff), nameof(Hediff.TryMergeWith))]
    public static class Hediff_TryMergeWith_Patch
    {
        public static bool Prefix(Hediff __instance, Hediff other, ref bool __result)
        {
            if (other == null || other.def != __instance.def || other.Part != __instance.Part)
            {
                return true;
            }
            var pawn = __instance.pawn;
            if (__instance.def == DefsOf.GoJuiceHigh && pawn.HasActiveGene(DefsOf.Goji_DrugReceptive_GoJuice))
            {
                __result = false;
                return false;
            }
            if (__instance.def == DefsOf.WakeUpHigh && pawn.HasActiveGene(DefsOf.Goji_DrugReceptive_WakeUp))
            {
                __result = false;
                return false;
            }
            if ((__instance.def == DefsOf.PsychiteTeaHigh || __instance.def == DefsOf.YayoHigh || __instance.def == DefsOf.FlakeHigh) && pawn.HasActiveGene(DefsOf.Goji_DrugReceptive_Psychite))
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
