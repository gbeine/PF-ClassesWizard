using System;
using System.Collections.Generic;
using System.Linq;
using Harmony12;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class ActionFromJson : JsonTransformation
    {
        private static readonly ActionFactory _actionFactory = ActionFactory.INSTANCE;

        private static readonly Dictionary<string, Func<JsonTypes.Action, GameAction>> _createActionDelegates =
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
            _logger.Debug("Adding delegate: Conditional");
            _createActionDelegates.Add("Conditional",
                actionData =>
                {
                    ConditionsChecker conditionChecker = ConditionFromJson.CreateConditionChecker(actionData.As<JsonTypes.Condition>("Condition"));

                    IEnumerable<GameAction> ifTrueActions = actionData.Exists("IfTrue")
                        ? actionData.AsList<JsonTypes.Action>("IfTrue")
                            .Select(a => ActionFromJson.CreateAction(a))
                        : Array.Empty<GameAction>();

                    IEnumerable<GameAction> ifFalseActions = actionData.Exists("IfFalse")
                        ? actionData.AsList<JsonTypes.Action>("IfFalse")
                            .Select(a => ActionFromJson.CreateAction(a))
                        : Array.Empty<GameAction>();

                    return _actionFactory.CreateAction<Conditional>(a =>
                    {
                        a.ConditionsChecker = conditionChecker;
                        a.IfTrue = new ActionList() { Actions = ifTrueActions.ToArray() };
                        a.IfFalse = new ActionList() { Actions = ifFalseActions.ToArray() };
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

            _logger.Debug("Adding delegate: ContextActionConditionalSaved");
            _createActionDelegates.Add("ContextActionConditionalSaved",
                actionData =>
                {
                    IEnumerable<GameAction> actionsFailed = Array.Empty<GameAction>();
                    if (actionData.Exists("Failed"))
                    {
                        foreach (var action in actionData.AsList<JsonTypes.Action>("Failed"))
                        {
                            actionsFailed.Add(CreateAction(action));
                        }
                    }

                    IEnumerable<GameAction> actionsSucceed = Array.Empty<GameAction>();
                    if (actionData.Exists("Succeed"))
                    {
                        foreach (var action in actionData.AsList<JsonTypes.Action>("Succeed"))
                        {
                            actionsFailed.Add(CreateAction(action));
                        }
                    }

                    return _actionFactory.CreateAction<ContextActionConditionalSaved>(a =>
                    {
                        a.Failed = new ActionList() { Actions = actionsFailed.ToArray() };
                        a.Succeed = new ActionList() { Actions = actionsSucceed.ToArray() };
                    });
                });

            _logger.Debug("Adding delegate: ContextActionRemoveBuffSingleStack");
            _createActionDelegates.Add("ContextActionRemoveBuffSingleStack",
                actionData =>
                {
                    return _actionFactory.CreateAction<ContextActionRemoveBuffSingleStack>(a =>
                    {
                        a.TargetBuff = _buffRepository.GetBuff(
                            _identifierLookup.lookupBuff(actionData.AsString("TargetBuff")));
                    });
                });

            _logger.Debug("Adding delegate: ContextActionSavingThrow");
            _createActionDelegates.Add("ContextActionSavingThrow",
                actionData =>
                {
                    IEnumerable<GameAction> actions = Array.Empty<GameAction>();
                    foreach (var action in actionData.AsList<JsonTypes.Action>("Actions"))
                    {
                        actions.Add(CreateAction(action));
                    }

                    return _actionFactory.CreateAction<ContextActionSavingThrow>(a =>
                    {
                        a.Type = actionData.Exists("SavingThrowType")
                            ? EnumParser.parseSavingThrowType(actionData.AsString("SavingThrowType"))
                            : SavingThrowType.Unknown;
                    });
                });

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
        }
    }
}
