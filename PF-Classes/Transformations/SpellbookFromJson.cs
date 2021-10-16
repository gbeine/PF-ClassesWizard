using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core;
using PF_Core.Factories;
using PF_Core.Repositories;

namespace PF_Classes.Transformations
{
    public class SpellbookFromJson
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly SpellbookRepository _spellbookRepository = SpellbookRepository.INSTANCE;

        private static readonly SpellbookFactory _spellbookFactory = new SpellbookFactory();

        public static BlueprintSpellbook GetSpellbook(Spellbook spellbookData, BlueprintCharacterClass characterClass)
        {
            _logger.Log($"Creating spellbook from JSON data {spellbookData.Guid}");

            // SpellsKnow is optional for a spell book
            BlueprintSpellsTable spellsKnown = null;
            if (spellbookData.HasSpellsKnownDefinition)
            {
                spellsKnown = SpellsTableFromJson.GetSpellsTable(spellbookData.SpellsKnownDefinition);
            }
            else if (!String.Empty.Equals(spellbookData.SpellsKnown))
            {
                spellsKnown = getSpellbook(spellbookData.SpellsKnown).SpellsKnown;
            }

            BlueprintSpellsTable spellsPerDay = spellbookData.HasSpellsPerDayDefinition
                ? SpellsTableFromJson.GetSpellsTable(spellbookData.SpellsPerDayDefinition)
                : getSpellbook(spellbookData.SpellsPerDay).SpellsPerDay;

            BlueprintSpellList spellList = spellbookData.HasSpellListDefinition
                ? SpellListFromJson.GetSpellList(spellbookData.SpellListDefinition)
                : getSpellbook(spellbookData.SpellList).SpellList;

            BlueprintSpellbook spellbook = _spellbookFactory.CreateSpellbook(
                spellbookData.Name, spellbookData.Guid, characterClass,
                spellbookData.IsArcane, spellbookData.IsSpontaneous, spellbookData.CanCopyScrolls, spellbookData.AllSpellsKnown,
                spellbookData.SpellsPerLevel, spellbookData.CasterLevelModifier,
                EnumParser.parseStatType(spellbookData.CastingAttribute),
                EnumParser.parseCantripsType(spellbookData.Cantrips),
                spellsKnown, spellsPerDay, spellList);

            _logger.Log("DONE: Creating spellbook");
            IdentifierRegistry.INSTANCE.Register(spellbook);
            return spellbook;
        }

        private static BlueprintSpellbook getSpellbook(String value) =>
            _spellbookRepository.GetSpellbook(
                IdentifierLookup.INSTANCE.lookupSpellbook(value));
    }
}
