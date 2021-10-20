using System;

namespace PF_Core.CallOfTheWild.MetamagicFeats
{
    [Flags]
    public enum MetamagicExtender
    {
        //in game metamagic is used up to 32, which is 0x00000020
        Intensified = 0x40000000,
        Dazing = 0x20000000,
        Persistent = 0x10000000,
        Rime = 0x08000000,
        Toppling = 0x04000000,
        Selective = 0x02000000,
        ElementalFire = 0x01000000,
        ElementalCold = 0x00800000,
        ElementalElectricity = 0x00400000,
        ElementalAcid = 0x00200000,
        Elemental = ElementalFire | ElementalCold | ElementalElectricity | ElementalAcid,
        BloodIntensity = 0x00100000,
        IntensifiedGeneral = BloodIntensity | Intensified,
        Piercing = 0x00080000,
        ForceFocus = 0x00040000,
        RangedAttackRollBonus = 0x00020000,
        ExtraRoundDuration = 0x00010000,
        ImprovedSpellSharing = 0x00008000,
        RollSpellResistanceTwice = 0x00004000,
        ThrenodicSpell = 0x00002000,
        VerdantSpell = 0x00001000,
        BloodPiercing = 0x00000800,
        FreeMetamagic = ForceFocus | RangedAttackRollBonus | BloodIntensity | ExtraRoundDuration | ImprovedSpellSharing | RollSpellResistanceTwice | BloodPiercing
    }
}
