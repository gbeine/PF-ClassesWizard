using Kingmaker.Designers.Mechanics.Facts;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class RemoveFeatureOnApplyDelegate : AbstractComponentDelegate
    {
        public static RemoveFeatureOnApply CreateComponent(Component componentData)
        {
            RemoveFeatureOnApply c = _componentFactory.CreateComponent<RemoveFeatureOnApply>();

            c.Feature = getFeature(componentData.AsString("Feature"));

            return c;
        }
    }
}
