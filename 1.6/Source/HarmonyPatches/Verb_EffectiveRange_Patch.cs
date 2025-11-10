using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;
using System;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Verb), "get_EffectiveRange")]
    public static class Verb_EffectiveRange_Patch
    {
        public static void Postfix(Verb __instance, ref float __result)
        {
            if (__instance is Verb_CastAbility castAbility && castAbility.ability is Psycast)
            {
                var pawn = castAbility.caster as Pawn;
                if (pawn != null && pawn.HasActiveGene(DefsOf.Goji_Clairvoyance))
                {
                    var psySensitivity = pawn.GetStatValue(StatDefOf.PsychicSensitivity);
                    var bonusRange = (int)Math.Floor(psySensitivity - 1f);
                    if (bonusRange > 0 && __result > 0)
                    {
                        __result += bonusRange;
                    }
                }
            }
        }
    }
}