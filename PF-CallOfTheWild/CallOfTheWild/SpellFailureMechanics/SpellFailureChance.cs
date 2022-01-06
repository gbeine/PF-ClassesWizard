using Kingmaker.Blueprints;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;

namespace PF_CallOfTheWild.CallOfTheWild.SpellFailureMechanics
{
    public class SpellFailureChance : OwnedGameLogicComponent<UnitDescriptor>, IInitiatorRulebookHandler<RuleCastSpell>, IRulebookHandler<RuleCastSpell>, IInitiatorRulebookSubscriber
    {
        public ContextValue chance;
        public bool do_not_spend_slot_if_failed = false;
        public bool ignore_psychic = false;

        public void OnEventAboutToTrigger(RuleCastSpell evt)
        {
            if (!evt.Spell.Blueprint.IsSpell || evt.Spell.Spellbook == null || (evt.Spell.StickyTouch != null))
                return;

            if (ignore_psychic && evt.Spell.Spellbook.Blueprint.GetComponent<SpellbookMechanics.PsychicSpellbook>() != null)
            {
                return;
            }
            int threshold = this.chance.Calculate(this.Fact.MaybeContext);
            int d100 = RulebookEvent.Dice.D100;
            //Main.logger.Log($"Failure: {d100}/{threshold}");
            if (d100 > threshold)
            {
                return;
            }
            evt.SpellFailureChance = 200;
            if (do_not_spend_slot_if_failed)
            {
                evt.Spell.Caster.Ensure<SpellbookMechanics.UnitPartDoNotSpendNextSpell>().active = true;
            }
        }

        public void OnEventDidTrigger(RuleCastSpell evt) { }
    }
}
