using System;
using System.Collections.Generic;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class ActionFromJson : JsonTransformation
    {
        private static readonly ActionFactory _actionFactory = ActionFactory.INSTANCE;

        private static readonly Dictionary<String, Func<JsonTypes.Action, GameAction>> _createActionDelegates =
            new Dictionary<string, Func<JsonTypes.Action, GameAction>>();

        public static GameAction CreateAction(JsonTypes.Action actionData)
        {
            _logger.Debug($"Create action {actionData.Type}");
            GameAction action = _createActionDelegates[actionData.Type](actionData);

            _logger.Debug($"DONE: Create action {actionData.Type}");
            return action;
        }

        static ActionFromJson()
        {
            // This parses JSON action data for SpawnAreaEffect
            //
            //     "Type": "ContextActionSpawnAreaEffect",
            //     "AreaEffect": "loc:WallOfFireSpellAbilityArea",
            //     "BonusValue": "Default",
            //     "Rate": "Rounds",
            //     "DiceType": "Zero",
            //     "DiceCountValue": "0"
            _logger.Debug("Adding delegate: ContextActionSpawnAreaEffect");
            _createActionDelegates.Add("ContextActionSpawnAreaEffect",
                actionData =>
                {
                    ContextValue bonusValue = _actionFactory.CreateContextValue(
                        EnumParser.parseAbilityRankType(actionData.Duration.BonusValue)
                        );
                    ContextDurationValue durationValue = _actionFactory.CreateDurationValue(
                        bonusValue,
                        EnumParser.parseDurationRate(actionData.Duration.Rate),
                        EnumParser.parseDiceType(actionData.Duration.DiceType),
                        actionData.Duration.DiceCountValue
                        );

                    return _actionFactory.CreateAction<ContextActionSpawnAreaEffect>(a =>
                    {
                        a.AreaEffect = null; // TODO: actionData.AsString("AreaEffect");
                        a.DurationValue = durationValue;
                    });
                });

            _logger.Debug("Adding delegate: ContextActionApplyBuff");
            _createActionDelegates.Add("ContextActionApplyBuff",
                actionData =>
                {
                    ContextValue bonusValue = _actionFactory.CreateContextValue(
                        EnumParser.parseAbilityRankType(actionData.Duration.BonusValue)
                    );
                    ContextDurationValue durationValue = _actionFactory.CreateDurationValue(
                        bonusValue,
                        EnumParser.parseDurationRate(actionData.Duration.Rate),
                        EnumParser.parseDiceType(actionData.Duration.DiceType),
                        actionData.Duration.DiceCountValue
                    );

                    return _actionFactory.CreateAction<ContextActionApplyBuff>(a =>
                    {
                        a.Buff = _buffRepository.GetBuff(_identifierLookup.lookupBuff(actionData.AsString("Buff")));
                        a.IsFromSpell = actionData.Exists("IsFromSpell") && actionData.AsBool("IsFromSpell");
                        a.Permanent = actionData.Exists("Permanent") && actionData.AsBool("Permanent");
                        a.IsNotDispelable = actionData.Exists("IsNotDispelable") && actionData.AsBool("IsNotDispelable");
                        a.DurationSeconds = actionData.Exists("DurationSeconds") ? actionData.AsInt("DurationSeconds") : 0;
                        a.UseDurationSeconds = a.DurationSeconds > 0;
                        a.AsChild = !actionData.Exists("AsChild") || actionData.AsBool("AsChild");
                        a.ToCaster = actionData.Exists("ToCaster") && actionData.AsBool("ToCaster");
                        a.DurationValue = durationValue;
                    });
                });
        }
    }
}
