using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using PF_Core.Facades;

namespace PF_Core.Factories
{
    public class SpellbookFactory
    {
        private static readonly Harmony.FastSetter blueprintSpellbook_set_AssetId = Harmony.CreateFieldSetter<BlueprintSpellbook>("m_AssetGuid");
        private static readonly Harmony.FastSetter blueprintSpellList_set_AssetId = Harmony.CreateFieldSetter<BlueprintSpellList>("m_AssetGuid");

        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;
        
        public BlueprintSpellbook createSpellbook(String name, String guid)
        {
            _logger.Debug(String.Format("Create spellbook {0} with id {1}", name, guid));

            BlueprintSpellbook spellbook = _library.Create<BlueprintSpellbook>();
            blueprintSpellbook_set_AssetId(spellbook, guid);
            spellbook.name = name;

            _library.Add(spellbook);

            return spellbook;
        }
        public BlueprintSpellbook createSpellbookFrom(String name, String guid, String fromAssetId)
        {
            _logger.Debug(String.Format("Create spellbook {0} with id {1} based on {2}", name, guid, fromAssetId));

            BlueprintSpellbook original = _library.GetSpellbook(fromAssetId);
            BlueprintSpellbook clone = UnityEngine.Object.Instantiate(original);
            blueprintSpellbook_set_AssetId(clone, guid);
            clone.name = name;

            _library.Add(clone);

            return clone;
        }

        public BlueprintSpellbook createSpellbookFrom(String name, String guid, String fromAssetId, BlueprintCharacterClass characterClass,
            bool isArcane, bool isSpontaneous, bool canCopyScrolls, bool allSpellsKnown, StatType castingAttribute,
            BlueprintSpellsTable spellsKnown, BlueprintSpellsTable spellsPerDay, CantripsType cantripsType, BlueprintSpellList spellList)
        {
            _logger.Debug(String.Format("Create spellbook {0} with id {1} based on {2}", name, guid, fromAssetId));

            BlueprintSpellbook spellbook = createSpellbookFrom(name, guid, fromAssetId);
            spellbook.Name = new LocalizationFactory().CreateString($"{name}.Name", characterClass.Name);
            spellbook.IsArcane = isArcane;
            spellbook.Spontaneous = isSpontaneous;
            spellbook.CanCopyScrolls = canCopyScrolls;
            spellbook.AllSpellsKnown = allSpellsKnown;
            spellbook.CastingAttribute = castingAttribute;
            spellbook.CharacterClass = characterClass;
            spellbook.CasterLevelModifier = 0;
            spellbook.SpellsKnown = spellsKnown;
            spellbook.SpellsPerDay = spellsPerDay;
            spellbook.CantripsType = cantripsType;
            spellbook.SpellList = spellList;

            return spellbook;
        }

        public BlueprintSpellList createSpellList(String name, String guid, int level)
        {
            BlueprintSpellList spellList = _library.Create<BlueprintSpellList>();
            blueprintSpellList_set_AssetId(spellList, guid);
            spellList.name = name;
            spellList.SpellsByLevel = new SpellLevelList[level];

            for (int i = 0; i < spellList.SpellsByLevel.Length; i++)
            {
                spellList.SpellsByLevel[i] = new SpellLevelList(i);
            }

            _library.Add(spellList);

            return spellList;
            
        }

        public BlueprintSpellbook createEmptySpellbook()
        {
            return _library.Create<BlueprintSpellbook>();
        }
    }
}
