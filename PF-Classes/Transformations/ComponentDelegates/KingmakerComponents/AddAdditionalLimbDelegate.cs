using Kingmaker.UnitLogic.FactLogic;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AddAdditionalLimbDelegate : AbstractComponentDelegate
    {
        public static AddAdditionalLimb CreateComponent(Component componentData)
        {
            AddAdditionalLimb c = _componentFactory.CreateComponent<AddAdditionalLimb>();

            c.Weapon = getWeapon(componentData.AsString("Weapon"));

            return c;
        }
    }
}
