using System;
using PF_Classes.Identifier;
using PF_Core.Repositories;
using UnityEngine;

namespace PF_Classes.Transformations
{
    public class SpriteLookup
    {
        private static readonly CharacterClassesRepository _characterClassesRepository = CharacterClassesRepository.INSTANCE;
        private static readonly FeaturesRepository _featuresRepository = FeaturesRepository.INSTANCE;
        private static readonly SpellbookRepository _spellbookRepository = SpellbookRepository.INSTANCE;
        private static readonly IdentifierLookup _identifierLookup = IdentifierLookup.INSTANCE;

        internal static Sprite lookupFor(String identifier)
        {
            if (identifier == null)
            {
                return null;
            }

            if (_identifierLookup.existsCharacterClass(identifier))
            {
                return _characterClassesRepository.GetCharacterClass(
                    _identifierLookup.lookupCharacterClass(identifier)
                    ).Icon;
            }

            if (_identifierLookup.existsFeature(identifier))
            {
                return _featuresRepository.GetFeature(
                    _identifierLookup.lookupFeature(identifier)
                    ).Icon;
            }

            if (_identifierLookup.existsSpell(identifier))
            {
                return _spellbookRepository.GetSpell(
                    _identifierLookup.lookupSpell(identifier)
                    ).Icon;
            }

            return null;
        }
    }
}