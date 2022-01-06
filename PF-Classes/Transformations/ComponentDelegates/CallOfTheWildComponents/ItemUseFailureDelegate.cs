using PF_CallOfTheWild.CallOfTheWild.SpellFailureMechanics;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

namespace PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents
{
    public class ItemUseFailureDelegate : AbstractComponentDelegate
    {
        public static ItemUseFailure CreateComponent(Component componentData)
        {
            ItemUseFailure c = _componentFactory.CreateComponent<ItemUseFailure>();

            c.chance = componentData.AsInt("Chance");

            return c;
        }
    }
}
