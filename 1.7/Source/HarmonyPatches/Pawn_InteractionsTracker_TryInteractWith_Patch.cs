using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn_InteractionsTracker), nameof(Pawn_InteractionsTracker.TryInteractWith))]
    public static class Pawn_InteractionsTracker_TryInteractWith_Patch
    {
        public static bool Prepare() => ModsConfig.IsActive("vanillaracesexpanded.fungoid") && GojisMiscGenesMod.settings.disableFungoidPatch is false;

        public static void Prefix(Pawn_InteractionsTracker __instance, Pawn recipient)
        {
            if (__instance.pawn.HasActiveGene(DefsOf.VRE_Telepathy) && recipient.HasActiveGene(DefsOf.VRE_Telepathy))
            {
                SocialInteractionUtility_CanInitiateInteraction_Patch.isTelepathyInteraction++;
            }
        }

        public static void Postfix(Pawn_InteractionsTracker __instance, Pawn recipient)
        {
            if (__instance.pawn.HasActiveGene(DefsOf.VRE_Telepathy) && recipient.HasActiveGene(DefsOf.VRE_Telepathy))
            {
                SocialInteractionUtility_CanInitiateInteraction_Patch.isTelepathyInteraction--;
            }
        }
    }
}
