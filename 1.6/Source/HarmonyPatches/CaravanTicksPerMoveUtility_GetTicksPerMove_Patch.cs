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
            if (!caravan.pather.Moving) return;

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
                var mapParent = Find.WorldObjects.WorldObjectAt<MapParent>(caravan.pather.Destination);
                if (mapParent != null && mapParent.HasMap)
                {
                    __result = Mathf.RoundToInt(__result / 1.2f);
                }
            }
        }
    }
}
