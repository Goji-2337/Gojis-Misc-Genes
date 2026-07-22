using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(InteractionWorker), "Interacted")]
    public static class InteractionWorker_Interacted_Patch
    {
        public static bool isParroting;

        public static void Postfix(InteractionWorker __instance, Pawn initiator, Pawn recipient)
        {
            if (!isParroting && initiator != recipient && recipient.HasActiveGene(DefsOf.Goji_Parroting))
            {
                isParroting = true;
                try
                {
                    recipient.interactions.TryInteractWith(initiator, __instance.interaction);
                    if (ModsConfig.IdeologyActive)
                    {
                        Hediff buffHediff = null;
                        if (__instance.interaction == DefsOf.WorkDrive)
                        {
                            buffHediff = initiator.health.AddHediff(DefsOf.WorkDriveHediff, initiator.health.hediffSet.GetBrain());
                        }
                        else if (__instance.interaction == DefsOf.PreachHealth)
                        {
                            buffHediff = initiator.health.AddHediff(DefsOf.PreachHealthHediff, initiator.health.hediffSet.GetBrain());
                        }
                        if (buffHediff != null)
                        {
                            buffHediff.TryGetComp<HediffComp_Disappears>().SetDuration(GenDate.TicksPerDay);
                        }
                    }
                }
                finally
                {
                    isParroting = false;
                }
            }
        }
    }
}
