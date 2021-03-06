using PF_Classes.Identifier;
using PF_Core;
using PF_Core.Repositories;

namespace PF_Classes.Transformations
{
    public abstract class JsonTransformation
    {
        protected static readonly Logger _logger = Logger.INSTANCE;

        protected static readonly IdentifierLookup _identifierLookup = IdentifierLookup.INSTANCE;
        protected static readonly IdentifierRegistry _identifierRegistry = IdentifierRegistry.INSTANCE;

        protected static readonly AreaEffectRepository _areaEffectRepository = AreaEffectRepository.INSTANCE;
        protected static readonly BuffRepository _buffRepository = BuffRepository.INSTANCE;
        protected static readonly CharacterClassesRepository _characterClassesRepository = CharacterClassesRepository.INSTANCE;
        protected static readonly FeaturesRepository _featuresRepository = FeaturesRepository.INSTANCE;
        protected static readonly ItemRepository _itemRepository = ItemRepository.INSTANCE;
        protected static readonly SpellbookRepository _spellbookRepository = SpellbookRepository.INSTANCE;
        protected static readonly StatProgressionRepository _statProgressionRepository = StatProgressionRepository.INSTANCE;
    }
}
