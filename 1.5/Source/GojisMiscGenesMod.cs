using HarmonyLib;
using Verse;

namespace GojisMiscGenes
{
    public class GojisMiscGenesMod : Mod
    {
        public GojisMiscGenesMod(ModContentPack pack) : base(pack)
        {
            new Harmony("GojisMiscGenesMod").PatchAll();
        }
    }
}