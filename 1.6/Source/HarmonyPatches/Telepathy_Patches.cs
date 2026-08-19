using System;
using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    public static class TelepathyState
    {
        [ThreadStatic]
        public static int isTelepathyInteraction;
    }

    [HarmonyPatch(typeof(SocialInteractionUtility), nameof(SocialInteractionUtility.CanInitiateInteraction))]
    public static class Telepathy_CanInitiateInteraction_Patch
    {
        public static bool Prepare() => ModsConfig.IsActive("vanillaracesexpanded.fungoid") && !GojisMiscGenesMod.settings.disableFungoidPatch;

        public static void Prefix(Pawn pawn)
        {
            if (pawn.HasActiveGene(DefsOf.VRE_Telepathy))
            {
                TelepathyState.isTelepathyInteraction++;
            }
        }

        public static void Postfix(Pawn pawn)
        {
            if (pawn.HasActiveGene(DefsOf.VRE_Telepathy))
            {
                TelepathyState.isTelepathyInteraction--;
            }
        }
    }

    [HarmonyPatch(typeof(Pawn_InteractionsTracker), nameof(Pawn_InteractionsTracker.CanInteractNowWith))]
    public static class Telepathy_CanInteractNowWith_Patch
    {
        public static bool Prepare() => ModsConfig.IsActive("vanillaracesexpanded.fungoid") && !GojisMiscGenesMod.settings.disableFungoidPatch;

        public static void Prefix(Pawn_InteractionsTracker __instance, Pawn recipient)
        {
            if (__instance.pawn.HasActiveGene(DefsOf.VRE_Telepathy) && recipient.HasActiveGene(DefsOf.VRE_Telepathy))
            {
                TelepathyState.isTelepathyInteraction++;
            }
        }

        public static void Postfix(Pawn_InteractionsTracker __instance, Pawn recipient)
        {
            if (__instance.pawn.HasActiveGene(DefsOf.VRE_Telepathy) && recipient.HasActiveGene(DefsOf.VRE_Telepathy))
            {
                TelepathyState.isTelepathyInteraction--;
            }
        }
    }

    [HarmonyPatch(typeof(Pawn_InteractionsTracker), nameof(Pawn_InteractionsTracker.TryInteractWith))]
    public static class Telepathy_TryInteractWith_Patch
    {
        public static bool Prepare() => ModsConfig.IsActive("vanillaracesexpanded.fungoid") && !GojisMiscGenesMod.settings.disableFungoidPatch;

        public static void Prefix(Pawn_InteractionsTracker __instance, Pawn recipient)
        {
            if (__instance.pawn.HasActiveGene(DefsOf.VRE_Telepathy) && recipient.HasActiveGene(DefsOf.VRE_Telepathy))
            {
                TelepathyState.isTelepathyInteraction++;
            }
        }

        public static void Postfix(Pawn_InteractionsTracker __instance, Pawn recipient)
        {
            if (__instance.pawn.HasActiveGene(DefsOf.VRE_Telepathy) && recipient.HasActiveGene(DefsOf.VRE_Telepathy))
            {
                TelepathyState.isTelepathyInteraction--;
            }
        }
    }

    [HarmonyPatch(typeof(PawnCapacitiesHandler), nameof(PawnCapacitiesHandler.CapableOf))]
    public static class Telepathy_CapableOf_Patch
    {
        public static bool Prepare() => ModsConfig.IsActive("vanillaracesexpanded.fungoid") && !GojisMiscGenesMod.settings.disableFungoidPatch;

        public static bool Prefix(PawnCapacityDef capacity, ref bool __result)
        {
            if (TelepathyState.isTelepathyInteraction > 0 && (capacity == PawnCapacityDefOf.Talking || capacity == PawnCapacityDefOf.Hearing))
            {
                __result = true;
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(CompAbility_RequiresCapacity), nameof(CompAbility_RequiresCapacity.GizmoDisabled))]
    public static class Telepathy_RequiresCapacity_Patch
    {
        public static bool Prepare() => ModsConfig.IsActive("vanillaracesexpanded.fungoid") && !GojisMiscGenesMod.settings.disableFungoidPatch;

        public static void Postfix(CompAbility_RequiresCapacity __instance, ref bool __result)
        {
            if (__result && __instance.Props.capacity == PawnCapacityDefOf.Talking && __instance.parent.pawn.HasActiveGene(DefsOf.VRE_Telepathy))
            {
                __result = false;
            }
        }
    }

    [HarmonyPatch(typeof(Dialog_BeginRitual), "BlockingIssues")]
    public static class Telepathy_RitualBlocking_Patch
    {
        public static bool Prepare() => ModsConfig.IsActive("vanillaracesexpanded.fungoid") && !GojisMiscGenesMod.settings.disableFungoidPatch;

        public static IEnumerable<string> Postfix(IEnumerable<string> __result, Dialog_BeginRitual __instance)
        {
            if (__instance.organizer.HasActiveGene(DefsOf.VRE_Telepathy) && !__instance.organizer.health.capacities.CapableOf(PawnCapacityDefOf.Talking))
            {
                foreach (var pawn in __instance.assignments.Participants)
                {
                    if (!pawn.HasActiveGene(DefsOf.VRE_Telepathy))
                    {
                        yield return "Goji_TelepathyRitualRequiresAllCarriers".Translate();
                        yield break;
                    }
                }
            }
        }
    }
}
