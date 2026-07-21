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
        public static ThoughtDef Goji_DryadDiedGreaterDebuff;
        public static GeneDef Goji_CheekPouch;
        public static GeneDef Goji_WindRider;
        public static GeneDef Goji_RouteMemory;
        public static GeneDef Goji_CudChew;
        public static GeneDef Goji_Parroting;
        public static GeneDef Goji_MotionSickness;
        public static GeneDef Goji_NineLives;
        public static GeneDef Goji_TunnelVision;
        public static GeneDef Goji_PhantomPain;
        public static GeneDef Goji_DenseScar;
        public static GeneDef Goji_Hoarder;
        public static HediffDef Goji_MotionSicknessHediff;
        public static HediffDef Goji_CudChewing;
        public static TraitDef PsychicSensitivity;
        public static DamageArmorCategoryDef Blunt;

        static DefsOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(DefsOf));
        }
    }
}
