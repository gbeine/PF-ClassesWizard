using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class BindAbilitiesToClassDelegate : AbstractComponentDelegate
    {
        public static BindAbilitiesToClass CreateComponent(BlueprintSpellbook spellbook, BlueprintCharacterClass characterClass, bool cantrips = false, int levelStep = 1)
        {
            BindAbilitiesToClass c = _componentFactory.CreateComponent<BindAbilitiesToClass>();

            c.CharacterClass = characterClass;
            c.LevelStep = levelStep;
            c.Cantrip = cantrips;
            c.Abilites = spellbook.SpellList.SpellsByLevel[0].Spells.ToArray();
            c.Stat = spellbook.CastingAttribute;

            return c;
        }

        public static BindAbilitiesToClass CreateComponent(Component componentData, BlueprintCharacterClass blueprintCharacterClass)
        {
            BindAbilitiesToClass c = _componentFactory.CreateComponent<BindAbilitiesToClass>();

            c.CharacterClass = blueprintCharacterClass;

            if (componentData.Exists("Spellbook"))
            {
                BlueprintSpellbook spellbook = getSpellbook(componentData.AsString("Spellbook"));
                c.Abilites = spellbook.SpellList.SpellsByLevel[0].Spells.ToArray();
                c.Stat = spellbook.CastingAttribute;
            }
            else
            {
                if (blueprintCharacterClass.Spellbook != null)
                {
                    c.Abilites = blueprintCharacterClass.Spellbook.SpellList.SpellsByLevel[0].Spells.ToArray();
                    c.Stat = blueprintCharacterClass.Spellbook.CastingAttribute;
                }
            }

            if (componentData.Exists("LevelStep"))
                c.LevelStep = componentData.AsInt("LevelStep");
            if (componentData.Exists("Cantrip"))
                c.Cantrip = componentData.AsBool("Cantrip");

            c.Archetypes = Array.Empty<BlueprintArchetype>(); // TODO: implement
            c.AdditionalClasses = Array.Empty<BlueprintCharacterClass>(); // TODO: implement

            return c;
        }
    }
}
