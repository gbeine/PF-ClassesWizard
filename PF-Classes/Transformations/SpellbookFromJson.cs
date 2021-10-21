using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class SpellbookFromJson : JsonTransformation
    {
        private static readonly SpellbookFactory _spellbookFactory = SpellbookFactory.INSTANCE;
        private static readonly LocalizationFactory _localizationFactory = new LocalizationFactory();

        public static BlueprintSpellbook GetSpellbook(Spellbook spellbookData)
        {
            _logger.Log($"Creating spellbook from JSON data {spellbookData.Guid}");

            BlueprintSpellbook spellbook = !string.Empty.Equals(spellbookData.From)
                ? _spellbookFactory.CreateSpellbookFrom(spellbookData.Name, spellbookData.Guid, _identifierLookup.lookupSpellbook(spellbookData.From))
                : _spellbookFactory.CreateSpellbook(spellbookData.Name, spellbookData.Guid);

            BlueprintCharacterClass characterClass = !string.Empty.Equals(spellbookData.CharacterClass)
                ? _characterClassesRepository.GetCharacterClass(
                    _identifierLookup.lookupCharacterClass(spellbookData.CharacterClass))
                : null;

            if (spellbookData.IsArcane.HasValue)
                spellbook.IsArcane = spellbookData.IsArcane.Value;
            if (spellbookData.IsSpontaneous.HasValue)
                spellbook.Spontaneous = spellbookData.IsSpontaneous.Value;
            if (spellbookData.CanCopyScrolls.HasValue)
                spellbook.CanCopyScrolls = spellbookData.CanCopyScrolls.Value;
            if (spellbookData.AllSpellsKnown.HasValue)
                spellbook.AllSpellsKnown = spellbookData.AllSpellsKnown.Value;

            if (!string.Empty.Equals(spellbookData.DisplayName))
                spellbook.Name = _localizationFactory.CreateString($"{spellbookData.Name}.Name", spellbookData.DisplayName);;

            if (!string.Empty.Equals(spellbookData.CastingAttribute))
                spellbook.CastingAttribute = EnumParser.parseStatType(spellbookData.CastingAttribute);

            if (!string.Empty.Equals(spellbookData.Cantrips))
                spellbook.CantripsType = EnumParser.parseCantripsType(spellbookData.Cantrips);

            if (spellbookData.HasSpellsPerDayDefinition)
                spellbook.SpellsPerDay = SpellsTableFromJson.GetSpellsTable(spellbookData.SpellsPerDayDefinition);
            else if (!String.Empty.Equals(spellbookData.SpellsPerDay))
                spellbook.SpellsPerDay = getSpellbook(spellbookData.SpellsPerDay).SpellsPerDay;

            if (spellbookData.HasSpellListDefinition)
                spellbook.SpellList = SpellListFromJson.GetSpellList(spellbookData.SpellListDefinition);
            else if (!String.Empty.Equals(spellbookData.SpellList))
                spellbook.SpellList = getSpellbook(spellbookData.SpellList).SpellList;

            // SpellsKnow is optional for a spell book
            if (spellbookData.HasSpellsKnownDefinition)
                spellbook.SpellsKnown = SpellsTableFromJson.GetSpellsTable(spellbookData.SpellsKnownDefinition);
            else if (!String.Empty.Equals(spellbookData.SpellsKnown))
                spellbook.SpellsKnown = getSpellbook(spellbookData.SpellsKnown).SpellsKnown;

            if (characterClass != null)
            {
                _logger.Debug("Adding spellbook to character class");
                characterClass.Spellbook = spellbook;
                spellbook.CharacterClass = characterClass;
            }

            _logger.Log("DONE: Creating spellbook");
            _identifierRegistry.Register(spellbook);
            return spellbook;
        }

        private static BlueprintSpellbook getSpellbook(String value) =>
            _spellbookRepository.GetSpellbook(
                _identifierLookup.lookupSpellbook(value));
    }
}
