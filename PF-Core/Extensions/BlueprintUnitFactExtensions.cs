using System;
using Kingmaker.Blueprints.Facts;
using PF_Core.Facades;
using PF_Core.Factories;
using UnityEngine;

namespace PF_Core.Extensions
{
    public static class BlueprintUnitFactExtensions
    {
        private static readonly Harmony.FastSetter blueprintUnitFact_set_DisplayName = Harmony.CreateFieldSetter<BlueprintUnitFact>("m_DisplayName");
        private static readonly Harmony.FastSetter blueprintUnitFact_set_Description = Harmony.CreateFieldSetter<BlueprintUnitFact>("m_Description");
        private static readonly Harmony.FastSetter blueprintUnitFact_set_Icon = Harmony.CreateFieldSetter<BlueprintUnitFact>("m_Icon");

        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly LocalizationFactory LocalizationFactoryFactory = new LocalizationFactory();

        public static void SetNameDescriptionIcon(this BlueprintUnitFact feature, String displayName, String description, Sprite icon)
        {
            SetNameDescription(feature, displayName, description);
            feature.SetIcon(icon);
        }

        public static void SetNameDescription(this BlueprintUnitFact feature, String displayName, String description)
        {
            feature.SetName(LocalizationFactoryFactory.CreateString(feature.name + ".Name", displayName));
            feature.SetDescription(description);
        }

        public static void SetIcon(this BlueprintUnitFact feature, Sprite icon)
        {
            blueprintUnitFact_set_Icon(feature, icon);
        }

        public static void SetName(this BlueprintUnitFact feature, String name)
        {
            blueprintUnitFact_set_DisplayName(feature, LocalizationFactoryFactory.CreateString(feature.name + ".Name", name));
        }

        public static void SetDescription(this BlueprintUnitFact feature, String description)
        {
            blueprintUnitFact_set_Description(feature, LocalizationFactoryFactory.CreateString(feature.name + ".Description", description));
        }
    }
}
