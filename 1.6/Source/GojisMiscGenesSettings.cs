using Verse;

namespace GojisMiscGenes
{
    public class GojisMiscGenesSettings : ModSettings
    {
        public bool zeroCostArchiteGenes;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref zeroCostArchiteGenes, "zeroCostArchiteGenes", false);
            base.ExposeData();
        }
    }
}
