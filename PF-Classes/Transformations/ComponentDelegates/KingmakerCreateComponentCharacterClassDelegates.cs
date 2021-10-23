using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.AddDelegates;
using PF_Core.Extensions;

namespace PF_Classes.Transformations.ComponentDelegates
{
    public class KingmakerCreateComponentCharacterClassDelegates : JsonTransformation
    {
        private static readonly Dictionary<String, Action<BlueprintScriptableObject, Component, BlueprintCharacterClass>> CreateComponentCharacterClassDelegates =
            new Dictionary<string, Action<BlueprintScriptableObject, Component, BlueprintCharacterClass>>();

        public static bool CanAdd(string component) =>
            CreateComponentCharacterClassDelegates.ContainsKey(component);

        public static void Add(Component component, BlueprintScriptableObject target, BlueprintCharacterClass blueprintCharacterClass) =>
            CreateComponentCharacterClassDelegates[component.Type](target, component, blueprintCharacterClass);

        static KingmakerCreateComponentCharacterClassDelegates()
        {
            _logger.Debug($"Adding delegate: AddFeatureOnClassLevel");
            CreateComponentCharacterClassDelegates.Add("AddFeatureOnClassLevel",
                (target, componentData, characterClass) =>
                    target.AddComponent(AddFeatureOnClassLevelDelegate.CreateComponent(componentData, characterClass)));

            _logger.Debug($"Adding delegate: AddKnownSpell");
            CreateComponentCharacterClassDelegates.Add("AddKnownSpell",
                (target, componentData, characterClass) =>
                    target.AddComponent(AddKnownSpellDelegate.CreateComponent(componentData, characterClass)));

            _logger.Debug($"Adding delegate: BindAbilitiesToClass");
            CreateComponentCharacterClassDelegates.Add("BindAbilitiesToClass",
                (target, componentData, characterClass) =>
                    target.AddComponent(BindAbilitiesToClassDelegate.CreateComponent(componentData, characterClass)));

            _logger.Debug($"Adding delegate: ContextRankConfig");
            CreateComponentCharacterClassDelegates.Add("ContextRankConfig",
                (target, componentData, characterClass) =>
                    target.AddComponent(ContextRankConfigDelegate.CreateComponent(componentData, characterClass)));

            _logger.Debug($"Adding delegate: LearnSpells");
            CreateComponentCharacterClassDelegates.Add("LearnSpells",
                (target, componentData, characterClass) =>
                    target.AddComponent(LearnSpellsDelegate.CreateComponent(componentData, characterClass)));
        }
    }
}
