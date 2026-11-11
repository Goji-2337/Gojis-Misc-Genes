using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse.AI;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(JobDriver_FeedBaby), nameof(JobDriver_FeedBaby.MakeNewToils))]
    public static class JobDriver_FeedBaby_MakeNewToils_Patch
    {
        public static IEnumerable<Toil> Postfix(IEnumerable<Toil> values, JobDriver_FeedBaby __instance)
        {
            foreach (var toil in values)
            {
                if (toil == __instance.FeedingToil)
                {
                    toil.AddFinishAction(() =>
                    {
                        var baby = __instance.Baby;
                        if (baby.Dead is false && baby.needs.food.CurLevelPercentage > __instance.initialFoodPercentage)
                        {
                            Gene_Imprint.CheckImprint(baby, __instance.pawn);
                        }
                    });
                }
                yield return toil;
            }
        }
    }
}
