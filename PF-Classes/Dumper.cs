using System;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using PF_Classes.Identifier;
using PF_Core;
using PF_Core.Extensions;

namespace PF_Classes
{
    public class Dumper
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        public static void Dump(LibraryScriptableObject library)
        {

        }

        internal static void DumpSpellbooks(LibraryScriptableObject library)
        {
            foreach (var spellbook in Spellbooks.INSTANCE.AllIdentifiers)
            {
                _logger.Log($"Spellbook {spellbook.Key}");
                BlueprintSpellbook sp = library.Get<BlueprintSpellbook>(spellbook.Value);
                foreach (var sll in sp.SpellList.SpellsByLevel)
                {
                    _logger.Log($"Level {sll.SpellLevel}");
                    foreach (var spell in sll.Spells)
                    {
                        _logger.Log($"public static const String {spell.name} = \"{spell.AssetGuid}\"; // {spell.Name}");
                    }
                }
            }
        }

        internal static void Dump(BlueprintCharacterClass blueprintCharacterClass)
        {
            _logger.Log(blueprintCharacterClass.Name);
            Dump(blueprintCharacterClass.Progression.LevelEntries);
        }

        internal static void Dump(BlueprintArchetype blueprintArchetype)
        {
            _logger.Log(blueprintArchetype.Name);
            Dump(blueprintArchetype.AddFeatures);
        }

        internal static void Dump(BlueprintSpellsTable blueprintSpellsTable)
        {
            _logger.Log(blueprintSpellsTable);
            Dump(blueprintSpellsTable.Levels);
        }

        private static void Dump(SpellsLevelEntry[] spellsLevelEntries)
        {
            int i = 1;
            foreach (var sle in spellsLevelEntries)
            {
                string list = string.Join(",", sle.Count);
                _logger.Log($"Level {i}: {list}");
                i++;
            }
        }

        private static void Dump(LevelEntry[] levelEntries)
        {
            foreach (var le in levelEntries)
            {
                _logger.Log(String.Format("Level {0}", le.Level));
                foreach (var f in le.Features)
                {
                    _logger.Log(String.Format("{0} {1}", f.AssetGuid, f.Name));
                }
            }
        }

    }
}