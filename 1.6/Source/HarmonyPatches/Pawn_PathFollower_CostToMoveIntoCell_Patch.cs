using HarmonyLib;
using UnityEngine;
using Verse;
using Verse.AI;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn_PathFollower), "CostToMoveIntoCell", typeof(Pawn), typeof(IntVec3))]
    public static class Pawn_PathFollower_CostToMoveIntoCell_Patch
    {
        public static void Postfix(Pawn pawn, IntVec3 c, ref float __result)
        {
            if (pawn.HasActiveGene(DefsOf.Goji_Hoarder))
            {
                var terrain = c.GetTerrain(pawn.Map);
                var terrainCost = terrain != null ? terrain.pathCost : 0;
                var snowCost = WeatherBuildupUtility.MovementTicksAddOn(pawn.Map.snowGrid.GetCategory(c));
                var cleanPathCost = Mathf.Max(terrainCost, snowCost);

                var moveBase = (c.x != pawn.Position.x && c.z != pawn.Position.z) ? pawn.TicksPerMoveDiagonal : pawn.TicksPerMoveCardinal;
                var expectedCost = (float)(moveBase + cleanPathCost);
                if (pawn.CurJob != null)
                {
                    switch (pawn.jobs.curJob.locomotionUrgency)
                    {
                        case LocomotionUrgency.Amble:
                            expectedCost = Mathf.Max(expectedCost * 3f, 60f);
                            break;
                        case LocomotionUrgency.Walk:
                            expectedCost = Mathf.Max(expectedCost * 2f, 50f);
                            break;
                        case LocomotionUrgency.Jog:
                            break;
                        case LocomotionUrgency.Sprint:
                            expectedCost = Mathf.RoundToInt(expectedCost * 0.75f);
                            break;
                    }
                }
                __result = Mathf.Max(1f, Mathf.Min(__result, expectedCost));
            }
        }
    }
}
