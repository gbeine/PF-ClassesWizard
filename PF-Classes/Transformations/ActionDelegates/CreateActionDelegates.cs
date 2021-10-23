using System;
using System.Collections.Generic;
using Kingmaker;
using Kingmaker.ElementsSystem;
using PF_Classes.Transformations.ActionDelegates.KingmakerActions;
using PF_Core;

namespace PF_Classes.Transformations.ActionDelegates
{
    public class CreateActionDelegates
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly Dictionary<string, Func<JsonTypes.Action, GameAction>> Delegates =
            new Dictionary<string, Func<JsonTypes.Action, GameAction>>();

        public static bool CanCreate(string action) =>
            Delegates.ContainsKey(action);

        public static GameAction Create(JsonTypes.Action action) =>
            Delegates[action.Type](action);

        static CreateActionDelegates()
        {
            _logger.Debug("Adding delegate: Conditional");
            Delegates.Add("Conditional", ConditionalDelegate.CreateAction);

            _logger.Debug("Adding delegate: ContextActionApplyBuff");
            Delegates.Add("ContextActionApplyBuff", ContextActionApplyBuffDelegate.CreateAction);

            _logger.Debug("Adding delegate: ContextActionConditionalSaved");
            Delegates.Add("ContextActionConditionalSaved", ContextActionConditionalSavedDelegate.CreateAction);

            _logger.Debug("Adding delegate: ContextActionRemoveBuffSingleStack");
            Delegates.Add("ContextActionRemoveBuffSingleStack", ContextActionRemoveBuffSingleStackDelegate.CreateAction);

            _logger.Debug("Adding delegate: ContextActionSavingThrow");
            Delegates.Add("ContextActionSavingThrow", ContextActionSavingThrowDelegate.CreateAction);

            _logger.Debug("Adding delegate: ContextActionSpawnAreaEffect");
            Delegates.Add("ContextActionSpawnAreaEffect", ContextActionSpawnAreaEffectDelegate.CreateAction);
        }
    }
}
