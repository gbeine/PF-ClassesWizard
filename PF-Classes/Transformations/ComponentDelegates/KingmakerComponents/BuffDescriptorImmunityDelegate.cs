using Kingmaker.UnitLogic.FactLogic;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class BuffDescriptorImmunityDelegate : AbstractComponentDelegate
    {
        public static BuffDescriptorImmunity CreateComponent(Component componentData)
        {
            BuffDescriptorImmunity c = _componentFactory.CreateComponent<BuffDescriptorImmunity>();

            c.Descriptor = EnumParser.parseSpellDescriptor(componentData.AsString("Descriptor"));

            return c;
        }
    }
}
