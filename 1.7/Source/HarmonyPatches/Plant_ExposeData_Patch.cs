using System.Runtime.CompilerServices;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Plant), nameof(Plant.ExposeData))]
    public static class Plant_ExposeData_Patch
    {
        public static void Postfix(Plant __instance)
        {
            var lastPollinatedTick = __instance.GetLastPollinatedTick();
            Scribe_Values.Look(ref lastPollinatedTick, "lastPollinatedTick", 0);
            if (lastPollinatedTick > 0)
            {
                __instance.SetLastPollinatedTick(lastPollinatedTick);
            }
        }

        private static ConditionalWeakTable<Plant, PlantPollinationData> plantLastPollinatedTicks = new();

        public static int GetLastPollinatedTick(this Plant plant)
        {
            if (plantLastPollinatedTicks.TryGetValue(plant, out var data))
            {
                return data.lastPollinatedTick;
            }
            return 0;
        }

        public static void SetLastPollinatedTick(this Plant plant, int lastPollinatedTick)
        {
            plantLastPollinatedTicks.GetOrCreateValue(plant).lastPollinatedTick = lastPollinatedTick;
        }

        private class PlantPollinationData
        {
            public int lastPollinatedTick;
        }
    }
}
