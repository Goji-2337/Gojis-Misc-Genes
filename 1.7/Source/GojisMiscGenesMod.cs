using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace GojisMiscGenes
{
    public class GojisMiscGenesMod : Mod
    {
        public static GojisMiscGenesSettings settings;
        private static Dictionary<GeneDef, int> originalArchiteMetabolismCosts = new Dictionary<GeneDef, int>();

        public GojisMiscGenesMod(ModContentPack pack) : base(pack)
        {
            settings = GetSettings<GojisMiscGenesSettings>();
            new Harmony("GojisMiscGenesMod").PatchAll();
            ApplyZeroCostArchiteSetting();
        }

        public override void WriteSettings()
        {
            base.WriteSettings();
            ApplyZeroCostArchiteSetting();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            var listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled("Goji_Setting_ZeroCostArchite_Label".Translate(), ref settings.zeroCostArchiteGenes, "Goji_Setting_ZeroCostArchite_Description".Translate());
            listingStandard.CheckboxLabeled("Goji_Setting_DisableHussar_Label".Translate(), ref settings.disableHussarPatch, "Goji_Setting_DisableHussar_Description".Translate());
            listingStandard.CheckboxLabeled("Goji_Setting_DisableFungoid_Label".Translate(), ref settings.disableFungoidPatch, "Goji_Setting_DisableFungoid_Description".Translate());
            listingStandard.CheckboxLabeled("Goji_Setting_DisablePhytokin_Label".Translate(), ref settings.disablePhytokinPatch, "Goji_Setting_DisablePhytokin_Description".Translate());
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return Content.Name;
        }

        public static void ApplyZeroCostArchiteSetting()
        {
            var applyZeroCost = settings.zeroCostArchiteGenes;
            foreach (var geneDef in DefDatabase<GeneDef>.AllDefs)
            {
                if (geneDef.biostatArc > 0 && (geneDef.modContentPack == null || !geneDef.modContentPack.IsOfficialMod))
                {
                    if (applyZeroCost)
                    {
                        if (!originalArchiteMetabolismCosts.ContainsKey(geneDef) && geneDef.biostatMet != 0)
                        {
                            originalArchiteMetabolismCosts[geneDef] = geneDef.biostatMet;
                        }
                        if (geneDef.biostatMet != 0)
                        {
                            geneDef.biostatMet = 0;
                        }
                    }
                    else
                    {
                        if (originalArchiteMetabolismCosts.TryGetValue(geneDef, out var originalValue))
                        {
                            geneDef.biostatMet = originalValue;
                        }
                    }
                }
            }
        }
    }
}
