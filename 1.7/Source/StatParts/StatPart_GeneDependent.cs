using RimWorld;
using Verse;
using System;

namespace GojisMiscGenes
{
    public class StatPart_GeneDependent : StatPart
    {
        public GeneDef gene;
        public StatDef factorStat;

        public override void TransformValue(StatRequest req, ref float val)
        {
            if (ActiveFor(req, out Pawn pawn))
            {
                float factor = pawn.GetStatValue(factorStat);
                // Use the minValue of the stat this part is attached to (parentStat)
                val *= Math.Max(factor, parentStat.minValue);
            }
        }

        public override string ExplanationPart(StatRequest req)
        {
            if (ActiveFor(req, out Pawn pawn))
            {
                float factor = pawn.GetStatValue(factorStat);
                // Use the minValue of the stat this part is attached to (parentStat)
                float displayFactor = Math.Max(factor, parentStat.minValue);
                string prefix = "Goji_StatPart_GeneDependent_Prefix".Translate(factorStat.LabelCap);
                string suffix = "Goji_StatPart_GeneDependent_Suffix".Translate(gene.LabelCap);
                return $"{prefix} x{displayFactor.ToStringPercent()} {suffix}";
            }
            return null;
        }

        private bool ActiveFor(StatRequest req, out Pawn pawn)
        {
            pawn = null;
            if (gene == null || factorStat == null)
            {
                return false;
            }

            if (req.HasThing && req.Thing is Pawn p)
            {
                pawn = p;
                return pawn.HasActiveGene(gene);
            }
            return false;
        }
    }
}