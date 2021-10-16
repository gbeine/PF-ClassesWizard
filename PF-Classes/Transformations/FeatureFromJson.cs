using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core;
using PF_Core.Extensions;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class FeatureFromJson
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly FeatureFactory _featureFactory = new FeatureFactory();
        private static readonly ProgressionFactory _progressionFactory = new ProgressionFactory();

        public static BlueprintFeature GetFeature(Feature featureData, BlueprintCharacterClass characterClass)
        {
            _logger.Log($"Creating feature from JSON data {featureData.Guid}");

            BlueprintFeature feature;

            if (featureData.IsProgression)
            {
                if (featureData.Progression.HasLevelEntries)
                {
                    feature = _progressionFactory.CreateProgression(
                        featureData.Name, featureData.Guid, featureData.DisplayName, featureData.Description,
                        SpriteLookup.lookupFor(featureData.Icon),
                        EnumParser.parseFeatureGroup(featureData.FeatureGroup),
                        ProgressionFromJson.getUIDeterminatorsGroup(featureData.Progression).ToArray(),
                        ProgressionFromJson.getUIGroups(featureData.Progression).ToArray(),
                        ProgressionFromJson.getLevelEntries(featureData.Progression).ToArray());
                }
                else
                {
                    feature = _progressionFactory.CreateProgression(
                        featureData.Name, featureData.Guid, featureData.DisplayName, featureData.Description,
                        SpriteLookup.lookupFor(featureData.Icon),
                        EnumParser.parseFeatureGroup(featureData.FeatureGroup)
                    );
                }

            }
            else if (!String.Empty.Equals(featureData.From) && IdentifierLookup.INSTANCE.existsFeature(featureData.From))
            {
                feature = _featureFactory.CreateFeatureFrom(featureData.Name, featureData.Guid,
                    IdentifierLookup.INSTANCE.lookupFeature(featureData.From));
            }
            else if (!String.Empty.Equals(featureData.Icon))
            {
                feature = _featureFactory.CreateFeature(featureData.Name, featureData.Guid, featureData.DisplayName,
                    featureData.Description, SpriteLookup.lookupFor(featureData.Icon));
            }
            else
            {
                feature = _featureFactory.CreateFeature(featureData.Name, featureData.Guid, featureData.DisplayName,
                    featureData.Description);
            }

            feature.ReapplyOnLevelUp = featureData.ReapplyOnLevelUp;

            List<BlueprintComponent> components = new List<BlueprintComponent>();
            foreach (var component in featureData.Components)
            {
                components.Add(ComponentFromJson.GetComponent(component, characterClass));
            }

            feature.SetComponents(components.ToArray());

            _logger.Log("DONE: Create feature");
            IdentifierRegistry.INSTANCE.Register(feature);
            return feature;
        }
    }
}
