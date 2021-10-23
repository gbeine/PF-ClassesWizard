using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AddSpellResistanceDelegate : AbstractComponentDelegate
    {
        public static AddSpellResistance CreateComponent(Component componentData)
        {
            AddSpellResistance c = _componentFactory.CreateComponent<AddSpellResistance>();

            c.Value = new ContextValue()
            {
                ValueType = ContextValueType.Rank,
                ValueRank = componentData.Exists("Rank")
                    ? EnumParser.parseAbilityRankType(componentData.AsString("Rank"))
                    : AbilityRankType.Default
            };

            return c;
        }
    }
}
