using Kingmaker.Blueprints.Classes.Spells;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class SpellComponentDelegate : AbstractComponentDelegate
    {
        public static SpellComponent CreateComponent(Component componentData)
        {
            SpellComponent c = _componentFactory.CreateComponent<SpellComponent>();

            if (componentData.Exists("School"))
                c.School = EnumParser.parseSpellSchool(componentData.AsString("School"));

            return c;
        }
    }
}
