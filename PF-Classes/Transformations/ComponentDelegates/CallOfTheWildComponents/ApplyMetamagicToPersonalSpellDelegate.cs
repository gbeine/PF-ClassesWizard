using PF_CallOfTheWild.CallOfTheWild.MetamagicMechanics;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

namespace PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents
{
    public class ApplyMetamagicToPersonalSpellDelegate : AbstractComponentDelegate
    {
        public static ApplyMetamagicToPersonalSpell CreateComponent(Component componentData)
        {
            ApplyMetamagicToPersonalSpell c = _componentFactory.CreateComponent<ApplyMetamagicToPersonalSpell>();

            c.caster_level_increase = componentData.Exists("CasterLevelIncrease")
                ? componentData.AsInt("CasterLevelIncrease")
                : 0;
            if (componentData.Exists("Metamagic"))
            {
                c.metamagic = EnumParser.parseMetamagic(componentData.AsString("Metamagic"));
            }

            return c;
        }
    }
}
