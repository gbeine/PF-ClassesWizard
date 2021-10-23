using Kingmaker.UnitLogic.FactLogic;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AddDamageResistancePhysicalDelegate : AbstractComponentDelegate
    {
        public static AddDamageResistancePhysical CreateComponent(Component componentData)
        {
            AddDamageResistancePhysical c = _componentFactory.CreateComponent<AddDamageResistancePhysical>();

            c.BypassedByMagic = componentData.Exists("BypassedByMagic") && componentData.AsBool("BypassedByMagic");
            if (componentData.Exists("Value"))
                c.Value = componentData.AsInt("Value");

            return c;
        }
    }
}
