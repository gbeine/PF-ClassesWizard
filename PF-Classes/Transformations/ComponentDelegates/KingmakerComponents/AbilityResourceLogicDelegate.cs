using Kingmaker.UnitLogic.Abilities.Components;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AbilityResourceLogicDelegate : AbstractComponentDelegate
    {
        public static AbilityResourceLogic CreateComponent(Component componentData) =>
            _componentFactory.CreateComponent<AbilityResourceLogic>();
    }
}
