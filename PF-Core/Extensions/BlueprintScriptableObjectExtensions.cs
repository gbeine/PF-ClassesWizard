using System;
using System.Collections.Generic;
using System.Linq;
using Harmony12;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using PF_Core.Facades;

namespace PF_Core.Extensions
{
    public static class BlueprintScriptableObjectExtensions
    {
        private static readonly Harmony.FastSetter blueprintScriptableObject_set_AssetId = Harmony.CreateFieldSetter<BlueprintScriptableObject>("m_AssetGuid");

        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly SaveCompatibility _saveCompatibility = SaveCompatibility.INSTANCE;
        private static readonly Dictionary<string, Action<BlueprintScriptableObject>> _removeComponents = new Dictionary<string, Action<BlueprintScriptableObject>>();

        public static void SetAssetId(this BlueprintScriptableObject blueprintScriptableObject, String assetId) =>
            blueprintScriptableObject_set_AssetId(blueprintScriptableObject, assetId);

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

        public static void RemoveComponents(this BlueprintScriptableObject blueprintScriptableObject, string component)
        {
            if (_removeComponents.ContainsKey(component))
            {
                _removeComponents[component](blueprintScriptableObject);
            }
        }

        public static void RemoveComponents<T>(this BlueprintScriptableObject blueprintScriptableObject) where T : BlueprintComponent
        {
            var compnents_to_remove = blueprintScriptableObject.GetComponents<T>().ToArray();
            foreach (var c in compnents_to_remove)
            {
                blueprintScriptableObject.SetComponents(blueprintScriptableObject.ComponentsArray.RemoveFromArray(c));
            }
        }

        public static void ReplaceComponent<T>(this BlueprintScriptableObject blueprintScriptableObject, Action<T> action) where T : BlueprintComponent
        {
            var replacement = blueprintScriptableObject.GetComponent<T>().CreateCopy();
            action(replacement);
            ReplaceComponent(blueprintScriptableObject, blueprintScriptableObject.GetComponent<T>(), replacement);
        }

        internal static void ReplaceComponent(this BlueprintScriptableObject blueprintScriptableObject, BlueprintComponent original, BlueprintComponent replacement)
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

        static BlueprintScriptableObjectExtensions()
        {
            _removeComponents.Add("AbilityResourceLogic", o => o.RemoveComponents<AbilityResourceLogic>());
        }
    }
}
