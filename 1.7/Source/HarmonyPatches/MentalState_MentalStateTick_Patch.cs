using HarmonyLib;
using Verse;
using Verse.AI;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(MentalState), nameof(MentalState.MentalStateTick))]
    public static class MentalState_MentalStateTick_Patch
    {
        private const int Interval = 30;
        private const int UnsetMaxDurationThreshold = 99999999;

        public static bool Prefix(MentalState __instance, int delta)
        {
            if (__instance.causedByMood && __instance.pawn.HasActiveGene(DefsOf.Goji_PassiveAggressive)
                && __instance.def.maxTicksBeforeRecovery < UnsetMaxDurationThreshold)
            {
                if (__instance.pawn.IsHashIntervalTick(Interval, delta))
                {
                    __instance.age += Interval;
                    if (__instance.age >= __instance.def.maxTicksBeforeRecovery || __instance.forceRecoverAfterTicks != -1 && __instance.age >= __instance.forceRecoverAfterTicks) __instance.RecoverFromState();
                }
                return false;
            }
            return true;
        }
    }
}
