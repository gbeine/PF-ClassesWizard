using Kingmaker.Blueprints.Classes.Prerequisites;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class PrerequisiteNoFeatureDelegate : AbstractComponentDelegate
    {
        public static PrerequisiteNoFeature CreateComponent(Component componentData)
        {
            PrerequisiteNoFeature c = _componentFactory.CreateComponent<PrerequisiteNoFeature>();

            c.Feature = getFeature(componentData.AsString("Feature"));
            c.Group = Prerequisite.GroupType.All;

            return c;
        }
    }
}
