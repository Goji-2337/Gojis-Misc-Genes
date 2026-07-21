using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    public class Gene_NineLives : Gene
    {
        public override bool Active
        {
            get
            {
                var genes = pawn.genes.GenesListForReading;
                foreach (var gene in genes)
                {
                    if (gene.Active && gene.def.exclusionTags != null && gene.def.exclusionTags.Contains("Tail"))
                    {
                        return base.Active;
                    }
                }
                return false;
            }
        }

        public override void Tick()
        {
            base.Tick();
            if (ModsConfig.AnomalyActive && pawn.IsHashIntervalTick(250) && Active is false)
            {
                var deathRefusal = pawn.health.hediffSet.GetFirstHediff<Hediff_DeathRefusal>();
                if (deathRefusal != null && deathRefusal.UsesLeft > 0)
                {
                    deathRefusal.SetUseAmountDirect(0);
                }
            }
        }
    }
}
