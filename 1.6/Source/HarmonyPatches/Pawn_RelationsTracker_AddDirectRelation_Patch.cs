using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn_RelationsTracker), nameof(Pawn_RelationsTracker.AddDirectRelation))]
    public static class Pawn_RelationsTracker_AddDirectRelation_Patch
    {
        public static bool Prefix(Pawn_RelationsTracker __instance, PawnRelationDef def, Pawn otherPawn)
        {
            var pawn = __instance.pawn;
            if ((def == PawnRelationDefOf.Parent || def == PawnRelationDefOf.ParentBirth)
                && pawn.HasActiveGene(DefsOf.Goji_Imprint, out var gene) && gene is Gene_Imprint imprint)
            {
                if (imprint.imprintedParent is not null)
                {
                    if (imprint.imprintedParent != otherPawn)
                    {
                        return false;
                    }
                }
                else if (pawn.DevelopmentalStage.Baby())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
