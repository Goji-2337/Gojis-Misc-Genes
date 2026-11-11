using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn_AgeTracker), "BirthdayBiological")]
    public static class Pawn_AgeTracker_BirthdayBiological_Patch
    {
        public static void Postfix(Pawn_AgeTracker __instance, int birthdayAge)
        {
            if (birthdayAge >= 3 && __instance.pawn.HasActiveGene(DefsOf.Goji_Imprint, out var gene) && gene is Gene_Imprint imprint && imprint.traitCopied is false)
            {
                imprint.TryCopyParentTrait();
            }
            if (ModsConfig.AnomalyActive && __instance.pawn.HasActiveGene(DefsOf.Goji_NineLives) && __instance.pawn.ageTracker.Adult)
            {
                var deathRefusal = __instance.pawn.health.hediffSet.GetFirstHediff<Hediff_DeathRefusal>();
                if (deathRefusal == null)
                {
                    deathRefusal = (Hediff_DeathRefusal)HediffMaker.MakeHediff(HediffDefOf.DeathRefusal, __instance.pawn);
                    __instance.pawn.health.AddHediff(deathRefusal);
                    deathRefusal.SetUseAmountDirect(1);
                }
                else if (deathRefusal.UsesLeft < 9)
                {
                    deathRefusal.SetUseAmountDirect(deathRefusal.UsesLeft + 1);
                }
            }
        }
    }
}
