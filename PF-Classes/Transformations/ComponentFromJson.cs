using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
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
            createCharacterClassFunctionDelegates.Add("AddKnownSpell",
                (component, characterClass) => _componentFactory.CreateAddKnownSpell(
                    getSpell(component.AsString("Spell")), characterClass, component.AsInt("SpellLevel")));

            createFunctionDelegates.Add("AddStatBonus",
                (component) => _componentFactory.CreateAddStatBonus(
                    EnumParser.parseStatType(component.AsString("Stat")),
                    component.AsInt("Bonus"),
                    EnumParser.parseModifierDescriptor(component.AsString("Descriptor"))));

            createFunctionDelegates.Add("Blindsense",
                (component) => _componentFactory.CreateBlindsense(component.AsInt("Range")));

            createFunctionDelegates.Add("Blindsight",
                (component) => _componentFactory.CreateBlindsense(component.AsInt("Range"), true));

            createFunctionDelegates.Add("BuffDescriptorImmunity",
                (component) => _componentFactory.CreateBuffDescriptorImmunity(
                    EnumParser.parseSpellDescriptor(component.AsString("Descriptor"))));

            createFunctionDelegates.Add("NoSelectionIfAlreadyHasFeature",
                (component) => _componentFactory.CreateNoSelectionIfAlreadyHasFeature(
                    component.AsBool("AnyFeatureFromSelection")));

            createFunctionDelegates.Add("PrerequisiteNoFeature",
                (component) => _componentFactory.CreatePrerequisiteNoFeature(
                    _featuresRepository.GetFeature(
                        IdentifierLookup.INSTANCE.lookupFeature(component.AsString("Feature")))));

            createFunctionDelegates.Add("SpellImmunityToSpellDescriptor",
                (component) => _componentFactory.CreateSpellImmunityToSpellDescriptor(
                    EnumParser.parseSpellDescriptor(component.AsString("Descriptor"))));

            createFunctionDelegates.Add("SpellImmunityToSpellDescriptor",
                (component) => _componentFactory.CreateSpellImmunityToSpellDescriptor(
                    EnumParser.parseSpellDescriptor(component.AsString("Descriptor"))));

            // components from CallOfTheWild

            createFunctionDelegates.Add("AddOutgoingConcealment",
                (component) =>
                    _cotwComponentFactory.CreateAddOutgoingConcealment(component.AsInt("DistanceGreater")));

            createFunctionDelegates.Add("SetVisibilityLimit",
                (component) =>
                    _cotwComponentFactory.CreateSetVisibilityLimit(component.AsInt("VisibilityLimit")));

            createFunctionDelegates.Add("Silence",
                (component) => _cotwComponentFactory.CreateSilence());

            createFunctionDelegates.Add("SpellFailureChance",
                (component) => _cotwComponentFactory.CreateSpellFailureChance(
                    component.AsInt("Chance"), component.AsBool("IgnorePsychic")));

            createFunctionDelegates.Add("SuppressBuffsCorrect",
                (component) =>
                    _cotwComponentFactory.CreateSuppressBuffsCorrect(
                        component.Exists("Descriptor") ? EnumParser.parseSpellDescriptor(component.AsString("Descriptor")) : SpellDescriptor.None,
                        component.Exists("Buffs")
                            ? component.AsArray("Buffs")
                                .Select(b => _buffRepository.GetBuff(IdentifierLookup.INSTANCE.lookupBuff(b)))
                                .ToArray()
                            : Array.Empty<BlueprintBuff>()
                    ));

            createFunctionDelegates.Add("WeaponsOnlyAttackBonus",
                (component) =>
                    _cotwComponentFactory.CreateWeaponsOnlyAttackBonus(component.AsInt("Bonus")));
        }
    }
}
