using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn_InteractionsTracker), nameof(Pawn_InteractionsTracker.TryInteractWith))]
    public static class Pawn_InteractionsTracker_TryInteractWith_Patch
    {
        private const float SocialFightChanceThreshold = 0.01f;

        public static void Prefix(Pawn_InteractionsTracker __instance, Pawn recipient)
        {
            if (ModsConfig.IsActive("vanillaracesexpanded.fungoid") && GojisMiscGenesMod.settings.disableFungoidPatch is false
                && __instance.pawn.HasActiveGene(DefsOf.VRE_Telepathy) && recipient.HasActiveGene(DefsOf.VRE_Telepathy))
            {
                SocialInteractionUtility_CanInitiateInteraction_Patch.isTelepathyInteraction++;
            }
        }

        public static void Postfix(Pawn_InteractionsTracker __instance, Pawn recipient, InteractionDef intDef, bool __result)
        {
            if (ModsConfig.IsActive("vanillaracesexpanded.fungoid") && GojisMiscGenesMod.settings.disableFungoidPatch is false
                && __instance.pawn.HasActiveGene(DefsOf.VRE_Telepathy) && recipient.HasActiveGene(DefsOf.VRE_Telepathy))
            {
                SocialInteractionUtility_CanInitiateInteraction_Patch.isTelepathyInteraction--;
            }
            if (__result is false)
            {
                return;
            }
            var initiator = __instance.pawn;
            if (!initiator.HasActiveGene(DefsOf.Goji_SocialGrooming) || initiator.HostileTo(recipient) || recipient.filth is null)
            {
                return;
            }
            if (recipient.MentalStateDef == MentalStateDefOf.SocialFighting)
            {
                return;
            }
            if (IsPositiveInteraction(intDef) && recipient.filth.carriedFilth.Count > 0)
            {
                recipient.filth.carriedFilth.Clear();
            }
        }

        private static bool IsPositiveInteraction(InteractionDef intDef)
        {
            if (intDef.socialFightBaseChance > SocialFightChanceThreshold)
            {
                return false;
            }
            var thought = intDef.recipientThought;
            if (thought is not null)
            {
                foreach (var stage in thought.stages)
                {
                    if (stage is not null && (stage.baseOpinionOffset > 0f || stage.baseMoodEffect > 0f))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
