using System;
using Kingmaker.Blueprints.Classes.Selection;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations;

namespace PF_Classes
{
    public class SelectionLoader : Loader
    {
        private FeatureSelection _selection;

        public SelectionLoader(String filename) : base(filename) { }

        public override bool load()
        {
            _logger.Debug("Parsing selection");
            _selection = Deserialize();
            _logger.Log($"DONE: Parsing selection {_selection.Guid}");
            return true;
        }

        public BlueprintFeatureSelection Selection
        {
            get { return SelectionFromJson.GetFeatureSelection(_selection); }
        }

        private FeatureSelection Deserialize()
        {
            return new FeatureSelection(_jObject);
        }
    }
}
