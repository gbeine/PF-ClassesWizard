using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Alignments;
using PF_Core;
using PF_Core.CallOfTheWild.AdditionalSpellDescriptors;

namespace PF_Classes.Transformations
{
    public class EnumParser
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        internal static DiceType parseDiceType(String value)
        {
            _logger.Log($"Parsing DiceType from {value}");
            DiceType diceType;
            if (DiceType.TryParse(value, out diceType))
            {
                return diceType;
            }

            throw new InvalidOperationException($"Cannot parse dice type {value}");
        }

        internal static StatType parseStatType(String value)
        {
            _logger.Log($"Parsing StatType from {value}");
            StatType statType;
            if (StatType.TryParse(value, out statType))
            {
                return statType;
            }
            if (StatType.TryParse($"Skill{value}", out statType))
            {
                return statType;
            }

            throw new InvalidOperationException($"Cannot parse stat type {value}");
        }

        internal static CantripsType parseCantripsType(String value)
        {
            _logger.Log($"Parsing CantripsType from {value}");
            CantripsType cantripsType;
            if (CantripsType.TryParse(value, out cantripsType))
            {
                return cantripsType;
            }

            throw new InvalidOperationException($"Cannot parse cantrips type {value}");
        }

        internal static WeaponCategory parseWeaponCategory(String value)
        {
            _logger.Log($"Parsing WeaponCategory from {value}");
            WeaponCategory weaponCategory;
            if (WeaponCategory.TryParse(value, out weaponCategory))
            {
                return weaponCategory;
            }

            throw new InvalidOperationException($"Cannot parse weapon type {value}");
        }

        internal static ArmorProficiencyGroup parseArmorProficiency(String value)
        {
            _logger.Log($"Parsing ArmorProficiencyGroup from {value}");
            ArmorProficiencyGroup armorProficiency;
            if (ArmorProficiencyGroup.TryParse(value, out armorProficiency))
            {
                return armorProficiency;
            }

            throw new InvalidOperationException($"Cannot parse armor proficiency type {value}");
        }

        internal static AlignmentMaskType parseAlignment(String value)
        {
            _logger.Log($"Parsing AlignmentMaskType from {value}");
            AlignmentMaskType alignment;
            if (AlignmentMaskType.TryParse(value, out alignment))
            {
                return alignment;
            }

            throw new InvalidOperationException($"Cannot parse alignment type {value}");
        }
        internal static FeatureGroup parseFeatureGroup(String value)
        {
            _logger.Log($"Parsing FeatureGroup from {value}");
            FeatureGroup featureGroup;
            if (FeatureGroup.TryParse(value, out featureGroup))
            {
                return featureGroup;
            }

            throw new InvalidOperationException($"Cannot parse feature group type {value}");
        }

        internal static ModifierDescriptor parseModifierDescriptor(String value)
        {
            _logger.Log($"Parsing ModifierDescriptor from {value}");
            ModifierDescriptor modifierDescriptor;
            if (ModifierDescriptor.TryParse(value, out modifierDescriptor))
            {
                return modifierDescriptor;
            }

            throw new InvalidOperationException($"Cannot parse modifier descriptor type {value}");
        }

        internal static SpellDescriptor parseSpellDescriptor(String value)
        {
            _logger.Log($"Parsing SpellDescriptor from {value}");
            SpellDescriptor spellDescriptor;
            if (SpellDescriptor.TryParse(value, out spellDescriptor))
            {
                return spellDescriptor;
            }
            // CallOfTheWild spell descriptors
            _logger.Log($"Parsing ExtraSpellDescriptor from {value}");
            ExtraSpellDescriptor extraSpellDescriptor;
            if (ExtraSpellDescriptor.TryParse(value, out extraSpellDescriptor))
            {
                return (SpellDescriptor)extraSpellDescriptor;
            }
            throw new InvalidOperationException($"Cannot parse spell descriptor type {value}");
        }
    }
}
