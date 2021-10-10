using System;
using System.Security.AccessControl;
using Harmony12;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;

namespace PF_Core.Filter
{
    public class SpellListFilter
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private BlueprintSpellList spellList;

        public SpellListFilter(BlueprintSpellList blueprintSpellList)
        {
            spellList = UnityEngine.Object.Instantiate(blueprintSpellList);
        }

        public BlueprintSpellList SpellSpellList
        {
            get { return this.spellList;  }
        }
        
        public SpellListFilter excludeSpellsFromList(Predicate<BlueprintAbility> predicate)
        {
            _logger.Log("Excluding spells from list");
            foreach (var spellLevelList in spellList.SpellsByLevel)
            {
                _logger.Log($"Remove spells from level {spellLevelList.SpellLevel}");
                foreach (var spell in spellLevelList.Spells.ToArray())
                {
                    if (predicate(spell))
                    {
                        _logger.Log($"Remove spell {spell.Name} id {spell.AssetGuid}");
                        spellLevelList.Spells.Remove(spell);
                    }
                }
            }

            return this;
        }

        public SpellListFilter addSpellsFromList(BlueprintSpellList blueprintSpellList)
        {
            _logger.Log($"Add spell from {blueprintSpellList.name}");
            foreach (var spellLevelListToAdd in blueprintSpellList.SpellsByLevel)
            {
                if (spellList.SpellsByLevel[spellLevelListToAdd.SpellLevel] != null)
                {
                    _logger.Log($"Add spells to level {spellLevelListToAdd.SpellLevel}");
                    var spells = spellList.SpellsByLevel[spellLevelListToAdd.SpellLevel].Spells;
                    foreach (var spellToAdd in spellLevelListToAdd.Spells)
                    {
                        if (!spells.Contains(spellToAdd) && !doesContainSpell(spellToAdd))
                        {
                            _logger.Log($"Add spell {spellToAdd.Name} id {spellToAdd.AssetGuid}");
                            spells.Add(spellToAdd);
                        }
                    }
                }
                else
                {
                    _logger.Log($"Add full spell level {spellLevelListToAdd.SpellLevel} from list");
                    spellList.SpellsByLevel.Add(spellLevelListToAdd);
                }
            }

            return this;
        }

        private bool doesContainSpell(BlueprintAbility spell)
        {
            bool contains = false;

            foreach (var spellLevelList in spellList.SpellsByLevel)
            {
                if (spellLevelList.Spells.Contains(spell))
                {
                    _logger.Log($"Spell {spell.name} already contained at level {spellLevelList.SpellLevel}");
                    contains = true;
                    break;
                }
            }

            return contains;
        }
    }
}
