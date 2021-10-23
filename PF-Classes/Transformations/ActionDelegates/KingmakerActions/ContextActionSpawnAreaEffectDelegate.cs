using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

namespace PF_Classes.Transformations.ActionDelegates.KingmakerActions
{
    public class ContextActionSpawnAreaEffectDelegate : AbstractActionDelegate
    {
        // This parses JSON action data for SpawnAreaEffect
        //
        //     "Type": "ContextActionSpawnAreaEffect",
        //     "AreaEffect": "loc:WallOfFireSpellAbilityArea",
        //     "BonusValue": "Default",
        //     "Rate": "Rounds",
        //     "DiceType": "Zero",
        //     "DiceCountValue": "0"
        public static ContextActionSpawnAreaEffect CreateAction(Action actionData)
        {
            ContextActionSpawnAreaEffect a = _actionFactory.CreateAction<ContextActionSpawnAreaEffect>();

            a.AreaEffect = null; // TODO: actionData.AsString("AreaEffect");

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
