using PF_CallOfTheWild.CallOfTheWild.EncumbranceMechanics;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

namespace PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents
{
    public class IgnoreEncumbrenceDelegate : AbstractComponentDelegate
    {
        public static IgnoreEncumbrence CreateComponent(Component componentData) =>
            _componentFactory.CreateComponent<IgnoreEncumbrence>();
    }
}
