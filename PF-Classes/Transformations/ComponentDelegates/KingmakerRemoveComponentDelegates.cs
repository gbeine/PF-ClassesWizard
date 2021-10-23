using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using PF_Core.Extensions;

namespace PF_Classes.Transformations.ComponentDelegates
{
    public class KingmakerRemoveComponentDelegates : JsonTransformation
    {
        private static readonly Dictionary<string, Action<BlueprintScriptableObject>> RemoveComponentDelegates
            = new Dictionary<string, Action<BlueprintScriptableObject>>();

        public static bool CanRemove(string component) => RemoveComponentDelegates.ContainsKey(component);

        public static void Remove(string component, BlueprintScriptableObject target) =>
            RemoveComponentDelegates[component](target);

        static KingmakerRemoveComponentDelegates()
        {
            RemoveComponentDelegates.Add("AbilityResourceLogic", target => target.RemoveComponents<AbilityResourceLogic>());
        }
    }
}
