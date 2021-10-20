using System;
using Kingmaker.Blueprints.Classes;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations;

namespace PF_Classes
{
    public class FeatureLoader : Loader
    {
        private Feature _feature;

        public FeatureLoader(String filename) : base(filename) { }

        public override bool load()
        {
            _logger.Debug("Parsing feature");
            _feature = Deserialize();
            _logger.Log($"DONE: Parsing feature {_feature.Guid}");
            return true;
        }

        public BlueprintFeature Feature
        {
            get { return FeatureFromJson.GetFeature(_feature); }
        }

        private Feature Deserialize()
        {
            return new Feature(_jObject);
        }
    }
}
