using Kingmaker.Blueprints.Classes.Spells;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class SpellListComponentDelegate : AbstractComponentDelegate
    {
        public static SpellListComponent CreateComponent(BlueprintSpellList spellList, int spellLevel)
        {
            SpellListComponent c = _componentFactory.CreateComponent<SpellListComponent>();

            c.SpellLevel = spellLevel;
            c.SpellList = spellList;

            return c;
        }
    }
}
