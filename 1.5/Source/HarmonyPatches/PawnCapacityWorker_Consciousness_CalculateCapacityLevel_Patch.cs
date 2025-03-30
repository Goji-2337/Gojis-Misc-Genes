using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(PawnCapacityWorker_Consciousness), nameof(PawnCapacityWorker_Consciousness.CalculateCapacityLevel))]
    public static class PawnCapacityWorker_Consciousness_CalculateCapacityLevel_Patch
    {
        static MethodInfo PainFactorInfo = AccessTools.Method(typeof(PawnCapacityWorker_Consciousness_CalculateCapacityLevel_Patch), nameof(GetPainFactor));

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            var codes = instructions.ToList();
            bool foundTarget = false;

            for (int i = 0; i < codes.Count; i++)
            {
                var currentInstruction = codes[i];

                // Check if the current instruction is ldloc.1 (loading num2)
                // and the next instruction is sub
                if (currentInstruction.opcode == OpCodes.Ldloc_1 && i + 1 < codes.Count && codes[i + 1].opcode == OpCodes.Sub)
                {
                    // Yield the ldloc.1 first
                    yield return currentInstruction;

                    // Now insert our logic *after* ldloc.1 and *before* sub
                    foundTarget = true;
                    yield return new CodeInstruction(OpCodes.Ldarg_1); // Load HediffSet (diffSet) argument
                    yield return new CodeInstruction(OpCodes.Call, PainFactorInfo); // Call GetPainFactor(diffSet) -> returns 1f or -1f
                    yield return new CodeInstruction(OpCodes.Mul); // Multiply num2 (already on stack) by the factor

                    // The loop will naturally yield the 'sub' instruction in the next iteration
                }
                else
                {
                    // Yield the instruction as is if it's not the ldloc.1 before the sub
                    yield return currentInstruction;
                }
            }

            if (!foundTarget)
            {
                Log.Error("[GojisMiscGenes] Failed to find target IL sequence (ldloc.1 followed by sub) in PawnCapacityWorker_Consciousness.CalculateCapacityLevel transpiler.");
            }
        }

        // Helper method to determine if pain should be reversed
        // Returns 1f for normal behavior, -1f to reverse the effect (subtraction becomes addition)
        public static float GetPainFactor(HediffSet diffSet)
        {
            // Check if the pawn exists and has the specified gene active
            if (diffSet.pawn.HasActiveGene(DefsOf.Goji_PainStimulated))
            {
                return -1f; // Reverse the effect
            }
            return 1f; // Normal effect
        }
    }
}