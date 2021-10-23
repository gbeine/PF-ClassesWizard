using System;
using System.Linq;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class NoSelectionIfAlreadyHasFeatureDelegate : AbstractComponentDelegate
    {
        public static NoSelectionIfAlreadyHasFeature CreateComponent(Component componentData)
        {
            NoSelectionIfAlreadyHasFeature c = _componentFactory.CreateComponent<NoSelectionIfAlreadyHasFeature>();

            c.AnyFeatureFromSelection = componentData.AsBool("AnyFeatureFromSelection");
            c.Features = componentData.Exists("Features")
                ? componentData.AsArray("Features")
                    .Select(f => getFeature(f))
                    .ToArray()
                : Array.Empty<BlueprintFeature>();

            return c;
        }
    }
}
