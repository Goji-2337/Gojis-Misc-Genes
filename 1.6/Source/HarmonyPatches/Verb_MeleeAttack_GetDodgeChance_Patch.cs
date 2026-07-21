using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Verb_MeleeAttack), "GetDodgeChance")]
    public static class Verb_MeleeAttack_GetDodgeChance_Patch
    {
        public static void Postfix(Verb_MeleeAttack __instance, LocalTargetInfo target, ref float __result)
        {
            if (target.Thing is Pawn defender && defender.HasActiveGene(DefsOf.Goji_TunnelVision))
            {
                var attackDir = (__instance.CasterPawn.DrawPos - defender.DrawPos).normalized;
                var faceDir = defender.Rotation.FacingCell.ToVector3().normalized;
                if (Vector3.Angle(faceDir, attackDir) > 90f)
                {
                    __result = 0f;
                }
            }
        }
    }
}
