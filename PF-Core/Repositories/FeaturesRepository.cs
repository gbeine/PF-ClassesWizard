using System;
using System.Collections.Generic;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using PF_Core.Facades;

namespace PF_Core.Repositories
{
    public class FeaturesRepository
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        private static readonly FeaturesRepository __instance = new FeaturesRepository();

        private FeaturesRepository() { }

        public static FeaturesRepository INSTANCE
        {
            get { return __instance;  }
        }

        public List<BlueprintFeature> AllFeatures
        {
            get { return _library.GetFeatures(); }
        }

        public BlueprintFeatureSelection GetFeatureSelection(String assetId)
        {
            _logger.Debug($"Search for FeatureSelection {assetId}");
            return _library.GetFeatureSelection(assetId);
        }

        public BlueprintFeature GetFeature(String assetId)
        {
            _logger.Debug($"Search for Feature {assetId}");
            return _library.GetFeature(assetId);
        }
    }
}
