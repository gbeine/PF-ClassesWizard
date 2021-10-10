using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using PF_Core.Facades;

namespace PF_Core.Repositories
{
    public class SpellRepository
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;
        private static readonly SpellRepository __instance = new SpellRepository();
        
        private SpellRepository() { }

        public static SpellRepository INSTANCE
        {
            get { return __instance;  }
        }
        
        public List<BlueprintAbility> AllSpells
        {
            get { return _library.GetAbilities().Where(s=> s.IsSpell).ToList(); }
        }
        
        public BlueprintAbility GetSpell(String assetId)
        {
            _logger.Debug(String.Format("Search for Spell {0}", assetId));
            return _library.GetAbility(assetId);
        }

    }
}
