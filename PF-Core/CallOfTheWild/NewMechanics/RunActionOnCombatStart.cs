using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace PF_Core.CallOfTheWild.NewMechanics
{
    [AllowedOn(typeof(BlueprintUnitFact))]
    public class RunActionOnCombatStart : RuleInitiatorLogicComponent<RuleInitiativeRoll>
    {
        public ActionList actions;

        public override void OnEventAboutToTrigger(RuleInitiativeRoll evt)
        {
            (Fact as IFactContextOwner).RunActionInContext(actions, Owner.Unit);
        }

        public override void OnEventDidTrigger(RuleInitiativeRoll evt)
        {
        }
    }
}
