using System;
using System.Collections.Generic;
using Harmony12;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class ConditionFromJson : JsonTransformation
    {
        private static readonly ConditionFactory _conditionFactory = ConditionFactory.INSTANCE;

        private static readonly Dictionary<string, Func<JsonTypes.Condition, Condition>> _createConditionDelegates =
            new Dictionary<string, Func<JsonTypes.Condition, Condition>>();

        public static Condition CreateCondition(JsonTypes.Condition conditionData)
        {
            _logger.Debug($"Create Condition {conditionData.Type}");
            Condition condition = _createConditionDelegates[conditionData.Type](conditionData);

            _logger.Debug($"DONE: Create Condition {conditionData.Type}");
            return condition;
        }

        public static ConditionsChecker CreateConditionChecker(JsonTypes.Condition conditionData)
        {
            _logger.Debug($"Create ConditionChecker {conditionData.Type}");

            IEnumerable<Condition> conditions = Array.Empty<Condition>();
            foreach (var condition in conditionData.AsList<JsonTypes.Condition>("Conditions"))
            {
                conditions.Add(CreateCondition(condition));
            }

            ConditionsChecker conditionsChecker = _conditionFactory.CreateConditionsChecker(EnumParser.parseOperation(conditionData.Type), conditions);
            _logger.Debug($"DONE: Create ConditionChecker {conditionData.Type}");
            return conditionsChecker;
        }

        static ConditionFromJson()
        {

            _logger.Debug("Adding delegate: ContextConditionHasFact");
            _createConditionDelegates.Add("ContextConditionHasFact",
                conditionData =>
                {
                    ContextConditionHasFact c = _conditionFactory.CreateCondition<ContextConditionHasFact>();
                    c.Not = conditionData.Exists("Not") && conditionData.AsBool("Not");
                    if (conditionData.Exists("Feature"))
                        c.Fact = _featuresRepository.GetFeature(
                            _identifierLookup.lookupFeature(conditionData.AsString("Feature")));

                    return c;
                });

            _logger.Debug("Adding delegate: ContextConditionIsCaster");
            _createConditionDelegates.Add("ContextConditionIsCaster",
                conditionData =>
                {
                    ContextConditionIsCaster c = _conditionFactory.CreateCondition<ContextConditionIsCaster>();
                    c.Not = conditionData.Exists("Not") && conditionData.AsBool("Not");

                    return c;
                });
        }
    }
}
