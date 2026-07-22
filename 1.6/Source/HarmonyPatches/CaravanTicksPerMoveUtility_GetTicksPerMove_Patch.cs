using System.Text;
using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(CaravanTicksPerMoveUtility), "GetTicksPerMove", typeof(Caravan), typeof(StringBuilder))]
    public static class CaravanTicksPerMoveUtility_GetTicksPerMove_Patch
    {
        public static void Postfix(Caravan caravan, ref int __result)
        {
            var destination = caravan.pather.Destination;
            if (destination.Valid is false) return;

            var hasRouteMemory = false;
            var pawns = caravan.PawnsListForReading;
            foreach (var pawn in pawns)
            {
                if (pawn.HasActiveGene(DefsOf.Goji_RouteMemory))
                {
                    hasRouteMemory = true;
                    break;
                }
            }

            if (hasRouteMemory)
            {
                var mapParent = Find.WorldObjects.WorldObjectAt<MapParent>(destination);
                if (mapParent != null && mapParent.HasMap || mapParent is Settlement settlement && settlement.EverVisited)
                {
                    __result = Mathf.RoundToInt(__result / 1.2f);
                }
            }
        }
    }
}
