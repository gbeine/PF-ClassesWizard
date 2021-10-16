using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Utility;
using PF_Core.Facades;

namespace PF_Core.Factories
{
    public class ComponentFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        public AddKnownSpell CreateAddKnownSpell(BlueprintAbility spell, BlueprintCharacterClass characterClass, int level)
        {
            _logger.Debug($"Create AddKnownSpell for {spell.name}");
            AddKnownSpell addKnownSpell = _library.Create<AddKnownSpell>();
            addKnownSpell.Spell = spell;
            addKnownSpell.SpellLevel = level;
            addKnownSpell.CharacterClass = characterClass;

            _logger.Debug($"DONE: Create AddKnownSpell for {spell.name}");
            return addKnownSpell;
        }

        public AddStatBonus CreateAddStatBonus(StatType statType, int value, ModifierDescriptor descriptor)
        {
            _logger.Debug($"Create AddStatBonus");
            AddStatBonus addStatBonus = _library.Create<AddStatBonus>();
            addStatBonus.Stat = statType;
            addStatBonus.Value = value;
            addStatBonus.Descriptor = descriptor;

            _logger.Debug($"DONE: Create AddStatBonus");
            return addStatBonus;
        }

        public BuffDescriptorImmunity CreateBuffDescriptorImmunity(SpellDescriptor spellDescriptor)
        {
            _logger.Debug($"Create BuffDescriptorImmunity {spellDescriptor}");
            BuffDescriptorImmunity buffDescriptorImmunity = _library.Create<BuffDescriptorImmunity>();
            buffDescriptorImmunity.Descriptor = spellDescriptor;

            _logger.Debug($"DONE: Create BuffDescriptorImmunity {spellDescriptor}");
            return buffDescriptorImmunity;
        }

        public SpellImmunityToSpellDescriptor CreateSpellImmunityToSpellDescriptor(SpellDescriptor spellDescriptor)
        {
            _logger.Debug($"Create SpellImmunityToSpellDescriptor {spellDescriptor}");
            SpellImmunityToSpellDescriptor spellImmunityToSpellDescriptor = _library.Create<SpellImmunityToSpellDescriptor>();
            spellImmunityToSpellDescriptor.Descriptor = spellDescriptor;

            _logger.Debug($"DONE: Create SpellImmunityToSpellDescriptor {spellDescriptor}");
            return spellImmunityToSpellDescriptor;
        }

        public NoSelectionIfAlreadyHasFeature CreateNoSelectionIfAlreadyHasFeature(bool anyFeatureFromSelection)
        {
            _logger.Debug($"Create NoSelectionIfAlreadyHasFeature");
            NoSelectionIfAlreadyHasFeature noSelectionIfAlreadyHasFeature =
                _library.Create<NoSelectionIfAlreadyHasFeature>(n =>
                    {
                        n.AnyFeatureFromSelection = anyFeatureFromSelection;
                        n.Features = Array.Empty<BlueprintFeature>();
                    });

            _logger.Debug($"DONE: Create NoSelectionIfAlreadyHasFeature");
            return noSelectionIfAlreadyHasFeature;
        }

        public Blindsense CreateBlindsense(int range, bool blindsight = false)
        {
            _logger.Debug($"Create Blindsense");
            Blindsense blindsense = _library.Create<Blindsense>();
            blindsense.Range = range.Feet();
            blindsense.Blindsight = blindsight;

            _logger.Debug($"DONE: Create Blindsense");
            return blindsense;
        }

        public PrerequisiteNoFeature CreatePrerequisiteNoFeature(BlueprintFeature feature)
        {
            _logger.Debug($"Create PrerequisiteNoFeature");
            PrerequisiteNoFeature prerequisiteNoFeature = _library.Create<PrerequisiteNoFeature>();
            prerequisiteNoFeature.Feature = feature;
            prerequisiteNoFeature.Group = Prerequisite.GroupType.All;

            _logger.Debug($"DONE: Create PrerequisiteNoFeature");
            return prerequisiteNoFeature;

        }

        public RemoveFeatureOnApply CreateRemoveFeatureOnApply(BlueprintFeature feature)
        {
            _logger.Debug($"Create RemoveFeatureOnApply for {feature.name}");
            RemoveFeatureOnApply removeFeatureOnApply = _library.Create<RemoveFeatureOnApply>();
            removeFeatureOnApply.Feature = feature;

            _logger.Debug($"DONE: Create RemoveFeatureOnApply for {feature.name}");
            return removeFeatureOnApply;
        }
    }
}
