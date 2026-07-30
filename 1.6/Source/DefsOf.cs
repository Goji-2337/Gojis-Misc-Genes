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
        public static GeneDef Goji_PhantomPain;
        public static GeneDef Goji_DenseScar;
        public static GeneDef Goji_Hoarder;
        public static HediffDef Goji_MotionSicknessHediff;
        public static HediffDef Goji_CudChewing;
        public static TraitDef PsychicSensitivity;
        public static DamageArmorCategoryDef Blunt;
        public static ThingDef Ambrosia;
        public static HediffDef AmbrosiaHigh;
        [MayRequireIdeology]
        public static InteractionDef WorkDrive;
        [MayRequireIdeology]
        public static InteractionDef PreachHealth;
        [MayRequireIdeology]
        [DefAlias("WorkDrive")]
        public static HediffDef WorkDriveHediff;
        [MayRequireIdeology]
        [DefAlias("PreachHealth")]
        public static HediffDef PreachHealthHediff;
        public static BodyPartDef Stomach;
        public static MeditationFocusDef Natural;
        public static GeneDef Goji_NatureRhythm;
        public static GeneDef Goji_PathogenHost;
        public static HediffDef FibrousMechanites;
        public static HediffDef SensoryMechanites;
        public static HediffDef GutWorms;
        public static HediffDef MuscleParasites;
        [MayRequire("vanillaracesexpanded.fungoid")]
        public static GeneDef VRE_Telepathy;
        [MayRequire("vanillaracesexpanded.phytokin")]
        public static GeneDef VRE_GreenThumb;
        [MayRequire("vanillaracesexpanded.phytokin")]
        public static ThoughtDef VRE_GreenThumbHappy;
        public static GeneDef Stagz_KeenReflexes;

        static DefsOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(DefsOf));
        }
    }
}
