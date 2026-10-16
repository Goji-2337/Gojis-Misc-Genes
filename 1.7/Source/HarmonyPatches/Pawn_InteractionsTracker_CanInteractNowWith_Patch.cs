using HarmonyLib;
using RimWorld;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn_InteractionsTracker), "CanInteractNowWith")]
    public static class Pawn_InteractionsTracker_CanInteractNowWith_Patch
    {
        public static void Postfix(ref bool __result)
        {
            if (InteractionWorker_Interacted_Patch.isParroting) __result = true;
        }
    }
}
