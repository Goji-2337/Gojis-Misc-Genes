using System.Collections.Generic;
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
                var itemCost = 0;
                var costWithoutItems = 0;
                var things = pawn.Map.thingGrid.ThingsListAtFast(c);
                var terrainDef = pawn.Map.terrainGrid.TerrainAt(c);
                if (terrainDef != null) costWithoutItems = terrainDef.pathCost;
                foreach (var t in things)
                {
                    if (t.def.category == ThingCategory.Item)
                    {
                        if (t.def.pathCost > itemCost) itemCost = t.def.pathCost;
                    }
                    else
                    {
                        if (t.def.pathCost > costWithoutItems) costWithoutItems = t.def.pathCost;
                    }
                }
                var snowCost = WeatherBuildupUtility.MovementTicksAddOn(pawn.Map.snowGrid.GetCategory(c));
                if (snowCost > costWithoutItems) costWithoutItems = snowCost;

                var diff = Mathf.Max(itemCost, costWithoutItems) - costWithoutItems;
                if (diff > 0) __result = Mathf.Max(1f, __result - diff);
            }
        }
    }
}
