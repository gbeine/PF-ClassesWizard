using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.UnitLogic.FactLogic;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AddFactsDelegate : AbstractComponentDelegate
    {
        public static AddFacts CreateComponent(Component componentData)
        {
            AddFacts c = _componentFactory.CreateComponent<AddFacts>();

            List<BlueprintUnitFact> facts = Array.Empty<BlueprintUnitFact>().ToList();
            string identifier = componentData.AsString("Fact");
            string name = "AddFacts$";
            if (spellbookExists(identifier))
            {
                BlueprintSpellbook spellbook = getSpellbook(identifier);
                name += spellbook.name;
                spellbook.SpellList.SpellsByLevel[0].Spells.ForEach(s => facts.Add(s));
            }
            else if (featureExists(identifier))
            {
                BlueprintFeature feature = getFeature(identifier);
                name += feature.name;
                facts.Add(feature);
            }
            else
            {
                string message = $"Not able to locate Fact: {identifier}";
                _logger.Error(message);
                throw new InvalidOperationException(message);
            }

            if (facts.Count > 0)
            {
                c.name = name;
                c.Facts = facts.ToArray();
            }

            return c;
        }
    }
}
