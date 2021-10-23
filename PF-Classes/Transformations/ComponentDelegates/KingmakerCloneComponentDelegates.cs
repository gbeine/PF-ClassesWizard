using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using PF_Core;
using PF_Core.Extensions;

namespace PF_Classes.Transformations.ComponentDelegates
{
    public class KingmakerCloneComponentDelegates
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly Dictionary<string, Action<BlueprintScriptableObject, BlueprintScriptableObject>> CloneComponentDelegates =
            new Dictionary<string, Action<BlueprintScriptableObject, BlueprintScriptableObject>>();

        public static bool CanClone(string component) => CloneComponentDelegates.ContainsKey(component);

        public static void Clone(string component, BlueprintScriptableObject target, BlueprintScriptableObject source) =>
                CloneComponentDelegates[component](target, source);

        private static void Clone<T>(BlueprintScriptableObject target, BlueprintScriptableObject source) where T : BlueprintComponent
        {
            _logger.Debug($"Cloning components of {typeof(T)} for {target.name}");
            foreach (var component in source.GetComponents<T>())
            {
                target.AddComponent(component);
            }
            _logger.Debug($"DONE: Cloning components of {typeof(T)} for {target.name}");
        }

        static KingmakerCloneComponentDelegates()
        {
            _logger.Debug("Adding delegate: AbilityTargetHasFact");
            CloneComponentDelegates.Add("AbilityTargetHasFact",
                (target, source) => Clone<AbilityTargetHasFact>(target, source));

            _logger.Debug("Adding delegate: AbilityTargetHasNoFactUnless");
            CloneComponentDelegates.Add("AbilityTargetHasNoFactUnless",
                (target, source) => Clone<AbilityTargetHasNoFactUnless>(target, source));
        }
    }
}
