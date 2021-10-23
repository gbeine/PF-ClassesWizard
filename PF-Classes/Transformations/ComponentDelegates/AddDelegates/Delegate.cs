using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using PF_Classes.Identifier;
using PF_Core;
using PF_Core.Factories;
using PF_Core.Repositories;

namespace PF_Classes.Transformations.ComponentDelegates.AddDelegates
{
    public abstract class Delegate
    {
        protected static readonly Logger _logger = Logger.INSTANCE;
        protected static readonly ComponentFactory _componentFactory = ComponentFactory.INSTANCE;

        private  static readonly IdentifierLookup _identifierLookup = IdentifierLookup.INSTANCE;
        private  static readonly FeaturesRepository _featuresRepository = FeaturesRepository.INSTANCE;
        private static readonly SpellbookRepository _spellbookRepository = SpellbookRepository.INSTANCE;

        protected static BlueprintAbility getSpell(string value) =>
            _spellbookRepository.GetSpell(_identifierLookup.lookupSpell(value));
        protected static BlueprintSpellbook getSpellbook(string value) =>
            _spellbookRepository.GetSpellbook(_identifierLookup.lookupSpellbook(value));
        protected static BlueprintFeature getFeature(string value) =>
            _featuresRepository.GetFeature(_identifierLookup.lookupFeature(value));
    }
}
