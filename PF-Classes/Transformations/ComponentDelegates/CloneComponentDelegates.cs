using Kingmaker.Blueprints;
using PF_Core.Extensions.JaethalsMagic;

namespace PF_Classes.Transformations.ComponentDelegates
{
    public class CloneComponentDelegates
    {
        public static bool CanClone(string component) =>
            CloneComponents.CanClone(component);

        public static void Clone(string component, BlueprintScriptableObject target, BlueprintScriptableObject source) =>
            CloneComponents.Clone(component, target, source);
    }
}
