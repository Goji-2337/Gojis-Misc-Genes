using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI.Group;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(PsychicRitualDef), nameof(PsychicRitualDef.CalculateMaxPower))]
    public static class PsychicRitualDef_CalculateMaxPower_Patch
    {
        public const float QualityOffsetPerCarrier = -0.1f;

        public static bool Prepare() => ModsConfig.AnomalyActive;

        public static void Postfix(PsychicRitualRoleAssignments assignments, List<QualityFactor> powerFactorsOut, ref float power)
        {
            var carriers = 0;
            foreach (var pawn in assignments.AllAssignedPawns)
            {
                if (pawn.HasActiveGene(DefsOf.Goji_JinxedBloodline))
                {
                    carriers++;
                }
            }
            if (carriers > 0)
            {
                var offset = QualityOffsetPerCarrier * carriers;
                power = Mathf.Clamp01(power + offset);
                powerFactorsOut?.Add(new QualityFactor
                {
                    label = "Goji_JinxedBloodline_QualityFactor".Translate(),
                    positive = false,
                    count = offset.ToStringPercent(),
                    quality = offset,
                    toolTip = "Goji_JinxedBloodline_QualityFactor_Tooltip".Translate()
                });
            }
        }
    }
}