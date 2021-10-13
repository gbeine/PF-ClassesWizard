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
                        _logger.Log($"public const String {spell.School} {spell.name} = \"{spell.AssetGuid}\"; // {spell.Name}");
                    }
                }
            }
        }

        internal static void DumpComponentsRogue(LibraryScriptableObject library)
        {
            BlueprintCharacterClass c = library.Get<BlueprintCharacterClass>(CharacterClasses.ROGUE);
            _logger.Log(c.Name);
            Dump(c.ComponentsArray);
        }

        internal static void DumpComponentsKineticist(LibraryScriptableObject library)
        {
            BlueprintCharacterClass c = library.Get<BlueprintCharacterClass>(CharacterClasses.KINETICIST);
            _logger.Log(c.Name);
            Dump(c.ComponentsArray);
        }

        internal static void DumpSpellbooksWizard(LibraryScriptableObject library)
        {
            BlueprintCharacterClass c = library.Get<BlueprintCharacterClass>(CharacterClasses.WIZARD);
            _logger.Log(c.Name);
            Dump(c.Spellbook);
            _logger.Log($"SpellsKnown {c.Spellbook.SpellsKnown}");
            _logger.Log($"SpellsPerDay {c.Spellbook.SpellsPerDay}");
            Dump(c.Spellbook.SpellsPerDay.Levels);
            Dump(c.Spellbook.ComponentsArray);
        }

        internal static void DumpSpellbooksCleric(LibraryScriptableObject library)
        {
            BlueprintCharacterClass c = library.Get<BlueprintCharacterClass>(CharacterClasses.CLERIC);
            _logger.Log(c.Name);
            Dump(c.Spellbook);
            _logger.Log($"SpellsKnown {c.Spellbook.SpellsKnown}");
            _logger.Log($"SpellsPerDay {c.Spellbook.SpellsPerDay}");
            Dump(c.Spellbook.SpellsPerDay.Levels);
        }
        internal static void DumpSpellbooksRanger(LibraryScriptableObject library)
        {
            BlueprintCharacterClass c = library.Get<BlueprintCharacterClass>(CharacterClasses.RANGER);
            _logger.Log(c.Name);
            Dump(c.Spellbook);
            _logger.Log($"SpellsKnown {c.Spellbook.SpellsKnown}");
            _logger.Log($"SpellsPerDay {c.Spellbook.SpellsPerDay}");
            Dump(c.Spellbook.SpellsPerDay.Levels);
        }

        internal static void Dump(BlueprintSpellbook blueprintSpellbook)
        {
            _logger.Log($"IsArcane: {blueprintSpellbook.IsArcane}");
            _logger.Log($"Spontaneous: {blueprintSpellbook.Spontaneous}");
            _logger.Log($"AllSpellsKnown: {blueprintSpellbook.AllSpellsKnown}");
            _logger.Log($"CanCopyScrolls: {blueprintSpellbook.CanCopyScrolls}");
            _logger.Log($"Cantrips: {blueprintSpellbook.CantripsType}");
            _logger.Log($"Components: {blueprintSpellbook.ComponentsArray.Length}");
        }

        private static void Dump(BlueprintComponent[] blueprintComponents)
        {
            foreach (var c in blueprintComponents)
            {
                _logger.Log($"C: {c.name}");
            }
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