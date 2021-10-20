using System;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using PF_Core.Extensions;
using PF_Core.Facades;

namespace PF_Core.Factories
{
    public class SpellFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        public BlueprintAbility CreateSpellFrom(String name, String guid, String fromAssetId)
        {
            _logger.Debug($"Create spell {name} with id {guid} based on {fromAssetId}");

            BlueprintAbility original = _library.GetAbility(fromAssetId);
            BlueprintAbility spell = UnityEngine.Object.Instantiate(original);
            spell.SetAssetId(guid);
            spell.name = name;

            _library.Add(spell);

            _logger.Debug($"DONE: Create spell {name} with id {guid} based on {fromAssetId}");
            return spell;
        }

        public BlueprintAbility CreateSpell(String name, String guid)
        {
            _logger.Debug($"Create spell {name} with id {guid}");

            BlueprintAbility spell = _library.Create<BlueprintAbility>();
            spell.SetAssetId(guid);
            spell.name = name;

            _library.Add(spell);

            _logger.Debug($"DONE: Create spell {name} with id {guid}");
            return spell;
        }
    }
}
