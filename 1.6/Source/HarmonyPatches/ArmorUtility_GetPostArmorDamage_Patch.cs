using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(ArmorUtility), "GetPostArmorDamage")]
    public static class ArmorUtility_GetPostArmorDamage_Patch
    {
        public static void Postfix(Pawn pawn, ref float __result, float armorPenetration, BodyPartRecord part, ref DamageDef damageDef, ref bool deflectedByMetalArmor, ref bool diminishedByMetalArmor)
        {
            if (__result > 0f && part != null && pawn.HasActiveGene(DefsOf.Goji_DenseScar))
            {
                if (damageDef.armorCategory == DamageArmorCategoryDefOf.Sharp || damageDef.armorCategory == DefsOf.Blunt)
                {
                    var extraArmor = 0f;
                    foreach (var hediff in pawn.health.hediffSet.hediffs)
                    {
                        if (hediff.Part == part && hediff.IsPermanent()) extraArmor += hediff.Severity / 100f;
                    }
                    if (extraArmor > 0f)
                    {
                        var armorRating = Mathf.Max(extraArmor - armorPenetration, 0f);
                        var value = Rand.Value;
                        if (value < armorRating * 0.5f)
                        {
                            __result = 0f;
                            deflectedByMetalArmor = true;
                        }
                        else if (value < armorRating)
                        {
                            __result = Mathf.Max(1f, Mathf.RoundToInt(__result * 0.5f));
                            diminishedByMetalArmor = true;
                            if (damageDef.armorCategory == DamageArmorCategoryDefOf.Sharp)
                            {
                                var blunt = DefsOf.Blunt;
                                if (blunt != null) damageDef.armorCategory = blunt;
                            }
                        }
                    }
                }
            }
        }
    }
}
