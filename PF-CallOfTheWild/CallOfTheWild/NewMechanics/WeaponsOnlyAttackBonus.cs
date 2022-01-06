using Kingmaker.Blueprints;
using Kingmaker.Enums;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Mechanics;

namespace PF_CallOfTheWild.CallOfTheWild.NewMechanics
{
    public class WeaponsOnlyAttackBonus : RuleInitiatorLogicComponent<RuleCalculateAttackBonusWithoutTarget>
    {
        public ContextValue Bonus;

        public override void OnEventAboutToTrigger(RuleCalculateAttackBonusWithoutTarget evt)
        {
            if (evt.Weapon == null)
                return;

            if (evt.Weapon.Blueprint.IsNatural || evt.Weapon.Blueprint.IsUnarmed)
                return;

            if (evt.Weapon.Blueprint.Category == WeaponCategory.Ray || evt.Weapon.Blueprint.Category == WeaponCategory.Touch)
                return;

            evt.AddBonus(Bonus.Calculate(this.Fact.MaybeContext), this.Fact);
        }

        public override void OnEventDidTrigger(RuleCalculateAttackBonusWithoutTarget evt)
        {
        }
    }
}
