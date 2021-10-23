using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.FactLogic;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class SpellImmunityToSpellDescriptorDelegate : AbstractComponentDelegate
    {
        public static SpellImmunityToSpellDescriptor CreateComponent(Component componentData)
        {
            SpellImmunityToSpellDescriptor c = _componentFactory.CreateComponent<SpellImmunityToSpellDescriptor>();

            SpellDescriptor spellDescriptor = SpellDescriptor.None;
            foreach (var sd in componentData.AsArray("Descriptor"))
            {
                spellDescriptor |= EnumParser.parseSpellDescriptor(sd);
            }
            c.Descriptor = spellDescriptor;

            return c;
        }
    }
}
