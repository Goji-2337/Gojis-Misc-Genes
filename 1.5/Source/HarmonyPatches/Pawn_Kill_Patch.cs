using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn), nameof(Pawn.Kill))]
    public static class Pawn_Kill_Patch
    {
        public static void Prefix(Pawn __instance, DamageInfo? dinfo, Hediff exactCulprit = null)
        {
            if (__instance.Map == null || __instance.genes == null || !__instance.HasActiveGene(DefsOf.Goji_GauranlenDescendant))
            {
                return;
            }

            foreach (Thing treeThing in __instance.Map.listerThings.ThingsOfDef(ThingDefOf.Plant_TreeGauranlen))
            {
                CompTreeConnection treeComp = treeThing.TryGetComp<CompTreeConnection>();

                if (treeComp != null && treeComp.ConnectedPawn == __instance)
                {
                    if (treeComp.dryads != null)
                    {
                        List<Pawn> dryadsCopy = new List<Pawn>(treeComp.dryads);

                        foreach (Pawn dryad in dryadsCopy)
                        {
                            if (dryad != null && dryad.Spawned && !dryad.Dead)
                            {
                                var cocoon = GenSpawn.Spawn(ThingDefOf.DryadCocoon, dryad.Position, dryad.Map);
                                CompDryadCocoon cocoonComp = cocoon.TryGetComp<CompDryadCocoon>();
                                if (cocoonComp != null)
                                {
                                    cocoonComp.TryAcceptPawn(dryad);
                                }
                                else
                                {
                                    Log.Error($"GojisMiscGenes: Failed to get CompDryadCocoon for spawned cocoon at {dryad.Position} for dryad {dryad.LabelShort}. Destroying cocoon.");
                                    cocoon.Destroy();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}