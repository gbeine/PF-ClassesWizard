using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities.Blueprints;
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

        private static readonly ComponentFactory _componentFactory = new ComponentFactory();

        private static readonly PF_Core.Factories.CallOfTheWild.ComponentFactory _cotwComponentFactory =
            new PF_Core.Factories.CallOfTheWild.ComponentFactory();

        public static BlueprintComponent GetComponent(Component componentData, BlueprintCharacterClass characterClass)
        {
            _logger.Log($"Creating component from JSON data {componentData.Type}");

            BlueprintComponent component;
            if (createFunctionDelegates.ContainsKey(componentData.Type))
            {
                component = createFunctionDelegates[componentData.Type](componentData, characterClass);
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

        private static readonly Dictionary<String, Func<Component, BlueprintCharacterClass, BlueprintComponent>> createFunctionDelegates =
            new Dictionary<string, Func<Component, BlueprintCharacterClass, BlueprintComponent>>();

        static ComponentFromJson()
        {
            createFunctionDelegates.Add("AddKnownSpell",
                (component, characterClass) => _componentFactory.CreateAddKnownSpell(
                    getSpell(component.AsString("Spell")), characterClass, component.AsInt("SpellLevel")));

            createFunctionDelegates.Add("BuffDescriptorImmunity",
                (component, characterClass) => _componentFactory.CreateBuffDescriptorImmunity(
                    EnumParser.parseSpellDescriptor(component.AsString("Descriptor"))));

            createFunctionDelegates.Add("SpellImmunityToSpellDescriptor",
                (component, characterClass) => _componentFactory.CreateSpellImmunityToSpellDescriptor(
                    EnumParser.parseSpellDescriptor(component.AsString("Descriptor"))));

            createFunctionDelegates.Add("NoSelectionIfAlreadyHasFeature",
                (component, characterClass) => _componentFactory.CreateNoSelectionIfAlreadyHasFeature(
                    component.AsBool("AnyFeatureFromSelection")));

            createFunctionDelegates.Add("Blindsense",
                (component, characterClass) => _componentFactory.CreateBlindsense(component.AsInt("Range")));

            createFunctionDelegates.Add("Blindsight",
                (component, characterClass) => _componentFactory.CreateBlindsense(component.AsInt("Range"), true));

            createFunctionDelegates.Add("RemoveFeatureOnApply",
                (component, characterClass) => _componentFactory.CreateRemoveFeatureOnApply(
                        _featuresRepository.GetFeature(
                        IdentifierLookup.INSTANCE.lookupFeature(component.AsString("Feature")))));

            createFunctionDelegates.Add("PrerequisiteNoFeature",
                (component, characterClass) => _componentFactory.CreatePrerequisiteNoFeature(
                    _featuresRepository.GetFeature(
                        IdentifierLookup.INSTANCE.lookupFeature(component.AsString("Feature")))));

            // components from CallOfTheWild

            createFunctionDelegates.Add("SetVisibilityLimit",
                (component, characterClass) =>
                    _cotwComponentFactory.CreateSetVisibilityLimit(component.AsInt("VisibilityLimit")));

            createFunctionDelegates.Add("AddOutgoingConcealment",
                (component, characterClass) =>
                    _cotwComponentFactory.CreateAddOutgoingConcealment(component.AsInt("DistanceGreater")));
        }
    }
}
