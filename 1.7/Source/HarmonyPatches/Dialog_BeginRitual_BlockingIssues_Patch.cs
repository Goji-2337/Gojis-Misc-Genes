using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Dialog_BeginRitual), "BlockingIssues")]
    public static class Dialog_BeginRitual_BlockingIssues_Patch
    {
        public static bool Prepare() => ModsConfig.IsActive("vanillaracesexpanded.fungoid") && GojisMiscGenesMod.settings.disableFungoidPatch is false;

        public static IEnumerable<string> Postfix(IEnumerable<string> __result, Dialog_BeginRitual __instance)
        {
            if (__result != null)
            {
                foreach (var issue in __result)
                {
                    yield return issue;
                }
            }
            if (__instance.organizer.HasActiveGene(DefsOf.VRE_Telepathy) && !__instance.organizer.health.capacities.CapableOf(PawnCapacityDefOf.Talking))
            {
                foreach (var pawn in __instance.assignments.Participants)
                {
                    if (!pawn.HasActiveGene(DefsOf.VRE_Telepathy))
                    {
                        yield return "Goji_TelepathyRitualRequiresAllCarriers".Translate();
                        yield break;
                    }
                }
            }
        }
    }
}
