using Verse;

namespace GojisMiscGenes
{
    [StaticConstructorOnStartup]
    public static class Startup
    {
        static Startup()
        {
            ModifyHussarWeaponAptitudeGenes();
        }

        private static void ModifyHussarWeaponAptitudeGenes()
        {
            if (!ModsConfig.IsActive("vanillaracesexpanded.hussar"))
            {
                return;
            }

            var weaponAptitudeCategory = DefDatabase<GeneCategoryDef>.GetNamed("VREH_WeaponAptitudes", errorOnFail: false);
            if (weaponAptitudeCategory != null)
            {
                foreach (var geneDef in DefDatabase<GeneDef>.AllDefsListForReading)
                {
                    if (geneDef.displayCategory == weaponAptitudeCategory && geneDef.biostatMet == -3)
                    {
                        geneDef.biostatMet = -1;
                    }
                }
            }
        }
    }
}
