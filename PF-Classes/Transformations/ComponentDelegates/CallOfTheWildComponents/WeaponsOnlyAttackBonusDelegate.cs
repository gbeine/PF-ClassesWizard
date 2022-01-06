using PF_CallOfTheWild.CallOfTheWild.NewMechanics;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

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
