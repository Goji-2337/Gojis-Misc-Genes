using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Skyfaller), "Impact")]
    public static class Skyfaller_Impact_Patch
    {
        public static void Prefix(Skyfaller __instance)
        {
            var innerThings = new List<Thing>();
            ThingOwnerUtility.GetAllThingsRecursively(__instance, innerThings);
            foreach (var item in innerThings)
            {
                if (item is Pawn pawn && pawn.HasActiveGene(DefsOf.Goji_MotionSickness))
                {
                    pawn.health.AddHediff(DefsOf.Goji_MotionSicknessHediff);
                }
            }
        }
    }
}
