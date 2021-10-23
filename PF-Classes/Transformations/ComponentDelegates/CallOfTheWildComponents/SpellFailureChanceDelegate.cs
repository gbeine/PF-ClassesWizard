using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;
using PF_Core.CallOfTheWild.SpellFailureMechanics;

namespace PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents
{
    public class SpellFailureChanceDelegate : AbstractComponentDelegate
    {
        public static SpellFailureChance CreateComponent(Component componentData)
        {
            SpellFailureChance c = _componentFactory.CreateComponent<SpellFailureChance>();

            c.chance = componentData.AsInt("Chance");
            c.do_not_spend_slot_if_failed = componentData.Exists("DoNotSpendSlotIfFailed") && componentData.AsBool("DoNotSpendSlotIfFailed");
            c.ignore_psychic = componentData.Exists("IgnorePsychic") && componentData.AsBool("IgnorePsychic");

            return c;
        }
    }
}
