using Kingmaker.UnitLogic.FactLogic;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class SpecificBuffImmunityDelegate : AbstractComponentDelegate
    {
        public static SpecificBuffImmunity CreateComponent(Component componentData)
        {
            SpecificBuffImmunity c = _componentFactory.CreateComponent<SpecificBuffImmunity>();

            c.Buff = getBuff(componentData.AsString("Buff"));

            return c;
        }
    }
}
