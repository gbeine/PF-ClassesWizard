using System;
using System.Collections.Generic;
using Kingmaker.Blueprints.Classes;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class ProgressionFromJson : JsonTransformation
    {
        private static readonly ProgressionFactory _progressionFactory = new ProgressionFactory();
        private static readonly UIGroupFactory _uiGroupFactory = new UIGroupFactory();
        private static readonly LevelEntryFactory _levelEntryFactory = new LevelEntryFactory();

        public static BlueprintProgression GetProgression(Progression progressionData)
        {
            _logger.Log($"Creating progression from JSON data {progressionData.Guid}");

            BlueprintProgression progression;
            if (!String.Empty.Equals(progressionData.From))
            {
                progression = _progressionFactory.CreateProgressionFrom(progressionData.Name, progressionData.Guid, progressionData.From);
            }
            else
            {
                progression = _progressionFactory.CreateProgression(progressionData.Name, progressionData.Guid, progressionData.DisplayName, progressionData.Description, null);
            }

            //     getUIDeterminatorsGroup(progressionData),
            //     getUIGroups(progressionData).ToArray(),
            //     getLevelEntries(progressionData).ToArray());
            //
            // feature = _progressionFactory.CreateProgression(
            //     featureData.Name, featureData.Guid, featureData.DisplayName, featureData.Description,
            //     SpriteLookup.lookupFor(featureData.Icon),
            //     EnumParser.parseFeatureGroup(featureData.FeatureGroup), characterClass,
            //     ProgressionFromJson.getUIDeterminatorsGroup(featureData.Progression).ToArray(),
            //     ProgressionFromJson.getUIGroups(featureData.Progression).ToArray(),
            //     ProgressionFromJson.getLevelEntries(featureData.Progression).ToArray());
            // if (featureData.IsProgression && !String.Empty.Equals(featureData.From) && IdentifierLookup.INSTANCE.existsFeature(featureData.From))
            // {
            //     // TODO: implement
            //     feature = null;
            // }
            // else if (featureData.IsProgression)
            // {
            //     feature = _progressionFactory.CreateProgression(
            //         featureData.Name, featureData.Guid, featureData.DisplayName, featureData.Description,
            //         SpriteLookup.lookupFor(featureData.Icon),
            //         EnumParser.parseFeatureGroup(featureData.FeatureGroup)
            //     );
            // }
            //

            _logger.Log("DONE: Creating progression");
            IdentifierRegistry.INSTANCE.Register(progression);
            return progression;
        }

        private static List<BlueprintFeatureBase> getUIDeterminatorsGroup(Progression progressionData)
        {
            _logger.Log("Creating UIDeterminatorsGroup");
            List<BlueprintFeatureBase> uiDeterminatorsGroup = new List<BlueprintFeatureBase>();
            foreach (var feature in progressionData.UiDeterminatorsGroup)
            {
                uiDeterminatorsGroup.Add(getUiDeterminatorGroupEntry(feature));
            }

            _logger.Log("DONE: Creating UIDeterminatorsGroup");
            return uiDeterminatorsGroup;
        }

        internal static List<UIGroup> getUIGroups(Progression progressionData)
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

        private static List<LevelEntry> getLevelEntries(Progression progressionData)
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
