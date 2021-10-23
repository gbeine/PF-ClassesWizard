using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;
using PF_Core.CallOfTheWild.NewMechanics;

namespace PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents
{
    public class WeaponsOnlyAttackBonusDelegate : AbstractComponentDelegate
    {
        public static WeaponsOnlyAttackBonus CreateComponent(Component componentData)
        {
            WeaponsOnlyAttackBonus c = _componentFactory.CreateComponent<WeaponsOnlyAttackBonus>();

            c.Bonus = componentData.AsInt("Bonus");

            return c;
        }
    }
}
