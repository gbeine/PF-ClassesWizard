using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents;
using PF_Core.Extensions;

namespace PF_Classes.Transformations.ComponentDelegates
{
    public class CallOfTheWildComponentDelegates : JsonTransformation
    {
        private static readonly Dictionary<String, Action<BlueprintScriptableObject, Component>> Delegates =
            new Dictionary<string, Action<BlueprintScriptableObject, Component>>();

        public static bool CanAdd(string component) =>
            Delegates.ContainsKey(component);

        public static void Add(Component component, BlueprintScriptableObject target) =>
            Delegates[component.Type](target, component);

        static CallOfTheWildComponentDelegates()
        {
            _logger.Debug($"Adding delegate: ActionInSurpriseRound");
            Delegates.Add("ActionInSurpriseRound",
                (target, componentData) =>
                    target.AddComponent(ActionInSurpriseRoundDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AddOutgoingConcealment");
            Delegates.Add("AddOutgoingConcealment",
                (target, componentData) =>
                    target.AddComponent(AddOutgoingConcealmentDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: AddSpeedBonusBasedOnRaceSize");
            Delegates.Add("AddSpeedBonusBasedOnRaceSize",
                (target, componentData) =>
                    target.AddComponent(AddSpeedBonusBasedOnRaceSizeDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: ApplyMetamagicToPersonalSpell");
            Delegates.Add("ApplyMetamagicToPersonalSpell",
                (target, componentData) =>
                    target.AddComponent(ApplyMetamagicToPersonalSpellDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: ConsiderUndeadForHealing");
            Delegates.Add("ConsiderUndeadForHealing",
                (target, componentData) =>
                    target.AddComponent(ConsiderUndeadForHealingDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: ContextIncreaseDescriptorSpellsDC");
            Delegates.Add("ContextIncreaseDescriptorSpellsDC",
                (target, componentData) =>
                    target.AddComponent(ContextIncreaseDescriptorSpellsDCDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: IgnoreEncumbrence");
            Delegates.Add("IgnoreEncumbrence",
                (target, componentData) =>
                    target.AddComponent(IgnoreEncumbrenceDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: ItemUseFailure");
            Delegates.Add("ItemUseFailure",
                (target, componentData) =>
                    target.AddComponent(ItemUseFailureDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: RunActionOnCombatStart");
            Delegates.Add("RunActionOnCombatStart",
                (target, componentData) =>
                    target.AddComponent(RunActionOnCombatStartDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: SaveAgainstHarmlessSpells");
            Delegates.Add("SaveAgainstHarmlessSpells",
                (target, componentData) =>
                    target.AddComponent(SaveAgainstHarmlessSpellsDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: SetVisibilityLimit");
            Delegates.Add("SetVisibilityLimit",
                (target, componentData) =>
                    target.AddComponent(SetVisibilityLimitDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: Silence");
            Delegates.Add("Silence",
                (target, componentData) =>
                    target.AddComponent(SilenceDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: SpellFailureChance");
            Delegates.Add("SpellFailureChance",
                (target, componentData) =>
                    target.AddComponent(SpellFailureChanceDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: SuppressBuffsCorrect");
            Delegates.Add("SuppressBuffsCorrect",
                (target, componentData) =>
                    target.AddComponent(SuppressBuffsCorrectDelegate.CreateComponent(componentData)));

            _logger.Debug($"Adding delegate: WeaponsOnlyAttackBonus");
            Delegates.Add("WeaponsOnlyAttackBonus",
                (target, componentData) =>
                    target.AddComponent(WeaponsOnlyAttackBonusDelegate.CreateComponent(componentData)));
        }
    }
}
