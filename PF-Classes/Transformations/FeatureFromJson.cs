using Kingmaker.Blueprints.Classes;
using PF_Classes.JsonTypes;
using PF_Core.Extensions;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class FeatureFromJson : JsonTransformation
    {
        private static readonly FeatureFactory _featureFactory = new FeatureFactory();

        public static BlueprintFeature GetFeature(Feature featureData)
        {
            _logger.Log($"Creating feature from JSON data {featureData.Guid}");

            BlueprintFeature feature = !string.Empty.Equals(featureData.From)
                ? _featureFactory.CreateFeatureFrom(featureData.Name, featureData.Guid,
                    _identifierLookup.lookupFeature(featureData.From))
                : _featureFactory.CreateFeature(featureData.Name, featureData.Guid);
            BlueprintCharacterClass characterClass = !string.Empty.Equals(featureData.Class)
                ? _characterClassesRepository.GetCharacterClass(
                    _identifierLookup.lookupCharacterClass(featureData.Class))
                : null;

            SetValuesFromData(feature, featureData, characterClass);

            feature.HideInUI = featureData.HideInUI.HasValue && featureData.HideInUI.Value;
            feature.ReapplyOnLevelUp = featureData.ReapplyOnLevelUp.HasValue && featureData.ReapplyOnLevelUp.Value;

            _logger.Log("DONE: Create feature");
            _identifierRegistry.Register(feature);
            return feature;
        }

        internal static void SetValuesFromData(BlueprintFeature feature, Feature featureData, BlueprintCharacterClass characterClass)
        {
            _logger.Log("Setting feature data");

            if (!string.Empty.Equals(featureData.DisplayName))
                feature.SetName(featureData.DisplayName);
            if (!string.Empty.Equals(featureData.Description))
                feature.SetDescription(featureData.Description);
            if (!string.Empty.Equals(featureData.Icon))
                feature.SetIcon(SpriteLookup.lookupFor(featureData.Icon));

            if (!string.Empty.Equals(featureData.FeatureGroup))
                feature.Groups = new[] { EnumParser.parseFeatureGroup(featureData.FeatureGroup) };

            ComponentFromJson.ProcessComponents(feature, featureData, characterClass);

            _logger.Log("DONE: Setting feature data");
        }
    }
}
