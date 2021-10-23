using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;
using PF_Core;
using PF_Core.Extensions;

namespace PF_Classes.Transformations.ComponentDelegates
{
    public class CreateComponentCharacterClassDelegates
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly Dictionary<String, Action<BlueprintScriptableObject, Component, BlueprintCharacterClass>> Delegates =
            new Dictionary<string, Action<BlueprintScriptableObject, Component, BlueprintCharacterClass>>();

        public static bool CanAdd(string component) =>
            Delegates.ContainsKey(component);

        public static void Add(Component component, BlueprintScriptableObject target, BlueprintCharacterClass blueprintCharacterClass) =>
            Delegates[component.Type](target, component, blueprintCharacterClass);

        static CreateComponentCharacterClassDelegates()
        {
            _logger.Debug($"Adding delegate: AddFeatureOnClassLevel");
            Delegates.Add("AddFeatureOnClassLevel",
                (target, componentData, characterClass) =>
                    target.AddComponent(AddFeatureOnClassLevelDelegate.CreateComponent(componentData, characterClass)));

            _logger.Debug($"Adding delegate: AddKnownSpell");
            Delegates.Add("AddKnownSpell",
                (target, componentData, characterClass) =>
                    target.AddComponent(AddKnownSpellDelegate.CreateComponent(componentData, characterClass)));

            _logger.Debug($"Adding delegate: BindAbilitiesToClass");
            Delegates.Add("BindAbilitiesToClass",
                (target, componentData, characterClass) =>
                    target.AddComponent(BindAbilitiesToClassDelegate.CreateComponent(componentData, characterClass)));

            _logger.Debug($"Adding delegate: ContextRankConfig");
            Delegates.Add("ContextRankConfig",
                (target, componentData, characterClass) =>
                    target.AddComponent(ContextRankConfigDelegate.CreateComponent(componentData, characterClass)));

            _logger.Debug($"Adding delegate: LearnSpells");
            Delegates.Add("LearnSpells",
                (target, componentData, characterClass) =>
                    target.AddComponent(LearnSpellsDelegate.CreateComponent(componentData, characterClass)));
        }
    }
}
