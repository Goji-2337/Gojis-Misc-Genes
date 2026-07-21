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
                }
                finally
                {
                    isParroting = false;
                }
            }
        }
    }
}
