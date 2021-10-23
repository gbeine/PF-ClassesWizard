using Kingmaker.UnitLogic.FactLogic;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AddConditionDelegate : AbstractComponentDelegate
    {
        public static AddCondition CreateComponent(Component componentData)
        {
            AddCondition c = _componentFactory.CreateComponent<AddCondition>();

            c.Condition = EnumParser.parseUnitCondition(componentData.AsString("Condition"));

            return c;
        }
    }
}
