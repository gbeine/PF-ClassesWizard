using Kingmaker.Utility;
using PF_CallOfTheWild.CallOfTheWild.ConcealementMechanics;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

namespace PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents
{
    public class SetVisibilityLimitDelegate : AbstractComponentDelegate
    {
        public static SetVisibilityLimit CreateComponent(Component componentData)
        {
            SetVisibilityLimit c = _componentFactory.CreateComponent<SetVisibilityLimit>();

            c.visibility_limit = componentData.AsInt("VisibilityLimit").Feet();

            return c;
        }
    }
}
