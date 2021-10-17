using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core;
using PF_Core.Factories;
using PF_Core.Repositories;

namespace PF_Classes.Transformations
{
    public class ComponentFromJson
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly FeaturesRepository _featuresRepository = FeaturesRepository.INSTANCE;
        private static readonly SpellbookRepository _spellbookRepository = SpellbookRepository.INSTANCE;
        private static readonly BuffRepository _buffRepository = BuffRepository.INSTANCE;

        private static readonly ComponentFactory _componentFactory = new ComponentFactory();

        private static readonly PF_Core.Factories.CallOfTheWild.ComponentFactory _cotwComponentFactory =
            new PF_Core.Factories.CallOfTheWild.ComponentFactory();

        public static BlueprintComponent GetComponent(Component componentData, BlueprintCharacterClass characterClass)
        {
            _logger.Log($"Creating component from JSON data {componentData.Type}");

            BlueprintComponent component;
            if (createCharacterClassFunctionDelegates.ContainsKey(componentData.Type))
            {
                component = createCharacterClassFunctionDelegates[componentData.Type](componentData, characterClass);
            }
            else if (createFunctionDelegates.ContainsKey(componentData.Type))
            {
                component = createFunctionDelegates[componentData.Type](componentData);
            }
            else
            {
                String message = $"Unknown component type {componentData.Type}";
                _logger.Error(message);
                throw new InvalidOperationException(message);
            }

            _logger.Log($"DONE: Creating component from JSON data {componentData.Type}");
            return component;
        }

        public static BlueprintComponent GetComponent(Component componentData)
        {
            _logger.Log($"Creating component from JSON data {componentData.Type}");

            BlueprintComponent component;
            if (createFunctionDelegates.ContainsKey(componentData.Type))
            {
                component = createFunctionDelegates[componentData.Type](componentData);
            }
            else
            {
                String message = $"Unknown component type {componentData.Type}";
                _logger.Error(message);
                throw new InvalidOperationException(message);
            }

            _logger.Log($"DONE: Creating component from JSON data {componentData.Type}");
            return component;
        }

        private static BlueprintAbility getSpell(String value) =>
            _spellbookRepository.GetSpell(
                IdentifierLookup.INSTANCE.lookupSpell(value));

        private static readonly Dictionary<String, Func<Component, BlueprintComponent>> createFunctionDelegates =
            new Dictionary<string, Func<Component, BlueprintComponent>>();

        private static readonly Dictionary<String, Func<Component, BlueprintCharacterClass, BlueprintComponent>> createCharacterClassFunctionDelegates =
            new Dictionary<string, Func<Component, BlueprintCharacterClass, BlueprintComponent>>();

