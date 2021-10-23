using System.Linq;
using Kingmaker.UnitLogic.FactLogic;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AddProficienciesDelegate : AbstractComponentDelegate
    {
        public static AddProficiencies CreateComponent(Component componentData)
        {
            AddProficiencies c = _componentFactory.CreateComponent<AddProficiencies>();

            if (componentData.Exists("WeaponProficiencies"))
                c.WeaponProficiencies = componentData.AsArray("WeaponProficiencies")
                    .Select(w => EnumParser.parseWeaponCategory(w)).ToArray();
            if (componentData.Exists("ArmorProficiencies"))
                c.ArmorProficiencies = componentData.AsArray("ArmorProficiencies")
                    .Select(a => EnumParser.parseArmorProficiency(a)).ToArray();

            return c;
        }
    }
}
