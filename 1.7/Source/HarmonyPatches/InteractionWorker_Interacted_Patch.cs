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
                        HediffDef buffHediffDef = null;
                        if (__instance.interaction == DefsOf.WorkDrive)
                        {
                            buffHediffDef = DefsOf.WorkDriveHediff;
                        }
                        else if (__instance.interaction == DefsOf.PreachHealth)
                        {
                            buffHediffDef = DefsOf.PreachHealthHediff;
                        }
                        if (buffHediffDef != null)
                        {
                            var buffHediff = initiator.health.AddHediff(buffHediffDef, initiator.health.hediffSet.GetBrain());
                            var ownHediff = recipient.health.hediffSet.GetFirstHediffOfDef(buffHediffDef);
                            var comp = ownHediff.TryGetComp<HediffComp_Disappears>();
                            buffHediff.TryGetComp<HediffComp_Disappears>().SetDuration(comp.ticksToDisappear);
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
