using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;

namespace GojisMiscGenes
{
    public class CompProperties_AbilityCommandDryad : CompProperties_AbilityEffect
    {
        public float cooldownPerCocoonDays = 1f;

        public CompProperties_AbilityCommandDryad()
        {
            compClass = typeof(CompAbilityEffect_CommandDryad);
        }
    }

    public class CompAbilityEffect_CommandDryad : CompAbilityEffect
    {
        public new CompProperties_AbilityCommandDryad Props => (CompProperties_AbilityCommandDryad)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn caster = parent.pawn;
            int cocoonsAffected = 0;
            Map map = caster.Map;

            if (map == null)
            {
                Log.ErrorOnce($"GojisMiscGenes: CompAbilityEffect_CommandDryad caster {caster.LabelShort} has no map.", caster.thingIDNumber ^ 1948571);
                return;
            }

            List<CompTreeConnection> casterTrees = new List<CompTreeConnection>();
            foreach (Thing treeThing in map.listerThings.ThingsOfDef(ThingDefOf.Plant_TreeGauranlen))
            {
                CompTreeConnection treeComp = treeThing.TryGetComp<CompTreeConnection>();
                if (treeComp != null && treeComp.ConnectedPawn == caster)
                {
                    casterTrees.Add(treeComp);
                }
            }

            if (!casterTrees.Any()) return;

            HashSet<Pawn> casterDryads = new HashSet<Pawn>();
            foreach (var treeComp in casterTrees)
            {
                if (treeComp.dryads != null)
                {
                    foreach (var dryad in treeComp.dryads)
                    {
                        if (dryad != null)
                        {
                            casterDryads.Add(dryad);
                        }
                    }
                }
            }

            if (!casterDryads.Any()) return;

            List<Thing> allThings = map.listerThings.AllThings.ToList();
            foreach (Thing thing in allThings)
            {
                CompDryadCocoon cocoonComp = thing.TryGetComp<CompDryadCocoon>();
                if (cocoonComp != null)
                {
                    if (!cocoonComp.innerContainer.NullOrEmpty() && cocoonComp.dryadKind != null)
                    {
                        Pawn pawnInside = cocoonComp.innerContainer.FirstOrDefault() as Pawn;
                        if (pawnInside != null && casterDryads.Contains(pawnInside))
                        {
                            cocoonComp.Complete();
                            cocoonsAffected++;
                            FleckMaker.ThrowDustPuff(thing.Position.ToVector3Shifted(), map, 1.5f);
                        }
                    }
                }
            }

            if (cocoonsAffected > 0 && parent.def.cooldownTicksRange.max > 0)
            {
                float cooldownDays = cocoonsAffected * Props.cooldownPerCocoonDays;
                int cooldownTicks = (int)(cooldownDays * GenDate.TicksPerDay);
                parent.StartCooldown(cooldownTicks);
            }
        }
    }
}