using System;
using Kingmaker.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics;
using PF_Core.Facades;

namespace PF_Core.Factories
{
    public class ActionFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        private static readonly ActionFactory __instance = new ActionFactory();

        private ActionFactory() { }

        public static ActionFactory INSTANCE
        {
            get { return __instance;  }
        }

        public ContextValue CreateContextValue(AbilityRankType value)
        {
            return new ContextValue() { ValueType = ContextValueType.Rank, ValueRank = value };
        }

        public ContextValue CreateContextValue(AbilitySharedValue value)
        {
            return new ContextValue() { ValueType = ContextValueType.Shared, ValueShared = value };
        }

        public ContextDurationValue CreateDurationValue(ContextValue bonusValue, DurationRate rate, DiceType diceType, ContextValue diceCountValue)
        {
            return new ContextDurationValue()
            {
                BonusValue = bonusValue,
                Rate = rate,
                DiceCountValue = diceCountValue,
                DiceType = diceType
            };
        }

        public T CreateAction<T>(Action<T> init) where T : GameAction
        {
            _logger.Debug($"Create Action for {typeof(T)}");
            T component = _library.Create<T>(init);

            _logger.Debug($"DONE: Create CoActionmponent for {typeof(T)}");
            return component;
        }

        public T CreateAction<T>() where T : GameAction
        {
            _logger.Debug($"Create Action for {typeof(T)}");
            T component = _library.Create<T>();

            _logger.Debug($"DONE: Create Action for {typeof(T)}");
            return component;
        }
    }
}
