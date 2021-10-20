using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using PF_Core.Extensions;

namespace PF_Classes.Transformations
{
    public class RemoveComponentFromJson : JsonTransformation
    {
        private static readonly Dictionary<string, Action<BlueprintScriptableObject>> _removeDelegates
            = new Dictionary<string, Action<BlueprintScriptableObject>>();

        public static void Remove(BlueprintScriptableObject target, string component)
        {
            _logger.Debug($"Removing component {component}");
            if (_removeDelegates.ContainsKey(component))
            {
                _removeDelegates[component](target);
            }
            _logger.Debug($"DONE: Removing component {component}");
        }

        static RemoveComponentFromJson()
        {
            _removeDelegates.Add("AbilityResourceLogic", target => target.RemoveComponents<AbilityResourceLogic>());
        }
    }
}
