using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [DefOf]
    public static class DefsOf
    {
        public static GeneDef Goji_MechaniteProne;
        public static IncidentDef Disease_FibrousMechanites;
        public static IncidentDef Disease_SensoryMechanites;

        static DefsOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(DefsOf));
        }
    }
}