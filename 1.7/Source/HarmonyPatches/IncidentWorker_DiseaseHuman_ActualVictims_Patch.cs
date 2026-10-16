using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(IncidentWorker_DiseaseHuman), "ActualVictims")]
    public static class IncidentWorker_DiseaseHuman_ActualVictims_Patch
    {
        public static void Postfix(ref IEnumerable<Pawn> __result)
        {
            if (__result != null && __result.Count() > 1)
            {
                __result = __result.Distinct().ToList();
            }
        }
    }
}