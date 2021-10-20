using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using PF_Core.Extensions;
using PF_Core.Facades;

namespace PF_Core.Factories
{
    public class FeatureSelectionFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        public BlueprintFeatureSelection CreateFeatureSelection(String name, String guid)
        {
            _logger.Debug($"Create feature selection {name} with id {guid}");

            BlueprintFeatureSelection selection = _library.Create<BlueprintFeatureSelection>();
            selection.SetAssetId(guid);
            selection.name = name;

            _library.Add(selection);

            _logger.Debug($"DONE: Create feature selection {name} with id {guid}");
            return selection;
        }

        public BlueprintFeatureSelection CreateFeatureSelection(String name, String guid, String displayName, String description, params BlueprintFeature[] features) =>
            CreateFeatureSelection(name, guid, displayName, description, FeatureGroup.None, features);

        public BlueprintFeatureSelection CreateFeatureSelection(String name, String guid, String displayName, String description, FeatureGroup group, params BlueprintFeature[] features)
        {
            _logger.Debug($"Create feature selection {name} with id {guid}");

            BlueprintFeatureSelection selection = CreateFeatureSelection(name, guid);
            selection.SetNameDescription(displayName, description);
            selection.Group = group;
            selection.AllFeatures = features;

            _logger.Debug($"DONE: Create feature selection {name} with id {guid}");
            return selection;
        }

    }
}
