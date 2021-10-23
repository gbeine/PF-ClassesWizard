using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;
using PF_Core.CallOfTheWild.UndeadMechanics;

namespace PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents
{
    public class ConsiderUndeadForHealingDelegate : AbstractComponentDelegate
    {
        public static ConsiderUndeadForHealing CreateComponent(Component componentData) =>
            _componentFactory.CreateComponent<ConsiderUndeadForHealing>();
    }
}
