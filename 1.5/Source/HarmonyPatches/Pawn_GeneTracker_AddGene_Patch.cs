using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn_GeneTracker), nameof(Pawn_GeneTracker.AddGene), new Type[] { typeof(GeneDef), typeof(bool) })]
    public static class Pawn_GeneTracker_AddGene_Patch
    {
        public static bool Prefix(Pawn_GeneTracker __instance, GeneDef geneDef)
        {
            if (geneDef == DefsOf.Goji_GauranlenDescendant)
            {
                foreach (Pawn otherPawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive)
                {
                    if (otherPawn != __instance.pawn && otherPawn.HasActiveGene(DefsOf.Goji_GauranlenDescendant))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}