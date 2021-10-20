using System;
using PF_Classes.Identifier;
using PF_Core;
using PF_Core.Repositories;
using UnityEngine;

namespace PF_Classes.Transformations
{
    public class SpriteLookup
    {
        private static readonly IdentifierLookup _identifierLookup = IdentifierLookup.INSTANCE;

        private static readonly CharacterClassesRepository _characterClassesRepository = CharacterClassesRepository.INSTANCE;
        private static readonly FeaturesRepository _featuresRepository = FeaturesRepository.INSTANCE;
        private static readonly ProgressionRepository _progressionRepository = ProgressionRepository.INSTANCE;
        private static readonly SpellbookRepository _spellbookRepository = SpellbookRepository.INSTANCE;

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

            if (_identifierLookup.existsProgression(identifier))
            {
                return _progressionRepository.GetProgression(
                    _identifierLookup.lookupProgression(identifier)
                ).Icon;
            }

            if (identifier.StartsWith("icon:"))
            {
                String fileName = identifier.Replace("icon:", "");
                if (Image2Sprite.Exists(fileName))
                {
                    return Image2Sprite.Create(fileName);
                }
            }

            return null;
        }
    }
}
