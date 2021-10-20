using System.Linq;
using Kingmaker.Blueprints.Classes.Selection;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class SelectionFromJson : JsonTransformation
    {
        private static readonly FeatureSelectionFactory _featureSelectionFactory = new FeatureSelectionFactory();

        public static BlueprintFeatureSelection GetFeatureSelection(FeatureSelection featureSelectionData)
        {
            _logger.Log($"Creating feature selection from JSON data {featureSelectionData.Name}");

            BlueprintFeatureSelection selection = _featureSelectionFactory.CreateFeatureSelection(
                featureSelectionData.Name, featureSelectionData.Guid, featureSelectionData.DisplayName, featureSelectionData.Description,
                EnumParser.parseFeatureGroup(featureSelectionData.FeatureGroup),
                featureSelectionData.Features
                    .Select(f =>
                    {
                        return _featuresRepository.GetFeature(_identifierLookup.lookupFeature(f));
                    })
                    .ToArray()
                );

            _logger.Log($"DONE: Creating buff from JSON data {featureSelectionData.Name}");
            IdentifierRegistry.INSTANCE.Register(selection);
            return selection;
        }
    }
}
