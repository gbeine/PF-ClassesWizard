using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using PF_Core;
using PF_Core.Extensions;

namespace PF_Classes.Transformations.ComponentDelegates
{
    public class RemoveComponentDelegates
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly Dictionary<string, Action<BlueprintScriptableObject>> Delegates
            = new Dictionary<string, Action<BlueprintScriptableObject>>();

        public static bool CanRemove(string component) => Delegates.ContainsKey(component);

        public static void Remove(string component, BlueprintScriptableObject target) =>
            Delegates[component](target);

        static RemoveComponentDelegates()
        {
            Delegates.Add("AbilityResourceLogic", target => target.RemoveComponents<AbilityResourceLogic>());
        }
    }
}
