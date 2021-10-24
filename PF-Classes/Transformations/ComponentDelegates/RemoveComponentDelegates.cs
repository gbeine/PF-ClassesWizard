using Kingmaker.Blueprints;
using PF_Core.Extensions.JaethalsMagic;

namespace PF_Classes.Transformations.ComponentDelegates
{
    public class RemoveComponentDelegates
    {
        public static bool CanRemove(string component) =>
            RemoveComponents.CanRemove(component);

        public static void Remove(string component, BlueprintScriptableObject target) =>
            RemoveComponents.Remove(component, target);
    }
}
