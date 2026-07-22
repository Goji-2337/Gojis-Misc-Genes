using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(TransportersArrivalAction_FormCaravan), nameof(TransportersArrivalAction_FormCaravan.Arrived))]
    public static class TransportersArrivalAction_FormCaravan_Arrived_Patch
    {
        public static void Postfix(List<ActiveTransporterInfo> transporters, PlanetTile tile)
        {
            if (!ModsConfig.BiotechActive)
            {
                return;
            }

            var allThings = new List<Thing>();
            foreach (var transporter in transporters)
            {
                ThingOwnerUtility.GetAllThingsRecursively(transporter, allThings);
            }
            foreach (var thing in allThings)
            {
                if (thing is Pawn pawn && pawn.HasActiveGene(DefsOf.Goji_MotionSickness))
                {
                    pawn.health.AddHediff(DefsOf.Goji_MotionSicknessHediff);
                }
            }
            allThings.Clear();
        }
    }
}