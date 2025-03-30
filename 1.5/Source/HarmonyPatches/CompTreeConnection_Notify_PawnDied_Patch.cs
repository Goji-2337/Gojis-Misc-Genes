using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(CompTreeConnection), nameof(CompTreeConnection.Notify_PawnDied))]
    public static class CompTreeConnection_Notify_PawnDied_Patch
    {
        private static ThoughtDef TryOverrideThought(ThoughtDef originalDef, Pawn connectedPawn)
        {
            if (connectedPawn.HasActiveGene(DefsOf.Goji_GauranlenDescendant))
            {
                return DefsOf.Goji_DryadDiedGreaterDebuff;
            }
            else
            {
                foreach (Pawn otherPawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive)
                {
                    if (otherPawn != connectedPawn && otherPawn.HasActiveGene(DefsOf.Goji_GauranlenDescendant))
                    {
                        otherPawn.needs?.mood?.thoughts?.memories.TryGainMemory(DefsOf.Goji_DryadDiedGreaterDebuff);
                    }
                }
            }
            return originalDef;
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundLdsfld = false;
            FieldInfo dryadDiedField = AccessTools.Field(typeof(ThoughtDefOf), nameof(ThoughtDefOf.DryadDied));
            FieldInfo connectedPawnField = AccessTools.Field(typeof(CompTreeConnection), "connectedPawn");
            foreach (var instruction in instructions)
            {
                yield return instruction;
                if (instruction.LoadsField(dryadDiedField))
                {
                    foundLdsfld = true;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, connectedPawnField);
                    yield return CodeInstruction.Call(typeof(CompTreeConnection_Notify_PawnDied_Patch), nameof(TryOverrideThought)); // 4. Call helper, replacing value on stack
                }
            }

            if (!foundLdsfld)
            {
                Log.Error("GojisMiscGenes: CompTreeConnection.Notify_PawnDied transpiler failed to find ldsfld ThoughtDefOf.DryadDied instruction.");
            }
        }
    }
}