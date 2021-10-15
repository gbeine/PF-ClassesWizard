using System;
using System.Collections.Generic;
using Kingmaker.Blueprints.Classes;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core;
using PF_Core.Factories;
using PF_Core.Repositories;
using UnityModManagerNet;

namespace PF_Classes.Transformations
{
    public class ProgressionFromJson
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly FeaturesRepository _featuresRepository = FeaturesRepository.INSTANCE;

        private static readonly ProgressionFactory _progressionFactory = new ProgressionFactory();
        private static readonly UIGroupFactory _uiGroupFactory = new UIGroupFactory();
        private static readonly LevelEntryFactory _levelEntryFactory = new LevelEntryFactory();

        public static BlueprintProgression GetProgression(Progression progressionData,
            BlueprintCharacterClass characterClass, List<BlueprintFeature> startFeatures) =>
            GetProgression(progressionData, characterClass, startFeatures.ToArray());

        public static BlueprintProgression GetProgression(Progression progressionData, BlueprintCharacterClass characterClass, params BlueprintFeature[] startFeatures)
        {
            _logger.Log($"Creating progression from JSON data {progressionData.Guid}");

            BlueprintProgression progression = _progressionFactory.CreateProgression(
                progressionData.Name, progressionData.Guid, characterClass,
                getUIDeterminatorsGroup(progressionData, startFeatures).ToArray(),
                getUIGroups(progressionData).ToArray(),
                getLevelEntries(progressionData, startFeatures).ToArray());

            _logger.Log("DONE: Creating progression");
            IdentifierRegistry.INSTANCE.Register(progression);
            return progression;
        }

        private static List<BlueprintFeatureBase> getUIDeterminatorsGroup(Progression progressionData) =>
            getUIDeterminatorsGroup(progressionData, Array.Empty<BlueprintFeature>());

        private static List<BlueprintFeatureBase> getUIDeterminatorsGroup(Progression progressionData, BlueprintFeature[] startFeatures)
        {
            _logger.Log("Creating UIDeterminatorsGroup");
            List<BlueprintFeatureBase> uiDeterminatorsGroup = new List<BlueprintFeatureBase>();
            foreach (var feature in progressionData.UiDeterminatorsGroup)
            {
                uiDeterminatorsGroup.Add(getUiDeterminatorGroupEntry(feature));
            }

            // add start features for class
            uiDeterminatorsGroup.AddRange(startFeatures);

            _logger.Log("DONE: Creating UIDeterminatorsGroup");
            return uiDeterminatorsGroup;
        }

        private static List<UIGroup> getUIGroups(Progression progressionData)
        {
            _logger.Log("Creating UIGroups");
            List<UIGroup> uiGroups = new List<UIGroup>();
            foreach (var group in progressionData.UiGroups)
            {
                List<BlueprintFeature> features = new List<BlueprintFeature>();
                foreach (var feature in @group)
                {
                    features.Add(getUiGroupEntry(feature));
                }

                uiGroups.Add(_uiGroupFactory.CreateUIGroup(features.ToArray()));
            }

            _logger.Log("DONE: Creating UIGroups");
            return uiGroups;
        }

        private static List<LevelEntry> getLevelEntries(Progression progressionData) =>
            getLevelEntries(progressionData, Array.Empty<BlueprintFeature>());

        private static List<LevelEntry> getLevelEntries(Progression progressionData, BlueprintFeature[] startFeatures)
        {
            _logger.Log("Creating LevelEntries");
            List<LevelEntry> levelEntries = new List<LevelEntry>();
            int level = 1;
            foreach (var levelEntry in progressionData.LevelEntries)
            {
                _logger.Log($"Creating LevelEntries for level {level}");
                List<BlueprintFeature> features = new List<BlueprintFeature>();
                foreach (var feature in levelEntry)
                {
                    features.Add(getLevelEntryFeature(feature));
                }

                levelEntries.Add(_levelEntryFactory.CreateLevelEntry(level, features));
                _logger.Log($"Done with level {level}");
                level++;
            }

            // add start features for class
            levelEntries[0].Features.AddRange(startFeatures);

            _logger.Log("DONE: Creating LevelEntries");
            return levelEntries;
        }

        private static BlueprintFeatureBase getUiDeterminatorGroupEntry(String value) =>
            _featuresRepository.GetFeature(
                IdentifierLookup.INSTANCE.lookupFeature(value));

        private static BlueprintFeature getUiGroupEntry(String value) =>
            _featuresRepository.GetFeature(
                IdentifierLookup.INSTANCE.lookupFeature(value));

        private static BlueprintFeature getLevelEntryFeature(String value) =>
            _featuresRepository.GetFeature(
                IdentifierLookup.INSTANCE.lookupFeature(value));
    }
}
