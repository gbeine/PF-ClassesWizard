using System;
using System.IO;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Newtonsoft.Json.Linq;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations;
using PF_Core;

namespace PF_Classes
{
    public class SpellLoader : Loader
    {
        private Spell _spell;

        public SpellLoader(String filename) : base(filename) { }

        public override bool load()
        {
            _logger.Debug("Parsing spell");
            _spell = Deserialize();
            _logger.Log($"DONE: Parsing buff {_spell.Guid}");
            return true;
        }

        public BlueprintAbility Spell
        {
            get { return SpellFromJson.GetSpell(_spell); }
        }

        private Spell Deserialize()
        {
            return new Spell(_jObject);
        }
    }
}
