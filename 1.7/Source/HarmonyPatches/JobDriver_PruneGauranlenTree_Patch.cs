using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(JobDriver_PruneGauranlenTre), nameof(JobDriver_PruneGauranlenTre.MakeNewToils))]
    public static class JobDriver_PruneGauranlenTree_MakeNewToils_Patch
    {
        public static bool Prepare() => ModsConfig.IsActive("vanillaracesexpanded.phytokin") && !GojisMiscGenesMod.settings.disablePhytokinPatch;

        public static IEnumerable<Toil> Postfix(IEnumerable<Toil> values, JobDriver_PruneGauranlenTre __instance)
        {
            var toils = values.ToList();
            var pawn = __instance.pawn;
            var lastToil = toils.LastOrDefault();
            if (lastToil != null)
            {
                lastToil.AddFinishAction(delegate
                {
                    if (pawn.HasActiveGene(DefsOf.VRE_GreenThumb))
                    {
                        pawn.needs?.mood?.thoughts?.memories?.TryGainMemory(DefsOf.VRE_GreenThumbHappy);
                    }
                });
            }
            return toils;
        }
    }
}
