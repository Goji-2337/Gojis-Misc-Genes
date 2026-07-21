using HarmonyLib;
using UnityEngine;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Verb), "CanHitTargetFrom")]
    public static class Verb_CanHitTargetFrom_Patch
    {
        public static void Postfix(Verb __instance, IntVec3 root, LocalTargetInfo targ, ref bool __result)
        {
            if (__result && !__instance.IsMeleeAttack && __instance.CasterPawn is Pawn casterPawn && casterPawn.HasActiveGene(DefsOf.Goji_TunnelVision))
            {
                var targetPos = targ.Cell.ToVector3Shifted();
                var pawnPos = root.ToVector3Shifted();
                if (targetPos != pawnPos)
                {
                    var attackDir = (targetPos - pawnPos).normalized;
                    var faceDir = casterPawn.Rotation.FacingCell.ToVector3().normalized;
                    if (Vector3.Angle(faceDir, attackDir) > 45f)
                    {
                        __result = false;
                    }
                }
            }
        }
    }
}
