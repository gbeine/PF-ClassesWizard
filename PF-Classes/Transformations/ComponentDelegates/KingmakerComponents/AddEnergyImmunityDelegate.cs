using Kingmaker.UnitLogic.FactLogic;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AddEnergyImmunityDelegate : AbstractComponentDelegate
    {
        public static AddEnergyImmunity CreateComponent(Component componentData)
        {
            AddEnergyImmunity c = _componentFactory.CreateComponent<AddEnergyImmunity>();

            c.Type = EnumParser.parseDamageEnergyType(componentData.AsString("DamageEnergyType"));

            return c;
        }
    }
}
