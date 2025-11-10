using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(IncidentWorker_DiseaseHuman), nameof(IncidentWorker_DiseaseHuman.PotentialVictimCandidates))]
    public static class IncidentWorker_DiseaseHuman_PotentialVictimCandidates_Patch
    {
        public static void Postfix(IncidentWorker_DiseaseHuman __instance, ref IEnumerable<Pawn> __result)
        {
            if (__instance.def != DefsOf.Disease_FibrousMechanites && __instance.def != DefsOf.Disease_SensoryMechanites)
            {
                return;
            }

            var pawnsToAdd = new List<Pawn>();
            foreach (var pawn in __result)
            {
                if (pawn.HasActiveGene(DefsOf.Goji_MechaniteProne))
                {
                    pawnsToAdd.Add(pawn);
                    pawnsToAdd.Add(pawn);
                }
            }

            if (pawnsToAdd.Any())
            {
                __result = __result.Concat(pawnsToAdd);
            }
        }
    }
}