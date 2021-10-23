using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Utility;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class BlindsenseDelegate : AbstractComponentDelegate
    {
        public static Blindsense CreateComponent(Component componentData)
        {
            Blindsense c = _componentFactory.CreateComponent<Blindsense>();

            c.Range = componentData.AsInt("Range").Feet();
            c.Blindsight = componentData.Exists("Blindsight") && componentData.AsBool("Blindsight");

            return c;
        }
    }
}
