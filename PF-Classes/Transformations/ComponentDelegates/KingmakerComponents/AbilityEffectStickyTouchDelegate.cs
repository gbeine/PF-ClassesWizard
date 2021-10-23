using Kingmaker.UnitLogic.Abilities.Components;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AbilityEffectStickyTouchDelegate : AbstractComponentDelegate
    {
        public static AbilityEffectStickyTouch CreateComponent(Component componentData)
        {
            AbilityEffectStickyTouch c = _componentFactory.CreateComponent<AbilityEffectStickyTouch>();

            c.TouchDeliveryAbility = getSpell(componentData.AsString("TouchDeliveryAbility"));

            return c;
        }
    }
}
