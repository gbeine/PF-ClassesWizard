using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;
using PF_Core.CallOfTheWild.EncumbranceMechanics;

namespace PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents
{
    public class IgnoreEncumbrenceDelegate : AbstractComponentDelegate
    {
        public static IgnoreEncumbrence CreateComponent(Component componentData) =>
            _componentFactory.CreateComponent<IgnoreEncumbrence>();
    }
}
