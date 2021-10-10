using System;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using PF_Core.Extensions;
using PF_Core.Facades;
using UnityEngine;

namespace PF_Core.Factories
{
    public class ProgressionFactory
    {
        private static readonly Harmony.FastSetter blueprintProgression_set_AssetId = Harmony.CreateFieldSetter<BlueprintProgression>("m_AssetGuid");

        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        public BlueprintProgression createProgression(String name, String guid, String displayName, String description,
            Sprite icon,
            FeatureGroup group, params BlueprintComponent[] components) =>
            createProgression(name, guid, displayName, description, icon, group, Array.Empty<BlueprintCharacterClass>(),
                Array.Empty<BlueprintArchetype>(), components);

        public BlueprintProgression createProgression(String name, String guid, String displayName, String description,
            Sprite icon,
            FeatureGroup group, BlueprintCharacterClass characterClass, params BlueprintComponent[] components) =>
            createProgression(name, guid, displayName, description, icon, group, new BlueprintCharacterClass[] { characterClass },
                Array.Empty<BlueprintArchetype>(), components);

        public BlueprintProgression createProgression(String name, String guid, String displayName, String description,
            Sprite icon,
            FeatureGroup group, BlueprintCharacterClass[] classes, params BlueprintComponent[] components) =>
            createProgression(name, guid, displayName, description, icon, group, classes,
                Array.Empty<BlueprintArchetype>(), components);

        public BlueprintProgression createProgression(String name, String guid, String displayName, String description,
            Sprite icon,
            FeatureGroup group, BlueprintArchetype archetype, params BlueprintComponent[] components) =>
            createProgression(name, guid, displayName, description, icon, group, Array.Empty<BlueprintCharacterClass>(),
                new BlueprintArchetype[] { archetype }, components);

        public BlueprintProgression createProgression(String name, String guid, String displayName, String description,
            Sprite icon,
            FeatureGroup group, BlueprintArchetype[] archetypes, params BlueprintComponent[] components) =>
            createProgression(name, guid, displayName, description, icon, group, Array.Empty<BlueprintCharacterClass>(),
                archetypes, components);
        
        private BlueprintProgression createProgression(String name, String guid, String displayName, String description, Sprite icon,
            FeatureGroup group, BlueprintCharacterClass[] classes, BlueprintArchetype[] archetypes, params BlueprintComponent[] components)
        {
            BlueprintProgression progression = _library.Create<BlueprintProgression>();
            blueprintProgression_set_AssetId(progression, guid);
            progression.name = name;
            progression.SetComponents(components);
            progression.Groups = new FeatureGroup[] { group };
            progression.SetNameDescriptionIcon(displayName, description, icon);
            progression.UIDeterminatorsGroup = Array.Empty<BlueprintFeatureBase>();
            progression.UIGroups = Array.Empty<UIGroup>();
            progression.Classes = classes;
            progression.Archetypes = archetypes;
            
            _library.Add(progression);

            return progression;
        }

        public BlueprintProgression createEmptyProgression()
        {
            return _library.Create<BlueprintProgression>();
        }
    }
}
