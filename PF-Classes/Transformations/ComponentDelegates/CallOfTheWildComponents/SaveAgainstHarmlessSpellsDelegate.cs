using PF_CallOfTheWild.CallOfTheWild.HarmlessSaves;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

namespace PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents
{
    public class SaveAgainstHarmlessSpellsDelegate : AbstractComponentDelegate
    {
        public static SaveAgainstHarmlessSpells CreateComponent(Component componentData) =>
            _componentFactory.CreateComponent<SaveAgainstHarmlessSpells>();
    }
}
