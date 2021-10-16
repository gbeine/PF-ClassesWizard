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

        public BlueprintProgression CreateProgression(String name, String guid, BlueprintCharacterClass characterClass,
            LevelEntry[] levelEntries, params BlueprintComponent[] components) =>
            CreateProgression(name, guid, characterClass.Name, characterClass.Description, characterClass.Icon,
                FeatureGroup.None, characterClass, Array.Empty<BlueprintFeatureBase>(),
                Array.Empty<UIGroup>(), levelEntries, components);

        public BlueprintProgression CreateProgression(String name, String guid, String displayName, String description,
            Sprite icon, FeatureGroup group, params BlueprintComponent[] components) =>
            CreateProgression(name, guid, displayName, description, icon, group,
                Array.Empty<BlueprintFeatureBase>(), Array.Empty<UIGroup>(), Array.Empty<LevelEntry>(), components);

        public BlueprintProgression CreateProgression(String name, String guid, BlueprintCharacterClass characterClass,
            BlueprintFeatureBase[] uiDeterminatorsGroup, UIGroup[] uiGroups, LevelEntry[] levelEntries,
            params BlueprintComponent[] components) =>
            CreateProgression(name, guid, characterClass.Name, characterClass.Description, characterClass.Icon,
                FeatureGroup.None, characterClass, uiDeterminatorsGroup, uiGroups, levelEntries, components);

        public BlueprintProgression CreateProgression(String name, String guid, String displayName, String description,
            Sprite icon, FeatureGroup group, BlueprintFeatureBase[] uiDeterminatorsGroup,
            UIGroup[] uiGroups, LevelEntry[] levelEntries, params BlueprintComponent[] components) =>
            CreateProgression(name, guid, displayName, description, icon, group, Array.Empty<BlueprintCharacterClass>(),
                Array.Empty<BlueprintArchetype>(), uiDeterminatorsGroup, uiGroups, levelEntries, components);

        public BlueprintProgression CreateProgression(String name, String guid, String displayName, String description,
            Sprite icon, FeatureGroup group, BlueprintCharacterClass characterClass, BlueprintFeatureBase[] uiDeterminatorsGroup,
            UIGroup[] uiGroups, LevelEntry[] levelEntries, params BlueprintComponent[] components) =>
            CreateProgression(name, guid, displayName, description, icon, group, new BlueprintCharacterClass[] { characterClass },
                Array.Empty<BlueprintArchetype>(), uiDeterminatorsGroup, uiGroups, levelEntries, components);

        public BlueprintProgression CreateProgression(String name, String guid, String displayName, String description,
            Sprite icon, FeatureGroup group, BlueprintCharacterClass[] classes, BlueprintFeatureBase[] uiDeterminatorsGroup,
            UIGroup[] uiGroups, LevelEntry[] levelEntries, params BlueprintComponent[] components) =>
            CreateProgression(name, guid, displayName, description, icon, group, classes,
                Array.Empty<BlueprintArchetype>(), uiDeterminatorsGroup, uiGroups, levelEntries, components);

        public BlueprintProgression CreateProgression(String name, String guid, String displayName, String description,
            Sprite icon, FeatureGroup group, BlueprintArchetype archetype, BlueprintFeatureBase[] uiDeterminatorsGroup,
            UIGroup[] uiGroups, LevelEntry[] levelEntries, params BlueprintComponent[] components) =>
            CreateProgression(name, guid, displayName, description, icon, group, Array.Empty<BlueprintCharacterClass>(),
                new BlueprintArchetype[] { archetype }, uiDeterminatorsGroup, uiGroups, levelEntries, components);

        public BlueprintProgression CreateProgression(String name, String guid, String displayName, String description,
            Sprite icon, FeatureGroup group, BlueprintArchetype[] archetypes, BlueprintFeatureBase[] uiDeterminatorsGroup,
            UIGroup[] uiGroups, LevelEntry[] levelEntries, params BlueprintComponent[] components) =>
            CreateProgression(name, guid, displayName, description, icon, group, Array.Empty<BlueprintCharacterClass>(),
                archetypes, uiDeterminatorsGroup, uiGroups, levelEntries, components);

        private BlueprintProgression CreateProgression(String name, String guid, String displayName, String description, Sprite icon,
            FeatureGroup group, BlueprintCharacterClass[] classes, BlueprintArchetype[] archetypes,
            BlueprintFeatureBase[] uiDeterminatorsGroup, UIGroup[] uiGroups, LevelEntry[] levelEntries , params BlueprintComponent[] components)
        {
            _logger.Debug($"Create progession {name} with id {guid}");

            BlueprintProgression progression = _library.Create<BlueprintProgression>();
            blueprintProgression_set_AssetId(progression, guid);
            progression.name = name;
            progression.SetNameDescriptionIcon(displayName, description, icon);
            progression.SetComponents(components);
            progression.Groups = new FeatureGroup[] { group };
            progression.Classes = classes;
            progression.Archetypes = archetypes;
            progression.UIDeterminatorsGroup = uiDeterminatorsGroup;
            progression.UIGroups = uiGroups;
            progression.LevelEntries = levelEntries;

            _library.Add(progression);

            _logger.Debug($"DONE: Create progession {name} with id {guid}");
            return progression;
        }

        public BlueprintProgression CreateEmptyProgression()
        {
            _logger.Debug("Create emptu=y progession");

            return _library.Create<BlueprintProgression>();
        }
    }
}
