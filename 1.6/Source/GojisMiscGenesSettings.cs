using Verse;

namespace GojisMiscGenes
{
    public class GojisMiscGenesSettings : ModSettings
    {
        public bool zeroCostArchiteGenes;
        public bool disableVEPatches;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref zeroCostArchiteGenes, "zeroCostArchiteGenes", false);
            Scribe_Values.Look(ref disableVEPatches, "disableVEPatches", false);
            base.ExposeData();
        }
    }
}
