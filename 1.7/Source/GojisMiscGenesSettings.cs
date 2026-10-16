using Verse;

namespace GojisMiscGenes
{
    public class GojisMiscGenesSettings : ModSettings
    {
        public bool zeroCostArchiteGenes;
        public bool disableHussarPatch;
        public bool disableFungoidPatch;
        public bool disablePhytokinPatch;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref zeroCostArchiteGenes, "zeroCostArchiteGenes", false);
            Scribe_Values.Look(ref disableHussarPatch, "disableHussarPatch", false);
            Scribe_Values.Look(ref disableFungoidPatch, "disableFungoidPatch", false);
            Scribe_Values.Look(ref disablePhytokinPatch, "disablePhytokinPatch", false);
            base.ExposeData();
        }
    }
}
