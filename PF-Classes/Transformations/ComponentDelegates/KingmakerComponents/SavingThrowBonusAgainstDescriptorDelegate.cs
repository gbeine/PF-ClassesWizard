using Kingmaker.Designers.Mechanics.Facts;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class SavingThrowBonusAgainstDescriptorDelegate : AbstractComponentDelegate
    {
        public static SavingThrowBonusAgainstDescriptor CreateComponent(Component componentData)
        {
            SavingThrowBonusAgainstDescriptor c = _componentFactory.CreateComponent<SavingThrowBonusAgainstDescriptor>();

            c.Bonus = componentData.Exists("Bonus")
                ? componentData.AsInt("Bonus")
                : 0;
            c.Value = componentData.Exists("Value")
                ? componentData.AsInt("Value")
                : 0;
            c.ModifierDescriptor = EnumParser.parseModifierDescriptor(componentData.AsString("ModifierDescriptor"));
            c.SpellDescriptor = EnumParser.parseSpellDescriptor(componentData.AsString("SpellDescriptor"));

            return c;
        }
    }
}
