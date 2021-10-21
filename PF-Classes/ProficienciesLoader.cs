using System;
using Kingmaker.Blueprints.Classes;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations;

namespace PF_Classes
{
    public class ProficienciesLoader : Loader
    {
        private Proficiencies _proficiencies;

        public ProficienciesLoader(String filename) : base(filename) { }

        public override bool load()
        {
            _logger.Debug("Parsing proficiencies");
            _proficiencies = Deserialize();
            _logger.Log($"DONE: Parsing proficiencies {_proficiencies.Guid}");
            return true;
        }

        public BlueprintFeature Proficiencies
        {
            get { return ProficienciesFromJson.GetProficiencies(_proficiencies); }
        }

        private Proficiencies Deserialize()
        {
            return new Proficiencies(_jObject);
        }
    }
}
