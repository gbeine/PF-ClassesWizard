using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using PF_Classes.JsonTypes;
using PF_Core;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class SpellbookFromJson
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly SpellbookFactory _spellbookFactory = new SpellbookFactory();

        public static BlueprintSpellbook GetSpellbook(Spellbook spellbookData, BlueprintCharacterClass characterClass)
        {
            _logger.Log($"Creating spellbook from JSON data {spellbookData.Guid}");

            BlueprintSpellsTable spellsKnown = null;
            if (spellbookData.SpellsKnown != null)
            {
                spellsKnown = _spellbookFactory.createSpellsTable(
                    spellbookData.SpellsKnown.Name, spellbookData.SpellsKnown.Guid, spellbookData.SpellsKnown.Table);
            }

            BlueprintSpellsTable spellsPerDay = _spellbookFactory.createSpellsTable(
                spellbookData.SpellsPerDay.Name, spellbookData.SpellsPerDay.Guid, spellbookData.SpellsPerDay.Table);

            BlueprintSpellList spellList = SpellListFromJson.GetSpellList(spellbookData.SpellList);

            BlueprintSpellbook spellbook = _spellbookFactory.CreateSpellbook(
                spellbookData.Name, spellbookData.Guid, characterClass,
                spellbookData.IsArcane, spellbookData.IsSpontaneous, spellbookData.CanCopyScrolls, spellbookData.AllSpellsKnown,
                EnumParser.parseStatType(spellbookData.CastingAttribute),
                EnumParser.parseCantripsType(spellbookData.Cantrips),
                spellsKnown, spellsPerDay, spellList);

            _logger.Log("DONE: Creating spellbook");
            return spellbook;
        }
    }
}
