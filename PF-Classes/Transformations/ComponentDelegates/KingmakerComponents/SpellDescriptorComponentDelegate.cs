using Kingmaker.Blueprints.Classes.Spells;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class SpellDescriptorComponentDelegate : AbstractComponentDelegate
    {
        public static SpellDescriptorComponent CreateComponent(Component componentData)
        {
            SpellDescriptorComponent c = _componentFactory.CreateComponent<SpellDescriptorComponent>();

            SpellDescriptor spellDescriptor = SpellDescriptor.None;
            foreach (var descriptor in componentData.AsArray("Descriptor"))
            {
                spellDescriptor |= EnumParser.parseSpellDescriptor(descriptor);
            }

            c.Descriptor = spellDescriptor;

            return c;
        }
    }
}
