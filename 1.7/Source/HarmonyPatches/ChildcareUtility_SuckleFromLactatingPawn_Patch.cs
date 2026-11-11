using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(ChildcareUtility), nameof(ChildcareUtility.SuckleFromLactatingPawn))]
    public static class ChildcareUtility_SuckleFromLactatingPawn_Patch
    {
        public static void Prefix(Pawn baby, ref float __state) => __state = baby.needs.food.CurLevelPercentage;

        public static void Postfix(Pawn baby, Pawn feeder, float __state)
        {
            if (baby.needs.food.CurLevelPercentage > __state)
            {
                Gene_Imprint.CheckImprint(baby, feeder);
            }
        }
    }
}
