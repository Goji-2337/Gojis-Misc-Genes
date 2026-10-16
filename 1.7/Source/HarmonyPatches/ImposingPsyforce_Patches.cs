using System;
using Verse;

namespace GojisMiscGenes
{
    public static class ImposingPsyforceContext
    {
        [ThreadStatic]
        public static Pawn CurrentCaster;
    }

    public struct ImposingPsyforceScope : IDisposable
    {
        private Pawn previous;

        public ImposingPsyforceScope(Pawn caster)
        {
            previous = ImposingPsyforceContext.CurrentCaster;
            if (caster != null && caster.HasActiveGene(DefsOf.Goji_ImposingPsyforce))
            {
                ImposingPsyforceContext.CurrentCaster = caster;
            }
        }

        public void Dispose()
        {
            ImposingPsyforceContext.CurrentCaster = previous;
        }
    }
}
