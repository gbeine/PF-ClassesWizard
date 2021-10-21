using System;
using Kingmaker.Blueprints.Classes.Spells;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations;

namespace PF_Classes
{
    public class SpellbookLoader : Loader
    {
        private Spellbook _spellbook;

        public SpellbookLoader(String filename) : base(filename) { }

        public override bool load()
        {
            _logger.Debug("Parsing spellbook");
            _spellbook = Deserialize();
            _logger.Log($"DONE: Parsing spellbook {_spellbook.Guid}");
            return true;
        }

        public BlueprintSpellbook Spellbook
        {
            get { return SpellbookFromJson.GetSpellbook(_spellbook); }
        }

        private Spellbook Deserialize()
        {
            return new Spellbook(_jObject);
        }
    }
}
