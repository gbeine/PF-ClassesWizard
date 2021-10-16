using System;
using System.Collections.Generic;
using Harmony12;
using Kingmaker.Blueprints;

namespace PF_Core.Extensions
{
    public static class BlueprintScriptableObjectExtensions
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly SaveCompatibility _saveCompatibility = SaveCompatibility.INSTANCE;

        public static void SetComponents(this BlueprintScriptableObject blueprintScriptableObject,
            params BlueprintComponent[] components)
        {
            // Fix names of components.
            // Generally this doesn't matter, but if they have serialization state, then their name needs to be unique.
            var names = new HashSet<string>();
            foreach (var component in components)
            {
                if (string.IsNullOrEmpty(component.name))
                {
                    component.name = $"${component.GetType().Name}";
                }

                if (!names.Add(component.name))
                {
                    _saveCompatibility.CheckComponent(blueprintScriptableObject, component);
                    String name;
                    for (int i = 0; !names.Add(name = $"{component.name}${i}"); i++) ;
                    component.name = name;
                }
            }

            blueprintScriptableObject.ComponentsArray = components;
        }

        public static void AddComponent(this BlueprintScriptableObject blueprintScriptableObject, BlueprintComponent component)
        {
            blueprintScriptableObject.SetComponents(blueprintScriptableObject.ComponentsArray.AddToArray(component));
        }

        public static void ReplaceComponent<T>(this BlueprintScriptableObject blueprintScriptableObject, Action<T> action) where T : BlueprintComponent
        {
            var replacement = blueprintScriptableObject.GetComponent<T>().CreateCopy();
            action(replacement);
            ReplaceComponent(blueprintScriptableObject, blueprintScriptableObject.GetComponent<T>(), replacement);
        }

        private static void ReplaceComponent(this BlueprintScriptableObject blueprintScriptableObject, BlueprintComponent original, BlueprintComponent replacement)
        {
            // Note: make a copy so we don't mutate the original component
            // (in case it's a clone of a game one).
            var components = blueprintScriptableObject.ComponentsArray;
            var newComponents = new BlueprintComponent[components.Length];
            for (int i = 0; i < components.Length; i++)
            {
                var c = components[i];
                newComponents[i] = c == original ? replacement : c;
            }
            blueprintScriptableObject.SetComponents(newComponents); // fix up names if needed
        }
    }
}
