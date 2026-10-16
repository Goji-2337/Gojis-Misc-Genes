using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(MemoryThoughtHandler), nameof(MemoryThoughtHandler.TryGainMemory), typeof(Thought_Memory), typeof(Pawn))]
    public static class MemoryThoughtHandler_TryGainMemory_Patch
    {
        private const int DurationMultiplier = 2;

        public static void Prefix(MemoryThoughtHandler __instance, Thought_Memory newThought)
        {
            if (__instance.pawn.HasActiveGene(DefsOf.Goji_PassiveAggressive) && newThought is Thought_MemorySocial social && newThought.def.durationDays > 0f && (social.CurStage?.baseOpinionOffset < 0f || social.def.stages.Any(s => s != null && s.baseOpinionOffset < 0f)))
            {
                newThought.durationTicksOverride = newThought.def.DurationTicks * DurationMultiplier;
            }
        }
    }
}
