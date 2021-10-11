using System;

namespace PF_Classes.Identifier
{
    public class SpellLists : Identifier
    {
        public static readonly SpellLists INSTANCE = new SpellLists();

        private SpellLists() { }

        public const String WIZARD_SPELLLIST = "ba0401fdeb4062f40a7aa95b6f07fe89";
    }
}
