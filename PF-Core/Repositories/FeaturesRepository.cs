using System;
using System.Collections.Generic;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities.Blueprints;
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
        
        public BlueprintFeature GetFeature(String assetId)
        {
            _logger.Debug(String.Format("Search for Feature {0}", assetId));
            return _library.GetFeature(assetId);
        }
    }
}
