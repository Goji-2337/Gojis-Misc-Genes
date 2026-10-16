using Verse;

namespace GojisMiscGenes
{
    public static class PawnExtensions
    {
        public static bool HasActiveGene(this Pawn pawn, GeneDef geneDef)
        {
            return pawn?.genes?.HasActiveGene(geneDef) ?? false;
        }

        public static bool HasActiveGene(this Pawn pawn, GeneDef geneDef, out Gene gene)
        {
            gene = pawn?.genes?.GetGene(geneDef);
            return gene != null && gene.Active;
        }
    }
}