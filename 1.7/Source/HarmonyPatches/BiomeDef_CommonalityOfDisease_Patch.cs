using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(BiomeDef), nameof(BiomeDef.CommonalityOfDisease))]
    public static class BiomeDef_CommonalityOfDisease_Patch
    {
        public static bool Prepare() => ModsConfig.RoyaltyActive;

        public static void Postfix(IncidentDef diseaseInc, ref float __result)
        {
            if ((diseaseInc == DefsOf.Disease_BloodRot || diseaseInc == DefsOf.Disease_Abasia) && PawnsFinder.AllMapsCaravansAndTravellingTransporters_Alive_FreeColonistsAndPrisoners.Any(p => p.HasActiveGene(DefsOf.Goji_ExoticDiseaseProne)))
            {
                __result = 1f;
            }
        }
    }
}
