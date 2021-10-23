using Kingmaker.Designers.Mechanics.Facts;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AuraFeatureComponentDelegate : AbstractComponentDelegate
    {
        public static AuraFeatureComponent CreateComponent(Component componentData)
        {
            AuraFeatureComponent c = _componentFactory.CreateComponent<AuraFeatureComponent>();

            c.Buff = getBuff(componentData.AsString("Buff"));

            return c;
        }
    }
}
