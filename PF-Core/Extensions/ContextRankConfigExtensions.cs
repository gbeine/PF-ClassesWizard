using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using PF_Core.Facades;

namespace PF_Core.Extensions
{
    public static class ContextRankConfigExtensions
    {
        private static readonly Harmony.FastSetter contextRankConfig_set_Type = Harmony.CreateFieldSetter<ContextRankConfig>("m_Type");
        private static readonly Harmony.FastSetter contextRankConfig_set_BaseValueType = Harmony.CreateFieldSetter<ContextRankConfig>("m_BaseValueType");
        private static readonly Harmony.FastSetter contextRankConfig_set_Progression = Harmony.CreateFieldSetter<ContextRankConfig>("m_Progression");
        private static readonly Harmony.FastSetter contextRankConfig_set_Stat = Harmony.CreateFieldSetter<ContextRankConfig>("m_Stat");
        private static readonly Harmony.FastSetter contextRankConfig_set_UseMin = Harmony.CreateFieldSetter<ContextRankConfig>("m_UseMin");
        private static readonly Harmony.FastSetter contextRankConfig_set_Min = Harmony.CreateFieldSetter<ContextRankConfig>("m_Min");
        private static readonly Harmony.FastSetter contextRankConfig_set_UseMax = Harmony.CreateFieldSetter<ContextRankConfig>("m_UseMax");
        private static readonly Harmony.FastSetter contextRankConfig_set_Max = Harmony.CreateFieldSetter<ContextRankConfig>("m_Max");
        private static readonly Harmony.FastSetter contextRankConfig_set_StartLevel = Harmony.CreateFieldSetter<ContextRankConfig>("m_StartLevel");
        private static readonly Harmony.FastSetter contextRankConfig_set_StepLevel = Harmony.CreateFieldSetter<ContextRankConfig>("m_StepLevel");
        private static readonly Harmony.FastSetter contextRankConfig_set_ExceptClasses = Harmony.CreateFieldSetter<ContextRankConfig>("m_ExceptClasses");
        private static readonly Harmony.FastSetter contextRankConfig_set_Class = Harmony.CreateFieldSetter<ContextRankConfig>("m_Class");
        private static readonly Harmony.FastSetter contextRankConfig_set_Archetype = Harmony.CreateFieldSetter<ContextRankConfig>("Archetype");
        private static readonly Harmony.FastSetter contextRankConfig_set_Feature = Harmony.CreateFieldSetter<ContextRankConfig>("m_Feature");
        private static readonly Harmony.FastSetter contextRankConfig_set_FeatureList = Harmony.CreateFieldSetter<ContextRankConfig>("m_FeatureList");
        private static readonly Harmony.FastSetter contextRankConfig_set_CustomProperty = Harmony.CreateFieldSetter<ContextRankConfig>("m_CustomProperty");
        private static readonly Harmony.FastSetter contextRankConfig_set_CustomProgression = Harmony.CreateFieldSetter<ContextRankConfig>("m_CustomProgression");

        public static Type GetTypeOf(this ContextRankConfig contextRankConfig, String name) => Harmony.GetType<ContextRankConfig>(name);

        public static void SetType(this ContextRankConfig contextRankConfig, AbilityRankType abilityRankType) =>
            contextRankConfig_set_Type(contextRankConfig, abilityRankType);
        public static void SetBaseValueType(this ContextRankConfig contextRankConfig, ContextRankBaseValueType contextRankBaseValueType) =>
            contextRankConfig_set_BaseValueType(contextRankConfig, contextRankBaseValueType);
        public static void SetProgression(this ContextRankConfig contextRankConfig, ContextRankProgression contextRankProgression) =>
            contextRankConfig_set_Progression(contextRankConfig, contextRankProgression);
        public static void SetStat(this ContextRankConfig contextRankConfig, StatType statType) =>
            contextRankConfig_set_Stat(contextRankConfig, statType);

        public static void SetUseMin(this ContextRankConfig contextRankConfig, bool useMin) =>
            contextRankConfig_set_UseMin(contextRankConfig, useMin);
        public static void SetMin(this ContextRankConfig contextRankConfig, int min) =>
            contextRankConfig_set_Min(contextRankConfig, min);

        public static void SetUseMax(this ContextRankConfig contextRankConfig, bool useMax) =>
            contextRankConfig_set_UseMax(contextRankConfig, useMax);
        public static void SetMax(this ContextRankConfig contextRankConfig, int max) =>
            contextRankConfig_set_Max(contextRankConfig, max);

        public static void SetStartLevel(this ContextRankConfig contextRankConfig, int startLevel) =>
            contextRankConfig_set_StartLevel(contextRankConfig, startLevel);
        public static void SetStepLevel(this ContextRankConfig contextRankConfig, int stepLevel) =>
            contextRankConfig_set_StepLevel(contextRankConfig, stepLevel);

        public static void SetExceptClasses(this ContextRankConfig contextRankConfig, bool exceptClasses) =>
            contextRankConfig_set_ExceptClasses(contextRankConfig, exceptClasses);

        public static void SetClass(this ContextRankConfig contextRankConfig, BlueprintCharacterClass[] blueprintCharacterClasses) =>
            contextRankConfig_set_Class(contextRankConfig, blueprintCharacterClasses);
        public static void SetArchetype(this ContextRankConfig contextRankConfig, BlueprintArchetype blueprintArchetype) =>
            contextRankConfig_set_Archetype(contextRankConfig, blueprintArchetype);
        public static void SetFeature(this ContextRankConfig contextRankConfig, BlueprintFeature blueprintFeature) =>
            contextRankConfig_set_Feature(contextRankConfig, blueprintFeature);
        public static void SetFeatureList(this ContextRankConfig contextRankConfig, BlueprintFeature[] blueprintFeatures) =>
            contextRankConfig_set_FeatureList(contextRankConfig, blueprintFeatures);

        public static void SetCustomProperty(this ContextRankConfig contextRankConfig, BlueprintUnitProperty blueprintUnitProperty) =>
            contextRankConfig_set_CustomProperty(contextRankConfig, blueprintUnitProperty);
        public static void SetCustomProgression(this ContextRankConfig contextRankConfig, Array customProgression) =>
            contextRankConfig_set_CustomProgression(contextRankConfig, customProgression);
    }
}
