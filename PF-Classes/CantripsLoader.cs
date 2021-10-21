using System;
using Kingmaker.Blueprints.Classes;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations;

namespace PF_Classes
{
    public class CantripsLoader : Loader
    {
        private Cantrips _cantrips;

        public CantripsLoader(String filename) : base(filename) { }

        public override bool load()
        {
            _logger.Debug("Parsing cantrips");
            _cantrips = Deserialize();
            _logger.Log($"DONE: Parsing cantrips {_cantrips.Guid}");
            return true;
        }

        public BlueprintFeature Cantrips
        {
            get { return CantripsFromJson.GetCantrips(_cantrips); }
        }

        private Cantrips Deserialize()
        {
            return new Cantrips(_jObject);
        }
    }
}
