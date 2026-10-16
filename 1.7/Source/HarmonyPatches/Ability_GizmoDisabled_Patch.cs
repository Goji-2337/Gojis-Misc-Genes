using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Ability), nameof(Ability.GizmoDisabled))]
    public static class Ability_GizmoDisabled_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions, MethodBase baseMethod)
        {
            var patched = false;
            var codes = codeInstructions.ToList();
            for (var i = 0; i < codes.Count; i++)
            {
                var codeInstruction = codes[i];
                bool shouldPatch = codeInstruction.Calls(AccessTools.Method(typeof(Pawn), nameof(Pawn.WorkTagIsDisabled)));

                yield return codeInstruction;

                if (shouldPatch)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Ability_GizmoDisabled_Patch), nameof(OverrideViolenceCheck)));
                    patched = true;
                }
            }

            if (!patched)
            {
                Log.Error($"GojisMiscGenes: failed to patch Ability.GizmoDisabled - couldn't find WorkTagIsDisabled call in {baseMethod.DeclaringType?.Namespace}.{baseMethod.DeclaringType?.Name}:{baseMethod.Name}");
            }
        }

        public static bool OverrideViolenceCheck(bool result, Ability instance)
        {
            if (result && instance is Psycast && instance.pawn.HasActiveGene(DefsOf.Goji_ImposingPsyforce))
            {
                return false;
            }
            return result;
        }
    }
}