using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(SocialInteractionUtility), nameof(SocialInteractionUtility.CanInitiateInteraction))]
    public static class SocialInteractionUtility_CanInitiateInteraction_Patch
    {
        [ThreadStatic]
        public static int isTelepathyInteraction;

        public static bool Prepare() => ModsConfig.IsActive("vanillaracesexpanded.fungoid") && GojisMiscGenesMod.settings.disableFungoidPatch is false;

        public static void Prefix(Pawn pawn)
        {
            if (pawn.HasActiveGene(DefsOf.VRE_Telepathy))
            {
                isTelepathyInteraction++;
            }
        }

        public static void Postfix(Pawn pawn)
        {
            if (pawn.HasActiveGene(DefsOf.VRE_Telepathy))
            {
                isTelepathyInteraction--;
            }
        }
    }
}
