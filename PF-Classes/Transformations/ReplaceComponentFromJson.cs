using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using PF_Classes.JsonTypes;
using PF_Core.Extensions;

namespace PF_Classes.Transformations
{
    public class ReplaceComponentFromJson : JsonTransformation
    {
        private static readonly Dictionary<string, Action<BlueprintScriptableObject, Component>> _replaceDelegates
            = new Dictionary<string, Action<BlueprintScriptableObject, Component>>();

        public static void ReplaceComponent(BlueprintScriptableObject target, Component component)
        {
            if (_replaceDelegates.ContainsKey(component.Type))
            {
                _replaceDelegates[component.Type](target, component);
            }
        }

        static ReplaceComponentFromJson()
        {
            _replaceDelegates.Add("AbilityEffectRunAction", (target, component) =>
                target.ReplaceComponent<AbilityEffectRunAction>(
                    c =>
                    {
                        c.Actions = null; // TODO: Helpers.CreateActionList(spawn_area)
                    }));
        }
    }
}
