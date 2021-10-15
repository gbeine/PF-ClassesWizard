using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using PF_Core.Facades;

namespace PF_Core.Repositories
{
    public class SpellbookRepository
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        private static readonly SpellbookRepository __instance = new SpellbookRepository();

        private SpellbookRepository() { }

        public static SpellbookRepository INSTANCE
        {
            get { return __instance;  }
        }

        public List<BlueprintAbility> AllSpells
        {
            get { return _library.GetAbilities().Where(s=> s.IsSpell).ToList(); }
        }

        public BlueprintSpellbook GetSpellbook(String assetId)
        {
            _logger.Debug($"Search for Spellbook {assetId}");
            return _library.GetSpellbook(assetId);
        }

        public BlueprintAbility GetSpell(String assetId)
        {
            _logger.Debug($"Search for Spell {assetId}");
            return _library.GetAbility(assetId);
        }
    }
}
