using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn_PathFollower), "TryEnterNextPathCell")]
    public static class Pawn_PathFollower_TryEnterNextPathCell_Patch
    {
        private const float GrowthIncrement = 0.01f;
        private const float DustScale = 0.5f;

        public static void Postfix(Pawn_PathFollower __instance)
        {
            var pawn = __instance.pawn;
            if (pawn.Spawned && pawn.HasActiveGene(DefsOf.Goji_Pollinator))
            {
                var plant = pawn.Position.GetPlant(pawn.Map);
                if (plant != null) TryPollinate(plant);
            }
        }

        private static void TryPollinate(Plant plant)
        {
            if (plant.def.plant.IsTree || plant.LifeStage != PlantLifeStage.Growing || plant.Blighted)
            {
                return;
            }
            var currentTick = Find.TickManager.TicksGame;
            var lastTick = plant.GetLastPollinatedTick();
            if (lastTick > 0 && currentTick - lastTick < GenDate.TicksPerDay)
            {
                return;
            }
            plant.SetLastPollinatedTick(currentTick);
            plant.Growth += GrowthIncrement;
            FleckMaker.ThrowDustPuff(plant.Position.ToVector3Shifted(), plant.Map, DustScale);
        }
    }
}
