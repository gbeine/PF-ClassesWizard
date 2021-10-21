using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using PF_Core;

namespace PF_Classes.Identifier
{
    public class IdentifierLookup
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        internal static readonly IdentifierLookup INSTANCE = new IdentifierLookup();

        private const string REFERENCE = "ref:";
        private const string INTRODUCED = "loc:";

        private IdentifierLookup() { }

        internal String lookupAbility(String value) => performLookup(Abilities.INSTANCE, value);
        internal String lookupBuff(String value) => performLookup(Buffs.INSTANCE, value);
        internal String lookupCharacterClass(String value) => performLookup(CharacterClasses.INSTANCE, value);
        internal String lookupFeature(String value) => performLookup(Features.INSTANCE, value);
        internal String lookupItem(String value) => performLookup(Items.INSTANCE, value);
        internal String lookupProgression(String value) => performLookup(Progressions.INSTANCE, value);
        internal String lookupSpell(String value) => performLookup(Abilities.INSTANCE, value);
        internal String lookupSpellbook(String value) => performLookup(Spellbooks.INSTANCE, value);
        internal String lookupSpellList(String value) => performLookup(SpellLists.INSTANCE, value);
        internal String lookupStatProgession(String value) => performLookup(StatProgession.INSTANCE, value);

        internal bool existsCharacterClass(String value) => performExists(CharacterClasses.INSTANCE, value, typeof(BlueprintCharacterClass));
        internal bool existsFeature(String value) => performExists(Features.INSTANCE, value, typeof(BlueprintFeature));
        internal bool existsProgression(String value) => performExists(Progressions.INSTANCE, value, typeof(BlueprintProgression));
        internal bool existsSpell(String value) => performExists(Abilities.INSTANCE, value,typeof(BlueprintAbility));
        internal bool existsSpellbook(String value) => performExists(Spellbooks.INSTANCE, value, typeof(BlueprintSpellbook));

        private String performLookup(Identifier identifierInstance, String value)
        {
            _logger.Debug($"Lookup identifier for {value}");
            if (value != null)
            {
                if (value.StartsWith(REFERENCE))
                {
                    return identifierInstance.GetGuidFor(value.Replace(REFERENCE, ""));
                }

                if (value.StartsWith(INTRODUCED))
                {
                    return IdentifierRegistry.INSTANCE.GuidForName(value.Replace(INTRODUCED, ""));
                }
            }
            // if the identifier not starts with a certrain string we simply return it
            // that's maybe not the best idea and but it ok for now...
            return value;
        }

        private bool performExists(Identifier identifierInstance, String value, Type type)
        {
            _logger.Debug($"Test if identifier for {value} exists");
            bool exists = false;
            if (value != null)
            {
                if (value.StartsWith(REFERENCE))
                {
                    exists |= identifierInstance.Contains(value.Replace(REFERENCE, ""));
                }

                if (value.StartsWith(INTRODUCED))
                {
                    exists |= IdentifierRegistry.INSTANCE.ExistsAndIsA(value.Replace(INTRODUCED, ""), type);
                }
            }
            return exists;
        }
    }
}
