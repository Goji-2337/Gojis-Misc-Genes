using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn_GeneTracker), nameof(Pawn_GeneTracker.RemoveGene))]
    public static class Pawn_GeneTracker_RemoveGene_Patch
    {
        public static void Postfix(Gene gene, Pawn_GeneTracker __instance)
        {
            if (gene.def.exclusionTags == null || !gene.def.exclusionTags.Contains("Tail")) return;
            var nineLives = __instance.GetGene(DefsOf.Goji_NineLives);
            if (nineLives == null || nineLives.Active) return;
            var deathRefusal = __instance.pawn.health.hediffSet.GetFirstHediff<Hediff_DeathRefusal>();
            if (deathRefusal != null && deathRefusal.UsesLeft > 0)
            {
                deathRefusal.SetUseAmountDirect(0);
            }
        }
    }
}
