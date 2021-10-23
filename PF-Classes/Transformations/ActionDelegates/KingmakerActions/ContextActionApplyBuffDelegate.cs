using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

namespace PF_Classes.Transformations.ActionDelegates.KingmakerActions
{
    public class ContextActionApplyBuffDelegate : AbstractActionDelegate
    {
        public static ContextActionApplyBuff CreateAction(Action actionData)
        {
            ContextActionApplyBuff a = _actionFactory.CreateAction<ContextActionApplyBuff>();

            a.Buff = getBuff(actionData.AsString("Buff"));
            a.IsFromSpell = actionData.Exists("IsFromSpell") && actionData.AsBool("IsFromSpell");
            a.Permanent = actionData.Exists("Permanent") && actionData.AsBool("Permanent");
            a.IsNotDispelable = actionData.Exists("IsNotDispelable") && actionData.AsBool("IsNotDispelable");
            a.DurationSeconds = actionData.Exists("DurationSeconds") ? actionData.AsInt("DurationSeconds") : 0;
            a.UseDurationSeconds = a.DurationSeconds > 0;
            a.AsChild = !actionData.Exists("AsChild") || actionData.AsBool("AsChild");
            a.ToCaster = actionData.Exists("ToCaster") && actionData.AsBool("ToCaster");

            ContextValue bonusValue = _actionFactory.CreateContextValue(
                EnumParser.parseAbilityRankType(actionData.Duration.BonusValue));

            a.DurationValue = _actionFactory.CreateDurationValue(
                bonusValue,
                EnumParser.parseDurationRate(actionData.Duration.Rate),
                EnumParser.parseDiceType(actionData.Duration.DiceType),
                actionData.Duration.DiceCountValue);

            return a;
        }
    }
}
