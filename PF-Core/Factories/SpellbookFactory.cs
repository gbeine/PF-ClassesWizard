using System;
using System.Collections.Generic;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using PF_Core.Extensions;
using PF_Core.Facades;

namespace PF_Core.Factories
{
    public class SpellbookFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        private static readonly SpellbookFactory __instance = new SpellbookFactory();

        private SpellbookFactory() { }

        public static SpellbookFactory INSTANCE
        {
            get { return __instance; }
        }

        public BlueprintSpellbook CreateSpellbook(String name, String guid)
        {
            _logger.Debug($"Create spellbook {name} with id {guid}");

            BlueprintSpellbook spellbook = _library.Create<BlueprintSpellbook>();
            spellbook.SetAssetId(guid);
            spellbook.name = name;

            _library.Add(spellbook);

            _logger.Debug($"DONE: Create spellbook {name} with id {guid}");
            return spellbook;
        }

        public BlueprintSpellbook CreateSpellbookFrom(String name, String guid, String fromAssetId)
        {
            _logger.Debug($"Create spellbook {name} with id {guid} based on {fromAssetId}");

            BlueprintSpellbook original = _library.GetSpellbook(fromAssetId);
            BlueprintSpellbook spellbook = UnityEngine.Object.Instantiate(original);
            spellbook.SetAssetId(guid);
            spellbook.name = name;

            _library.Add(spellbook);

            _logger.Debug($"DONE: Create spellbook {name} with id {guid} based on {fromAssetId}");
            return spellbook;
        }

        public BlueprintSpellbook CreateSpellbook(String name, String guid, BlueprintCharacterClass characterClass,
            bool isArcane, bool isSpontaneous, bool canCopyScrolls, bool allSpellsKnown, int SpellsPerLevel, int CasterLevelModifier,
            StatType castingAttribute, CantripsType cantripsType, BlueprintSpellsTable spellsKnown, BlueprintSpellsTable spellsPerDay, BlueprintSpellList spellList)
        {
            _logger.Debug($"Create spellbook {name} with id {guid}");

            BlueprintSpellbook spellbook = CreateSpellbook(name, guid);
            spellbook.Name = new LocalizationFactory().CreateString($"{name}.Name", characterClass.Name);
            spellbook.IsArcane = isArcane;
            spellbook.Spontaneous = isSpontaneous;
            spellbook.CanCopyScrolls = canCopyScrolls;
            spellbook.AllSpellsKnown = allSpellsKnown;
            spellbook.CastingAttribute = castingAttribute;
            spellbook.CharacterClass = characterClass;
            spellbook.SpellsPerLevel = SpellsPerLevel;
            spellbook.CasterLevelModifier = CasterLevelModifier;
            spellbook.SpellsKnown = spellsKnown;
            spellbook.SpellsPerDay = spellsPerDay;
            spellbook.CantripsType = cantripsType;
            spellbook.SpellList = spellList;
            spellbook.CharacterClass = characterClass;

            _logger.Debug($"DONE: Create spellbook {name} with id {guid}");
            return spellbook;
        }

        public BlueprintSpellList createSpellList(String name, String guid, int level)
        {
            _logger.Debug($"Create spell list {name} with id {guid}");

            BlueprintSpellList spellList = _library.Create<BlueprintSpellList>();
            spellList.SetAssetId(guid);
            spellList.name = name;
            // +1 here because 0 are cantrips, levels go from one to the value of level
            spellList.SpellsByLevel = new SpellLevelList[level+1];

            for (int i = 0; i < spellList.SpellsByLevel.Length; i++)
            {
                spellList.SpellsByLevel[i] = new SpellLevelList(i);
            }

            _library.Add(spellList);

            _logger.Debug($"DONE: Create spell list {name} with id {guid}");
            return spellList;
        }

        public SpellsLevelEntry createSpellsLevelEntry(params int[] count)
        {
            _logger.Debug($"Create spells level entry with {count.Length}");

            SpellsLevelEntry spellsLevelEntry = new SpellsLevelEntry();
            spellsLevelEntry.Count = count;

            _logger.Debug("DOME: Create spells level entry");
            return spellsLevelEntry;
        }

        public BlueprintSpellsTable createSpellsTable(String name, String guid, params SpellsLevelEntry[] levels)
        {
            _logger.Debug($"Create spells table {name} with id {guid}");

            BlueprintSpellsTable spellsTable = _library.Create<BlueprintSpellsTable>();
            spellsTable.SetAssetId(guid);
            spellsTable.name = name;
            spellsTable.Levels = levels;

            _library.Add(spellsTable);

            _logger.Debug($"DONE: Create spells table {name} with id {guid}");
            return spellsTable;
        }

        public BlueprintSpellsTable createSpellsTable(String name, String guid, List<List<int>> levels)
        {
            _logger.Debug($"Create spells table {name} with id {guid}");

            List<SpellsLevelEntry> spellsLevelEntries = new List<SpellsLevelEntry>();
            foreach (var level in levels)
            {
                SpellsLevelEntry levelEntry = createSpellsLevelEntry(level.ToArray());
                spellsLevelEntries.Add(levelEntry);
            }

            BlueprintSpellsTable spellsTable = createSpellsTable(name, guid, spellsLevelEntries.ToArray());

            _logger.Debug($"DONE: Create spells table {name} with id {guid}");
            return spellsTable;
        }

        public BlueprintSpellbook createEmptySpellbook()
        {
            return _library.Create<BlueprintSpellbook>();
        }
    }
}
