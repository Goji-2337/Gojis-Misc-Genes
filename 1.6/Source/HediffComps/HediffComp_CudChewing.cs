using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    public class HediffCompProperties_CudChewing : HediffCompProperties
    {
        public HediffCompProperties_CudChewing()
        {
            compClass = typeof(HediffComp_CudChewing);
        }
    }

    public class HediffComp_CudChewing : HediffComp
    {
        public float nutritionToGive;
        public List<ThoughtDef> thoughtsToGive = new List<ThoughtDef>();
        public int ticksLeft = 15000;

        public override void CompPostTick(ref float severityAdjustment)
        {
            ticksLeft--;
            if (ticksLeft <= 0)
            {
                Pawn.needs.food.CurLevel += nutritionToGive;
                if (thoughtsToGive != null)
                {
                    foreach (var def in thoughtsToGive) Pawn.needs.mood.thoughts.memories.TryGainMemory(def);
                }
                Pawn.health.RemoveHediff(parent);
            }
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look(ref nutritionToGive, "nutritionToGive", 0f);
            Scribe_Values.Look(ref ticksLeft, "ticksLeft", 0);
            Scribe_Collections.Look(ref thoughtsToGive, "thoughtsToGive", LookMode.Def);
        }
    }
}
