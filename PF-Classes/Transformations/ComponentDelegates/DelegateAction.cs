using System;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using PF_Classes.JsonTypes;
using PF_Core;

namespace PF_Classes.Transformations.ComponentDelegates
{
    public class DelegateAction
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        public static void Add(Component component, BlueprintScriptableObject target, BlueprintCharacterClass blueprintCharacterClass)
        {
            _logger.Log($"Adding component from JSON data {component.Type} with CharacterClass");

            if (KingmakerCreateComponentCharacterClassDelegates.CanAdd(component.Type))
                KingmakerCreateComponentCharacterClassDelegates.Add(component, target, blueprintCharacterClass);
            else
                Add(component, target);

            _logger.Log($"DONE: Creating component from JSON data {component.Type} with CharacterClass");
        }

        public static void Add(Component component, BlueprintScriptableObject target)
        {
            _logger.Log($"Adding component from JSON data {component.Type}");

            if (KingmakerCreateComponentDelegates.CanAdd(component.Type))
                KingmakerCreateComponentDelegates.Add(component, target);
            else if (CallOfTheWildComponentDelegates.CanAdd(component.Type))
                CallOfTheWildComponentDelegates.Add(component, target);
            else
            {
                string message = $"Adding of {component} not possible, no delegates known";
                _logger.Error(message);
                throw new InvalidOperationException(message);
            }

            _logger.Log($"DONE: Creating component from JSON data {component.Type}");
        }

        public static void Remove(string component, BlueprintScriptableObject target)
        {
            _logger.Debug($"Removing components {component} for {target.name}");

            if (KingmakerRemoveComponentDelegates.CanRemove(component))
                KingmakerRemoveComponentDelegates.Remove(component, target);
            else
            {
                string message = $"Removing of {component} not possible, no delegates known";
                _logger.Error(message);
                throw new InvalidOperationException(message);
            }

            _logger.Debug($"DONE: Removing components {component} for {target.name}");
        }

        public static void Clone(string component, BlueprintScriptableObject target, BlueprintScriptableObject source)
        {
            _logger.Debug($"Cloning components {component} for {target.name}");

            if (KingmakerCloneComponentDelegates.CanClone(component))
                KingmakerCloneComponentDelegates.Clone(component, target, source);
            else
            {
                string message = $"Cloning of {component} not possible, no delegates known";
                _logger.Error(message);
                throw new InvalidOperationException(message);
            }

            _logger.Debug($"DONE: Cloning components {component} for {target.name}");
        }
    }
}
