using RimWorld;
using Verse;
using UnityEngine;

namespace GojisMiscGenes
{
    public class StatPart_WindRiderSpeed : StatPart
    {
        private const float MinWindFactor = 0.1f;

        public override void TransformValue(StatRequest req, ref float val)
        {
            if (ActiveFor(req, out Pawn pawn))
            {
                if (!pawn.Position.Roofed(pawn.Map))
                {
                    float windSpeed = pawn.Map.windManager.WindSpeed;
                    val *= Mathf.Max(windSpeed, MinWindFactor);
                }
            }
        }

        public override string ExplanationPart(StatRequest req)
        {
            if (ActiveFor(req, out Pawn pawn))
            {
                if (!pawn.Position.Roofed(pawn.Map))
                {
                    float windSpeed = pawn.Map.windManager.WindSpeed;
                    string prefix = "Goji_StatPart_WindRiderSpeed".Translate();
                    string suffix = "Goji_StatPart_GeneDependent_Suffix".Translate(DefsOf.Goji_WindRider.LabelCap);
                    return $"{prefix} x{windSpeed.ToStringPercent()} {suffix}";
                }
            }
            return null;
        }

        private bool ActiveFor(StatRequest req, out Pawn pawn)
        {
            pawn = req.Thing as Pawn;
            return pawn != null && pawn.Map != null && pawn.HasActiveGene(DefsOf.Goji_WindRider);
        }
    }
}