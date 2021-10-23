using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Actions;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

namespace PF_Classes.Transformations.ActionDelegates.KingmakerActions
{
    public class ContextActionConditionalSavedDelegate : AbstractActionDelegate
    {
        public static ContextActionConditionalSaved CreateAction(JsonTypes.Action actionData)
        {
            ContextActionConditionalSaved a = _actionFactory.CreateAction<ContextActionConditionalSaved>();

            List<GameAction> actionsFailed = Array.Empty<GameAction>().ToList();
            if (actionData.Exists("Failed"))
            {
                foreach (var action in actionData.AsList<JsonTypes.Action>("Failed"))
                {
                    actionsFailed.Add(ActionFromJson.CreateAction(action));
                }
            }

            List<GameAction> actionsSucceed = Array.Empty<GameAction>().ToList();
            if (actionData.Exists("Succeed"))
            {
                foreach (var action in actionData.AsList<JsonTypes.Action>("Succeed"))
                {
                    actionsFailed.Add(ActionFromJson.CreateAction(action));
                }
            }

            a.Failed = new ActionList() { Actions = actionsFailed.ToArray() };
            a.Succeed = new ActionList() { Actions = actionsSucceed.ToArray() };

            return a;
        }
    }
}
