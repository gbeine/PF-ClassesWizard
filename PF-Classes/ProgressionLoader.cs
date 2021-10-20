using System;
using Kingmaker.Blueprints.Classes;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations;

namespace PF_Classes
{
    public class ProgressionLoader : Loader
    {
        private Progression _progression;

        public ProgressionLoader(String filename) : base(filename) { }

        public override bool load()
        {
            _logger.Debug("Parsing progression");
            _progression = Deserialize();
            _logger.Log($"DONE: Loading progression {_progression.Guid}");
            return true;
        }

        public BlueprintProgression Progression
        {
            get { return ProgressionFromJson.GetProgression(_progression); }
        }

        private Progression Deserialize()
        {
            return new Progression(_jObject);
        }
    }
}
