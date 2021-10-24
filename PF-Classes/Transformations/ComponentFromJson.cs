using System;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates;
using PF_Core.Extensions;

namespace PF_Classes.Transformations
{
    public class ComponentFromJson : JsonTransformation
    {
        public static void ProcessComponents(BlueprintScriptableObject target, JsonType targetData) =>
            ProcessComponents(target, targetData, null);

        public static void ProcessComponents(BlueprintScriptableObject target, JsonType targetData, BlueprintCharacterClass blueprintCharacterClass)
        {
            _logger.Debug($"Processing components for {targetData.Name}");

            if (targetData.ResetComponents)
            {
                target.SetComponents(Array.Empty<BlueprintComponent>());
            }

            if (targetData.RemoveComponents.Count > 0)
            {
                _logger.Log("Removing components");
                foreach (var component in targetData.RemoveComponents)
                {
                    ComponentDelegate.Remove(component, target);
                }
            }

            if (targetData.Components.Count > 0)
            {
                _logger.Log("Adding components");
                foreach (var component in targetData.Components)
                {
                    _logger.Debug($"Adding component {component.Type}");
                    if (blueprintCharacterClass != null)
                        ComponentDelegate.Add(component, target, blueprintCharacterClass);
                    else
                        ComponentDelegate.Add(component, target);
                    _logger.Debug($"DONE: Adding component {component.Type}");
                }
            }

            if (targetData.ComponentsFrom.Count > 0)
            {
                _logger.Log("Adding components from other blueprints");
                foreach (var component in targetData.ComponentsFrom)
                {
                    _logger.Debug($"Adding component {component.Type}");
                    CloneComponent(target, component);
                    _logger.Debug($"DONE: Adding component {component.Type}");
                }
            }
            _logger.Debug($"DONE: Processing components for {targetData.Name}");
        }

        private static void CloneComponent(BlueprintScriptableObject target, Component componentData)
        {
            _logger.Log($"Creating component from blueprint {componentData.Type}");

            BlueprintScriptableObject source = null;

            if (_identifierLookup.existsSpell(componentData.AsString("From")))
                source = _spellbookRepository.GetSpell(_identifierLookup.lookupSpell(componentData.AsString("From")));
            // TODO: look for other items

            if (source != null)
                ComponentDelegate.Clone(componentData.Type, target, source);

            _logger.Log($"DONE: Creating component blueprint {componentData.Type}");
        }
    }
}
