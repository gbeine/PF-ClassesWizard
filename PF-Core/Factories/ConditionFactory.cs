using System.Collections.Generic;
using System.Linq;
using Kingmaker.ElementsSystem;
using PF_Core.Facades;

namespace PF_Core.Factories
{
    public class ConditionFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        private static readonly ConditionFactory __instance = new ConditionFactory();

        private ConditionFactory() { }

        public static ConditionFactory INSTANCE
        {
            get { return __instance;  }
        }

        public T CreateCondition<T>() where T : Condition =>
            _library.Create<T>();

        public ConditionsChecker ConditionsCheckerAnd(IEnumerable<Condition> conditions) =>
            CreateConditionsChecker(Operation.And, conditions.ToArray());
        public ConditionsChecker ConditionsCheckerOr(IEnumerable<Condition> conditions) =>
            CreateConditionsChecker(Operation.Or, conditions.ToArray());
        public ConditionsChecker CreateConditionsChecker(Operation operation, IEnumerable<Condition> conditions)=>
            CreateConditionsChecker(operation, conditions.ToArray());

        public ConditionsChecker CreateConditionsChecker(Operation operation, params Condition[] conditions)
        {
            _logger.Debug($"Create ConditionsCheck {operation}");
            return new ConditionsChecker() { Conditions = conditions, Operation = operation };
        }
    }
}
