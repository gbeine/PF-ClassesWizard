using System;
using Kingmaker.Blueprints.Classes;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class FeatureFromJson : JsonTransformation
    {
        private static readonly FeatureFactory _featureFactory = new FeatureFactory();

        public static BlueprintFeature GetFeature(Feature featureData, BlueprintCharacterClass characterClass)
        {
            _logger.Log($"Creating feature from JSON data {featureData.Guid}");

            BlueprintFeature feature;

            feature = createFeature(featureData);

            foreach (var component in featureData.Components)
            {
                _logger.Debug($"Adding component {component.Type}");
                ComponentFromJson.AddComponent(feature, component, characterClass);
                _logger.Debug($"DONE: Adding component {component.Type}");
            }

            _logger.Log("DONE: Create feature");
            IdentifierRegistry.INSTANCE.Register(feature);
            return feature;
        }

        public static BlueprintFeature GetFeature(Feature featureData)
        {
            _logger.Log($"Creating feature from JSON data {featureData.Guid}");

            BlueprintFeature feature = createFeature(featureData);

            foreach (var component in featureData.Components)
            {
                _logger.Debug($"Adding component {component.Type}");
                ComponentFromJson.AddComponent(feature, component);
                _logger.Debug($"DONE: Adding component {component.Type}");
            }

            _logger.Log("DONE: Create feature");
            IdentifierRegistry.INSTANCE.Register(feature);
            return feature;
        }

        private static BlueprintFeature createFeature(Feature featureData)
        {
            _logger.Log($"Creating feature from JSON data {featureData.Guid}");

            BlueprintFeature feature;

            if (!String.Empty.Equals(featureData.From) && IdentifierLookup.INSTANCE.existsFeature(featureData.From))
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

            // feature.HideInUI = featureData.HideInUI;
            // feature.ReapplyOnLevelUp = featureData.ReapplyOnLevelUp;

            // TODO: remove components

            _logger.Log("DONE: Create feature");
            return feature;
        }
    }
}
