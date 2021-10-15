using Kingmaker.Blueprints.Classes.Spells;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class SpellsTableFromJson
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly SpellbookFactory _spellbookFactory = new SpellbookFactory();

        public static BlueprintSpellsTable GetSpellsTable(SpellsTable spellsTableData)
        {
            _logger.Log($"Creating spells table from JSON data {spellsTableData.Guid}");

            BlueprintSpellsTable spellsTable = _spellbookFactory.createSpellsTable(
                spellsTableData.Name, spellsTableData.Guid, spellsTableData.Table);

            _logger.Log("DONE: Creating spell list");
            IdentifierRegistry.INSTANCE.Register(spellsTable);
            return spellsTable;
        }
    }
}
