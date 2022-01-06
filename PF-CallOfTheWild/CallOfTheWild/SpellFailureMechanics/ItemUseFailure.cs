using System;
using Kingmaker.Blueprints;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;

namespace PF_CallOfTheWild.CallOfTheWild.SpellFailureMechanics
{
    public class ItemUseFailure : OwnedGameLogicComponent<UnitDescriptor>, IInitiatorRulebookHandler<RuleCastSpell>, IRulebookHandler<RuleCastSpell>, IInitiatorRulebookSubscriber
    {
        public int chance;

        public void OnEventAboutToTrigger(RuleCastSpell evt)
        {
            if ((evt.Spell?.SourceItemUsableBlueprint == null) || (evt.Spell.StickyTouch != null))
                return;
            evt.SpellFailureChance = Math.Max(evt.SpellFailureChance, this.chance);
        }

        public void OnEventDidTrigger(RuleCastSpell evt)
        {

        }
    }
}
