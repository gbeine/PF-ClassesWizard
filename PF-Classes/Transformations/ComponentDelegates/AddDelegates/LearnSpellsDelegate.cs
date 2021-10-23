using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.FactLogic;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.AddDelegates
{
    public class LearnSpellsDelegate : Delegate
    {
        public static LearnSpells CreateComponent(Component componentData, BlueprintCharacterClass blueprintCharacterClass)
        {
            LearnSpells c = _componentFactory.CreateComponent<LearnSpells>();

            c.CharacterClass = blueprintCharacterClass;

            if (componentData.Exists("Spellbook"))
            {
                BlueprintSpellbook spellbook = getSpellbook(componentData.AsString("Spellbook"));
                c.Spells = spellbook.SpellList.SpellsByLevel[0].Spells.ToArray();
            }
            else
            {
                if (blueprintCharacterClass.Spellbook != null)
                    c.Spells = blueprintCharacterClass.Spellbook.SpellList.SpellsByLevel[0].Spells.ToArray();
                else
                    _logger.Error("Invalid LearnSpells created: no spellbook defined and no spellbook exists for character class.");
            }

            return c;
        }
    }
}
