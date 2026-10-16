using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Thing), "Ingested")]
    public static class Thing_Ingested_Patch
    {
        public static void Postfix(Thing __instance, Pawn ingester, float __result)
        {
            if (__result > 0f && ingester.HasActiveGene(DefsOf.Goji_CudChew))
            {
                var foodKind = FoodUtility.GetFoodKind(__instance);
                if (foodKind != FoodKind.Meat && !__instance.def.IsMeat && !__instance.def.IsAnimalProduct)
                {
                    var stomachPart = ingester.health.hediffSet.GetBodyPartRecord(DefsOf.Stomach);
                    var hediff = ingester.health.hediffSet.GetFirstHediffOfDef(DefsOf.Goji_CudChewing);
                    if (hediff == null)
                    {
                        hediff = HediffMaker.MakeHediff(DefsOf.Goji_CudChewing, ingester, stomachPart);
                        ingester.health.AddHediff(hediff);
                    }

                    var comp = hediff.TryGetComp<HediffComp_CudChewing>();
                    comp.nutritionToGive = __result * 0.5f;
                    comp.ingestedDef = __instance.def;

                    var ingestingThoughts = FoodUtility.ThoughtsFromIngesting(ingester, __instance, __instance.def);
                    var thoughts = new List<ThoughtDef>();
                    foreach (var ingestingThought in ingestingThoughts)
                    {
                        thoughts.Add(ingestingThought.thought);
                    }
                    comp.thoughtsToGive = thoughts;
                    comp.ticksLeft = 15000;
                }
            }
        }
    }
}
