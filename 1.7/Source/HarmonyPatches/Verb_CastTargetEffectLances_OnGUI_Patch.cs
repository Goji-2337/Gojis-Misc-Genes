using HarmonyLib;
using Verse.AI;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Verb_CastTargetEffectLances), nameof(Verb_CastTargetEffectLances.OnGUI))]
    public static class Verb_CastTargetEffectLances_OnGUI_Patch
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
