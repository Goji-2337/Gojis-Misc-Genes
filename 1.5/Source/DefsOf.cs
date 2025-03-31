using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [DefOf]
    public static class DefsOf
    {
        public static GeneDef Goji_MechaniteProne;
        public static GeneDef Goji_PainStimulated;
        public static GeneDef Goji_Clairvoyance;
        public static GeneDef Goji_Hibernation;
        public static IncidentDef Disease_FibrousMechanites;
        public static IncidentDef Disease_SensoryMechanites;
        public static HediffDef HypothermicSlowdown;
        public static GeneDef Goji_GauranlenDescendant;
        public static AbilityDef Goji_CommandDryad;
        public static ThoughtDef Goji_DryadDiedGreaterDebuff;
        public static GeneDef Goji_CheekPouch;
        public static GeneDef Goji_WindRider;

        static DefsOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(DefsOf));
        }
    }
}