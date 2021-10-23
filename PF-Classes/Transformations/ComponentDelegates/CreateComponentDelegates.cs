using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;
using PF_Core;
using PF_Core.Extensions;

namespace PF_Classes.Transformations.ComponentDelegates
{
    public class CreateComponentDelegates

    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly Dictionary<string, Action<BlueprintScriptableObject, Component>> Delegates =
            new Dictionary<string, Action<BlueprintScriptableObject, Component>>();

        public static bool CanAdd(string component) =>
            Delegates.ContainsKey(component);

        public static void Add(Component component, BlueprintScriptableObject target) =>
            Delegates[component.Type](target, component);

        static CreateComponentDelegates()
        {

            _logger.Debug($"Adding delegate: AbilityAreaEffectRunAction");
            Delegates.Add("AbilityAreaEffectRunAction",
                (target, componentData) =>
                    target.AddComponent(AbilityAreaEffectRunActionDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AbilityDeliverTouch");
            Delegates.Add("AbilityDeliverTouch",
                (target, componentData) =>
                    target.AddComponent(AbilityDeliverTouchDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AbilityResourceLogic");
            Delegates.Add("AbilityResourceLogic",
                (target, componentData) =>
                    target.AddComponent(AbilityResourceLogicDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AbilityEffectRunAction");
            Delegates.Add("AbilityEffectRunAction",
                (target, componentData) =>
                    target.AddComponent(AbilityEffectRunActionDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AbilityEffectStickyTouch");
            Delegates.Add("AbilityEffectStickyTouch",
                (target, componentData) =>
                    target.AddComponent(AbilityEffectStickyTouchDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AbilitySpawnFx");
            Delegates.Add("AbilitySpawnFx",
                (target, componentData) =>
                    target.AddComponent(AbilitySpawnFxDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AddAdditionalLimb");
            Delegates.Add("AddAdditionalLimb",
                (target, componentData) =>
                    target.AddComponent(AddAdditionalLimbDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AddAreaEffect");
            Delegates.Add("AddAreaEffect",
                (target, componentData) =>
                    target.AddComponent(AddAreaEffectDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AddCondition");
            Delegates.Add("AddCondition",
                (target, componentData) =>
                    target.AddComponent(AddConditionDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AddConditionImmunity");
            Delegates.Add("AddConditionImmunity",
                (target, componentData) =>
                    target.AddComponent(AddConditionImmunityDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AddDamageResistancePhysical");
            Delegates.Add("AddDamageResistancePhysical",
                (target, componentData) =>
                    target.AddComponent(AddDamageResistancePhysicalDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AddEnergyImmunity");
            Delegates.Add("AddEnergyImmunity",
                (target, componentData) =>
                    target.AddComponent(AddEnergyImmunityDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AddFacts");
            Delegates.Add("AddFacts",
                (target, componentData) =>
                    target.AddComponent(AddFactsDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AddMechanicsFeature");
            Delegates.Add("AddMechanicsFeature",
                (target, componentData) =>
                    target.AddComponent(AddMechanicsFeatureDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AddProficiencies");
            Delegates.Add("AddProficiencies",
                (target, componentData) =>
                    target.AddComponent(AddProficienciesDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AddSpellResistance");
            Delegates.Add("AddSpellResistance",
                (target, componentData) =>
                    target.AddComponent(AddSpellResistanceDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AddStatBonus");
            Delegates.Add("AddStatBonus",
                (target, componentData) =>
                    target.AddComponent(AddStatBonusDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AuraFeatureComponent");
            Delegates.Add("AuraFeatureComponent",
                (target, componentData) =>
                    target.AddComponent(AuraFeatureComponentDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: Blindsense");
            Delegates.Add("Blindsense",
                (target, componentData) =>
                    target.AddComponent(BlindsenseDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: BuffDescriptorImmunity");
            Delegates.Add("BuffDescriptorImmunity",
                (target, componentData) =>
                    target.AddComponent(BuffDescriptorImmunityDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: ContextRankConfig");
            Delegates.Add("ContextRankConfig",
                (target, componentData) =>
                    target.AddComponent(ContextRankConfigDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: NoSelectionIfAlreadyHasFeature");
            Delegates.Add("NoSelectionIfAlreadyHasFeature",
                (target, componentData) =>
                    target.AddComponent(NoSelectionIfAlreadyHasFeatureDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: PrerequisiteNoFeature");
            Delegates.Add("PrerequisiteNoFeature",
                (target, componentData) =>
                    target.AddComponent(PrerequisiteNoFeatureDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: RemoveFeatureOnApply");
            Delegates.Add("RemoveFeatureOnApply",
                (target, componentData) =>
                    target.AddComponent(RemoveFeatureOnApplyDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: SavingThrowBonusAgainstDescriptor");
            Delegates.Add("SavingThrowBonusAgainstDescriptor",
                (target, componentData) =>
                    target.AddComponent(SavingThrowBonusAgainstDescriptorDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: SavingThrowBonusAgainstSpecificSpells");
            Delegates.Add("SavingThrowBonusAgainstSpecificSpells",
                (target, componentData) =>
                    target.AddComponent(SavingThrowBonusAgainstSpecificSpellsDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: SpecificBuffImmunity");
            Delegates.Add("SpecificBuffImmunity",
                (target, componentData) =>
                    target.AddComponent(SpecificBuffImmunityDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: SpellComponent");
            Delegates.Add("SpellComponent",
                (target, componentData) =>
                    target.AddComponent(SpellComponentDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: SpellDescriptorComponent");
            Delegates.Add("SpellDescriptorComponent",
                (target, componentData) =>
                    target.AddComponent(SpellDescriptorComponentDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: SpellImmunityToSpellDescriptor");
            Delegates.Add("SpellImmunityToSpellDescriptor",
                (target, componentData) =>
                    target.AddComponent(SpellImmunityToSpellDescriptorDelegate.CreateComponent(componentData)));
        }
    }
}
