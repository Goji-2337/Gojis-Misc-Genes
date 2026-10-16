using HarmonyLib;
using Verse.AI;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Verb_CastTargetEffectLances), nameof(Verb_CastTargetEffectLances.ValidateTarget))]
    public static class Verb_CastTargetEffectLances_ValidateTarget_Patch
    {
        public static void Prefix(Verb_CastTargetEffectLances __instance, ref ImposingPsyforceScope __state)
        {
            __state = new ImposingPsyforceScope(__instance.CasterPawn);
        }

        public static void Postfix(ImposingPsyforceScope __state)
        {
            __state.Dispose();
        }
    }
}
