using System;
using System.Data.Common;

namespace PF_Classes.Identifier
{
    public class IdentifierLookup
    {
        internal static readonly IdentifierLookup INSTANCE = new IdentifierLookup();

        private const string REFERENCE = "ref:";
        private const string INTRODUCED = "loc:";

        private IdentifierLookup() { }

        internal String lookupCharacterClass(String value) => performLookup(CharacterClasses.INSTANCE, value);
        internal String lookupStatProgession(String value) => performLookup(StatProgession.INSTANCE, value);
        internal String lookupFeature(String value) => performLookup(Features.INSTANCE, value);
        internal String lookupSpell(String value) => performLookup(Spells.INSTANCE, value);
        internal String lookupSpellbook(String value) => performLookup(Spellbooks.INSTANCE, value);

        private String performLookup(Identifier identifierInstance, String value)
        {
            if (value.StartsWith(REFERENCE))
            {
                return identifierInstance.GetGuidFor(value.Replace(REFERENCE, ""));
            }
            if (value.StartsWith(INTRODUCED))
            {
                return IdentifierRegistry.INSTANCE.GuidForName(value.Replace(INTRODUCED, ""));
            }
            // if the identifier not starts with a certrain string we simply return it
            // that's maybe not the best idea and but it ok for now...
            return value;
        }
    }
}
