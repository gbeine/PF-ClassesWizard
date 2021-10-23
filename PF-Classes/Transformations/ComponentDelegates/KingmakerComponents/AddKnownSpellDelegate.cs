using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AddKnownSpellDelegate : AbstractComponentDelegate
    {
        public static AddKnownSpell CreateComponent(Component componentData, BlueprintCharacterClass blueprintCharacterClass)
        {
            AddKnownSpell c = _componentFactory.CreateComponent<AddKnownSpell>();

            c.Spell = getSpell(componentData.AsString("Spell"));
            c.SpellLevel = componentData.AsInt("SpellLevel");
            c.CharacterClass = blueprintCharacterClass;

            return c;
        }
    }
}
