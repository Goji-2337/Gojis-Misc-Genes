using HarmonyLib;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(HediffComp_Disappears), "CompShouldRemove", MethodType.Getter)]
    public static class HediffComp_Disappears_CompShouldRemove_Patch
    {
        public static void Postfix(HediffComp_Disappears __instance, ref bool __result)
        {
            if (__result && (__instance.parent.def == DefsOf.FibrousMechanites || __instance.parent.def == DefsOf.SensoryMechanites) && __instance.Pawn.HasActiveGene(DefsOf.Goji_PathogenHost))
            {
                __result = false;
            }
        }
    }
}
