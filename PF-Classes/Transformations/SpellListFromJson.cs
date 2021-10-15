using System;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core;
using PF_Core.Factories;
using PF_Core.Repositories;

namespace PF_Classes.Transformations
{
    public class SpellListFromJson
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly SpellbookRepository _spellbookRepository = SpellbookRepository.INSTANCE;

        private static readonly SpellbookFactory _spellbookFactory = new SpellbookFactory();

        public static BlueprintSpellList GetSpellList(SpellList spellListData)
        {
            _logger.Log($"Creating spell list from JSON data {spellListData.Guid}");

            BlueprintSpellList spellList = _spellbookFactory.createSpellList(
                spellListData.Name, spellListData.Guid, spellListData.Level);

            // cantrips are at level 0, therefore we need to go from 0 to the last level
            for (int i = 0; i <= spellListData.Level; i++)
            {
                foreach (var spellId in spellListData.SpellsByLevel[i])
                {
                    BlueprintAbility spell = getSpell(spellId);
                    spellList.SpellsByLevel[i].Spells.Add(spell);
                }
            }

            _logger.Log("DONE: Creating spell list");
            IdentifierRegistry.INSTANCE.Register(spellList);
            return spellList;
        }

        private static BlueprintAbility getSpell(String value) =>
            _spellbookRepository.GetSpell(
                IdentifierLookup.INSTANCE.lookupSpell(value));
    }
}
