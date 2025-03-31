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


            foreach (Thing cocoonThing in map.listerThings.AllThings.ToList())
            {
                CompDryadCocoon cocoonComp = cocoonThing.TryGetComp<CompDryadCocoon>();
                if (cocoonComp != null)
                {
                    bool isTransforming = !cocoonComp.innerContainer.NullOrEmpty() &&
                cocoonComp.dryadKind != null;

                    if (isTransforming)
                    {
                        cocoonComp.Complete();
                        cocoonsAffected++;
                        FleckMaker.ThrowDustPuff(cocoonThing.Position.ToVector3Shifted(), map, 1.5f);
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