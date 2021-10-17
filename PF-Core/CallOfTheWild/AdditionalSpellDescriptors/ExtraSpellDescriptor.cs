using Kingmaker.Blueprints.Classes.Spells;

namespace PF_Core.CallOfTheWild.AdditionalSpellDescriptors
{
    public enum ExtraSpellDescriptor : long
    {
        Earth = 0x0004000000000000, //from earth blast
        Air   = 0x0008000000000000, //should be added to all air blasts (?)
        Water = 0x0010000000000000, //from kineticist water blast
                                    //cold blast have wrong descriptor 20000000000004 - i.e cold + something ?, should be just cold
                                    //magma blast is missing earth descriptor (only fire)
                                    //sandstorm only has earth (missing air)
                                    //mud blast has descriptor of 1000000000000000 while it should be earth + water
                                    //blizzard blast has 40000000000004 - while it should be cold + air
                                    //steam is only fire - should be water + fire
                                    //metal is correct
                                    //thunderstorm only has electricity - probably correct
                                    //plasma only has fire - should be air +fire
        Shadow = 0x0200000000000000,

        Pain                 = 0x1000000000000000,
        LanguageDependent    = 0x2000000000000000,
        HolyVindicatorShield = 0x4000000000000000,

        Silence = SpellDescriptor.Sonic | LanguageDependent // used for silence buff
    }
}
