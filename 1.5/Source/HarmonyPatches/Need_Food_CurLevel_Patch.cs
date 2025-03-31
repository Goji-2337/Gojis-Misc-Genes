using HarmonyLib;
using RimWorld;
using Verse;
using UnityEngine;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Need), "set_CurLevel")]
    public static class Need_Food_CurLevel_Patch
    {
        public static bool Prefix(Need __instance, float value)
        {
            if (__instance is Need_Food needFood)
            {
                Pawn pawn = needFood.pawn;
                if (pawn != null && pawn.HasActiveGene(DefsOf.Goji_CheekPouch))
                {
                    float maxLevel = needFood.MaxLevel;
                    float cheekPouchLimit = maxLevel * 2f;
                    float clampedValue = Mathf.Clamp(value, 0f, cheekPouchLimit);
                    __instance.curLevelInt = clampedValue;
                    return false;
                }
            }
            return true;
        }
    }
}