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
        public static void Prefix(Pawn __instance)
        {
            if (__instance.genes != null && __instance.HasActiveGene(DefsOf.Goji_GauranlenDescendant))
            {
                foreach (Thing tree in __instance.Map.listerThings.ThingsOfDef(ThingDefOf.Plant_TreeGauranlen))
                {
                    CompTreeConnection treeComp = tree.TryGetComp<CompTreeConnection>();
                    var dryadsList = treeComp.dryads;
                    List<Pawn> dryadsCopy = new List<Pawn>(dryadsList);

                    foreach (Pawn dryad in dryadsCopy)
                    {
                        if (dryad.Spawned && !dryad.Dead)
                        {
                            var cocoon = GenSpawn.Spawn(ThingDefOf.DryadCocoon, dryad.Position, dryad.Map);
                            CompDryadCocoon cocoonComp = cocoon.TryGetComp<CompDryadCocoon>();
                            if (cocoonComp != null)
                            {
                                cocoonComp.TryAcceptPawn(dryad);
                            }
                            else
                            {
                                cocoon.Destroy();
                            }
                        }
                    }
                }
            }
        }
    }
}