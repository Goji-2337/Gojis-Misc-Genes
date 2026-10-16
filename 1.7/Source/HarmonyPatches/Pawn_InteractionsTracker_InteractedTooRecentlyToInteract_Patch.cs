using HarmonyLib;
using RimWorld;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn_InteractionsTracker), "InteractedTooRecentlyToInteract")]
    public static class Pawn_InteractionsTracker_InteractedTooRecentlyToInteract_Patch
    {
        public static void Postfix(ref bool __result)
        {
            if (InteractionWorker_Interacted_Patch.isParroting) __result = false;
        }
    }
}
