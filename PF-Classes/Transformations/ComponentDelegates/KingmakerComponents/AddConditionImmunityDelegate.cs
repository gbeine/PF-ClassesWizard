using Kingmaker.UnitLogic.FactLogic;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AddConditionImmunityDelegate : AbstractComponentDelegate
    {
        public static AddConditionImmunity CreateComponent(Component componentData)
        {
            AddConditionImmunity c = _componentFactory.CreateComponent<AddConditionImmunity>();

            c.Condition = EnumParser.parseUnitCondition(componentData.AsString("Condition"));

            return c;
        }
    }
}
