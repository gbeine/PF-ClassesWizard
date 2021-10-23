using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using PF_Classes.Identifier;
using PF_Core;
using PF_Core.Factories;
using PF_Core.Repositories;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public abstract class AbstractComponentDelegate
    {
        protected static readonly Logger _logger = Logger.INSTANCE;
        protected static readonly ComponentFactory _componentFactory = ComponentFactory.INSTANCE;

        private static readonly IdentifierLookup _identifierLookup = IdentifierLookup.INSTANCE;
        private static readonly AreaEffectRepository _areaEffectRepository = AreaEffectRepository.INSTANCE;
        private static readonly BuffRepository _buffRepository = BuffRepository.INSTANCE;
        private static readonly ItemRepository _itemRepository = ItemRepository.INSTANCE;
        private static readonly FeaturesRepository _featuresRepository = FeaturesRepository.INSTANCE;
        private static readonly SpellbookRepository _spellbookRepository = SpellbookRepository.INSTANCE;

        protected static bool featureExists(string value) =>
            _identifierLookup.existsFeature(value);

        protected static bool spellbookExists(string value) =>
            _identifierLookup.existsSpellbook(value);

        protected static BlueprintAbilityAreaEffect getAreaEffect(string value) =>
            _areaEffectRepository.GetAreaEffect(_identifierLookup.lookupAbilityAreaEffect(value));
        protected static BlueprintBuff getBuff(string value) =>
            _buffRepository.GetBuff(_identifierLookup.lookupBuff(value));
        protected static BlueprintFeature getFeature(string value) =>
            _featuresRepository.GetFeature(_identifierLookup.lookupFeature(value));
        protected static BlueprintAbility getSpell(string value) =>
            _spellbookRepository.GetSpell(_identifierLookup.lookupSpell(value));
        protected static BlueprintSpellbook getSpellbook(string value) =>
            _spellbookRepository.GetSpellbook(_identifierLookup.lookupSpellbook(value));
        protected static BlueprintItemWeapon getWeapon(string value) =>
            _itemRepository.GetWeapon(_identifierLookup.lookupItem(value));
    }
}
