using System;
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

        internal String lookupCharacterClass(String value) => performLookup(CharacterClasses.INSTANCE, value);
        internal String lookupStatProgession(String value) => performLookup(StatProgession.INSTANCE, value);
        internal String lookupFeature(String value) => performLookup(Features.INSTANCE, value);
        internal String lookupSpell(String value) => performLookup(Spells.INSTANCE, value);
        internal String lookupSpellbook(String value) => performLookup(Spellbooks.INSTANCE, value);

        internal bool existsCharacterClass(String value) => performExists(CharacterClasses.INSTANCE, value);
        internal bool existsFeature(String value) => performExists(Features.INSTANCE, value);
        internal bool existsSpell(String value) => performExists(Spellbooks.INSTANCE, value);

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

        private bool performExists(Identifier identifierInstance, String value)
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
                    exists |= IdentifierRegistry.INSTANCE.NameExists(value.Replace(INTRODUCED, ""));
                }
            }
            return exists;
        }
    }
}
