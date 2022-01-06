using PF_CallOfTheWild.CallOfTheWild.NewMechanics;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

namespace PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents
{
    public class ContextIncreaseDescriptorSpellsDCDelegate : AbstractComponentDelegate
    {
        public static ContextIncreaseDescriptorSpellsDC CreateComponent(Component componentData)
        {
            ContextIncreaseDescriptorSpellsDC c = _componentFactory.CreateComponent<ContextIncreaseDescriptorSpellsDC>();

            c.Value = componentData.AsInt("Value");
            c.Descriptor = EnumParser.parseSpellDescriptor(componentData.AsString("Descriptor"));

            return c;
        }
    }
}
