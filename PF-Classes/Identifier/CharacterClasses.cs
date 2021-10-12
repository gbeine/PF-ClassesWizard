using System;

namespace PF_Classes.Identifier
{
    public class CharacterClasses : Identifier
    {
        public static readonly CharacterClasses INSTANCE = new CharacterClasses();

        private CharacterClasses() { }

        public const String ALCHEMIST  = "0937bec61c0dabc468428f496580c721";
        public const String BARBARIAN  = "f7d7eb166b3dd594fb330d085df41853";
        public const String BARD       = "772c83a25e2268e448e841dcd548235f";
        public const String CLERIC     = "67819271767a9dd4fbfd4ae700befea0";
        public const String DRUID      = "610d836f3a3a9ed42a4349b62f002e96";
        public const String FIGHTER    = "48ac8db94d5de7645906c7d0ad3bcfbd";
        public const String INQUISITOR = "f1a70d9e1b0b41e49874e1fa9052a1ce";
        public const String KINETICIST = "42a455d9ec1ad924d889272429eb8391";
        public const String MAGUS      = "45a4607686d96a1498891b3286121780";
        public const String MONK       = "e8f21e5b58e0569468e420ebea456124";
        public const String PALADIN    = "bfa11238e7ae3544bbeb4d0b92e897ec";
        public const String RANGER     = "cda0615668a6df14eb36ba19ee881af6";
        public const String ROGUE      = "299aa766dee3cbf4790da4efb8c72484";
        public const String SLAYER     = "c75e0971973957d4dbad24bc7957e4fb";
        public const String SORCERER   = "b3a505fb61437dc4097f43c3f8f9a4cf";
        public const String WIZARD     = "ba34257984f4c41408ce1dc2004e342e";
    }
}
