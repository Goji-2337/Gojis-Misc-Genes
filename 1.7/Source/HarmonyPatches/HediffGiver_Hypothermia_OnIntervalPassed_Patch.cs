using HarmonyLib;
using Verse;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(HediffGiver_Hypothermia), nameof(HediffGiver_Hypothermia.OnIntervalPassed))]
    public static class HediffGiver_Hypothermia_OnIntervalPassed_Patch
    {
        private static HediffDef TryOverrideHediffDef(HediffDef originalDef, Pawn pawn)
        {
            if (pawn.HasActiveGene(DefsOf.Goji_Hibernation))
            {
                return DefsOf.HypothermicSlowdown;
            }
            return originalDef;
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundTarget = false;

            foreach (var instruction in instructions)
            {
                yield return instruction;

                if (instruction.opcode == OpCodes.Stloc_S && instruction.operand is LocalBuilder lb && lb.LocalIndex == 4)
                {
                    foundTarget = true;
                    yield return new CodeInstruction(OpCodes.Ldloc_S, lb);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return CodeInstruction.Call(typeof(HediffGiver_Hypothermia_OnIntervalPassed_Patch), nameof(TryOverrideHediffDef));
                    yield return new CodeInstruction(OpCodes.Stloc_S, lb);
                }
            }

            if (!foundTarget)
            {
                Log.Error("GojisMiscGenes: HediffGiver_Hypothermia.OnIntervalPassed transpiler failed to find target stloc.s 4 instruction.");
            }
        }
    }
}