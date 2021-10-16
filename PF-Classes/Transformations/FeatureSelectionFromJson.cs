using System.Collections.Generic;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core;
using PF_Core.Factories;
using PF_Core.Repositories;

namespace PF_Classes.Transformations
{
    public class FeatureSelectionFromJson
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly FeaturesRepository _featuresRepository = FeaturesRepository.INSTANCE;

        private static readonly FeatureSelectionFactory _featureSelectionFactory = new FeatureSelectionFactory();

        public static BlueprintFeatureSelection GetFeatureSelection(FeatureSelection featureSelectionData)
        {
            _logger.Log($"Creating feature selection from JSON data {featureSelectionData.Guid}");

            List<BlueprintFeature> features = new List<BlueprintFeature>();
            foreach (var feature in featureSelectionData.Features)
            {
                features.Add(_featuresRepository.GetFeature(IdentifierLookup.INSTANCE.lookupFeature(feature)));
            }

            BlueprintFeatureSelection selection = _featureSelectionFactory.CreateFeatureSelection(
                    featureSelectionData.Name, featureSelectionData.Guid, featureSelectionData.DisplayName,
                    featureSelectionData.Description, EnumParser.parseFeatureGroup(featureSelectionData.FeatureGroup), features.ToArray());

            _logger.Log("DONE: Create feature selection");
            IdentifierRegistry.INSTANCE.Register(selection);
            return selection;
        }
    }
}
