using PF_CallOfTheWild.CallOfTheWild.SpellFailureMechanics;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

namespace PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents
{
    public class SilenceDelegate : AbstractComponentDelegate
    {
        public static Silence CreateComponent(Component componentData) =>
            _componentFactory.CreateComponent<Silence>();
    }
}
