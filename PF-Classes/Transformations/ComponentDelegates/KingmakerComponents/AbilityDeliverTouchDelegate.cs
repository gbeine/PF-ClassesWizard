using Kingmaker.UnitLogic.Abilities.Components;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AbilityDeliverTouchDelegate : AbstractComponentDelegate
    {
        public static AbilityDeliverTouch CreateComponent(Component componentData)
        {
            AbilityDeliverTouch c = _componentFactory.CreateComponent<AbilityDeliverTouch>();

            c.TouchWeapon = getWeapon(componentData.AsString("TouchWeapon"));

            return c;
        }
    }
}
