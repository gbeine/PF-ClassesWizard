using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AddStatBonusDelegate : AbstractComponentDelegate
    {
        public static AddStatBonus CreateComponent(Component componentData)
        {
            AddStatBonus c = _componentFactory.CreateComponent<AddStatBonus>();

            c.Stat = EnumParser.parseStatType(componentData.AsString("Stat"));
            c.Value = componentData.AsInt("Bonus");
            c.Descriptor = componentData.Exists("Descriptor")
                ? EnumParser.parseModifierDescriptor(componentData.AsString("Descriptor"))
                : ModifierDescriptor.UntypedStackable;

            return c;
        }
    }
}
