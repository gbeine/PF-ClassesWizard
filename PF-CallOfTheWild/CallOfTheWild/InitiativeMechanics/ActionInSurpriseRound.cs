using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace PF_CallOfTheWild.CallOfTheWild.InitiativeMechanics
{
    public class ActionInSurpriseRound : OwnedGameLogicComponent<UnitDescriptor>, IUnitInitiativeHandler
    {
        public ActionList actions;
        public void HandleUnitRollsInitiative(RuleInitiativeRoll rule)
        {
            if (rule.Initiator.Descriptor != Owner) return;

            // Are there other units not waiting on inititative?
            foreach (var unit in Game.Instance.State.Units.InCombat().CombatStates())
            {
                if (unit.IsWaitingInitiative) continue;
                //we are in surprise round
                (this.Fact as IFactContextOwner).RunActionInContext(actions, this.Owner.Unit);
                return;
            }
        }
    }
}
