using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using PF_Core.Extensions;

namespace PF_Classes.Transformations.ComponentDelegates
{
    public class KingmakerComponentCloneDelegates : JsonTransformation
    {
        internal static readonly Dictionary<string, Action<BlueprintScriptableObject, BlueprintScriptableObject>> CloneComponentDelegates =
            new Dictionary<string, Action<BlueprintScriptableObject, BlueprintScriptableObject>>();

        static KingmakerComponentCloneDelegates()
        {
            _logger.Debug($"Adding delegate: AbilityTargetHasFact");
            CloneComponentDelegates.Add("AbilityTargetHasFact", (target, source) =>
            {
                foreach (var component in source.GetComponents<AbilityTargetHasFact>())
                {
                    target.AddComponent(component);
                }
            });

            _logger.Debug($"Adding delegate: AbilityTargetHasNoFactUnless");
            CloneComponentDelegates.Add("AbilityTargetHasNoFactUnless", (target, source) =>
            {
                foreach (var component in source.GetComponents<AbilityTargetHasNoFactUnless>())
                {
                    target.AddComponent(component);
                }
            });
        }
    }
}
