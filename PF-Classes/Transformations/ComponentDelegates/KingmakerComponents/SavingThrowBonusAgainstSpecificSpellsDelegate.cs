using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class SavingThrowBonusAgainstSpecificSpellsDelegate : AbstractComponentDelegate
    {
        public static SavingThrowBonusAgainstSpecificSpells CreateComponent(Component componentData)
        {
            SavingThrowBonusAgainstSpecificSpells c = _componentFactory.CreateComponent<SavingThrowBonusAgainstSpecificSpells>();

            c.Value = componentData.Exists("Value")
                ? componentData.AsInt("Value")
                : 0;


            if (componentData.Exists("ModifierDescriptor"))
                c.ModifierDescriptor = EnumParser.parseModifierDescriptor(componentData.AsString("ModifierDescriptor"));

            List<BlueprintAbility> spells = Array.Empty<BlueprintAbility>().ToList();

            if (componentData.Exists("Spells"))
            {
                foreach (var spell in componentData.AsArray("Spells"))
                {
                    spells.Add(getSpell(spell));
                }
            }

            c.Spells = spells.ToArray();

            List<BlueprintFeature> bypassFeatures = Array.Empty<BlueprintFeature>().ToList();

            if (componentData.Exists("BypassFeatures"))
            {
                foreach (var feature in componentData.AsArray("BypassFeatures"))
                {
                    bypassFeatures.Add(getFeature(feature));
                }
            }
            c.BypassFeatures = bypassFeatures.ToArray();

            return c;
        }
    }
}