        static ComponentFromJson()
        {
            _logger.Debug($"Adding delegate: AddKnownSpell");
            createCharacterClassFunctionDelegates.Add("AddKnownSpell",
                (component, characterClass) => _componentFactory.CreateAddKnownSpell(
                    getSpell(component.AsString("Spell")), characterClass, component.AsInt("SpellLevel")));

            _logger.Debug($"Adding delegate: AddStatBonus");
            createFunctionDelegates.Add("AddStatBonus",
                component => _componentFactory.CreateAddStatBonus(
                    EnumParser.parseStatType(component.AsString("Stat")),
                    component.AsInt("Bonus"),
                    component.Exists("Descriptor")
                        ? EnumParser.parseModifierDescriptor(component.AsString("Descriptor"))
                        : ModifierDescriptor.UntypedStackable));

            _logger.Debug($"Adding delegate: Blindsense");
            createFunctionDelegates.Add("Blindsense",
                component => _componentFactory.CreateBlindsense(component.AsInt("Range")));

            _logger.Debug($"Adding delegate: Blindsight");
            createFunctionDelegates.Add("Blindsight",
                component => _componentFactory.CreateBlindsense(component.AsInt("Range"), true));

            _logger.Debug($"Adding delegate: Blindsight");
            createFunctionDelegates.Add("BuffDescriptorImmunity",
                (component) => _componentFactory.CreateBuffDescriptorImmunity(
                    EnumParser.parseSpellDescriptor(component.AsString("Descriptor"))));

            _logger.Debug($"Adding delegate: NoSelectionIfAlreadyHasFeature");
            createFunctionDelegates.Add("NoSelectionIfAlreadyHasFeature",
                (component) => _componentFactory.CreateNoSelectionIfAlreadyHasFeature(
                    component.AsBool("AnyFeatureFromSelection")));

            _logger.Debug($"Adding delegate: PrerequisiteNoFeature");
            createFunctionDelegates.Add("PrerequisiteNoFeature",
                (component) => _componentFactory.CreatePrerequisiteNoFeature(
                    _featuresRepository.GetFeature(
                        IdentifierLookup.INSTANCE.lookupFeature(component.AsString("Feature")))));

            _logger.Debug($"Adding delegate: RemoveFeatureOnApply");
            createFunctionDelegates.Add("RemoveFeatureOnApply",
                component => _componentFactory.CreateRemoveFeatureOnApply(
                    _featuresRepository.GetFeature(IdentifierLookup.INSTANCE.lookupFeature(component.AsString("Feature")))));

            _logger.Debug($"Adding delegate: SpecificBuffImmunity");
            createFunctionDelegates.Add("SpecificBuffImmunity",
                component => _componentFactory.CreateSpecificBuffImmunity(
                    _buffRepository.GetBuff(IdentifierLookup.INSTANCE.lookupBuff(component.AsString("Buff")))));

            _logger.Debug($"Adding delegate: SpellImmunityToSpellDescriptor");
            createFunctionDelegates.Add("SpellImmunityToSpellDescriptor",
                (component) => _componentFactory.CreateSpellImmunityToSpellDescriptor(
                    EnumParser.parseSpellDescriptor(component.AsString("Descriptor"))));

            // components from CallOfTheWild

            _logger.Debug($"Adding delegate: AddOutgoingConcealment");
            createFunctionDelegates.Add("AddOutgoingConcealment",
                (component) =>
                    _cotwComponentFactory.CreateAddOutgoingConcealment(component.AsInt("DistanceGreater")));

            _logger.Debug($"Adding delegate: SetVisibilityLimit");
            createFunctionDelegates.Add("SetVisibilityLimit",
                (component) =>
                    _cotwComponentFactory.CreateSetVisibilityLimit(component.AsInt("VisibilityLimit")));

            _logger.Debug($"Adding delegate: Silence");
            createFunctionDelegates.Add("Silence",
                (component) => _cotwComponentFactory.CreateSilence());

            _logger.Debug($"Adding delegate: SpellFailureChance");
            createFunctionDelegates.Add("SpellFailureChance",
                component => _cotwComponentFactory.CreateSpellFailureChance(
                    component.AsInt("Chance"), component.AsBool("IgnorePsychic")));

            _logger.Debug($"Adding delegate: SpellFailureChance");
            createFunctionDelegates.Add("SuppressBuffsCorrect",
                (component) =>
                    _cotwComponentFactory.CreateSuppressBuffsCorrect(
                        component.Exists("Descriptor")
                            ? EnumParser.parseSpellDescriptor(component.AsString("Descriptor"))
                            : SpellDescriptor.None,
                        component.Exists("Buffs")
                            ? component.AsArray("Buffs")
                                .Select(b => _buffRepository.GetBuff(IdentifierLookup.INSTANCE.lookupBuff(b)))
                                .ToArray()
                            : Array.Empty<BlueprintBuff>()
                    ));

            _logger.Debug($"Adding delegate: WeaponsOnlyAttackBonus");
            createFunctionDelegates.Add("WeaponsOnlyAttackBonus",
                (component) =>
                    _cotwComponentFactory.CreateWeaponsOnlyAttackBonus(component.AsInt("Bonus")));
        }
    }
}
