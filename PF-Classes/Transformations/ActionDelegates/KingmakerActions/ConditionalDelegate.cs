using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

namespace PF_Classes.Transformations.ActionDelegates.KingmakerActions
{
    public class ConditionalDelegate : AbstractActionDelegate
    {
        public static Conditional CreateAction(JsonTypes.Action actionData)
        {
            Conditional a = _actionFactory.CreateAction<Conditional>();

            IEnumerable<GameAction> ifTrueActions = actionData.Exists("IfTrue")
                ? actionData.AsList<JsonTypes.Action>("IfTrue")
                    .Select(z => ActionFromJson.CreateAction(z))
                : Array.Empty<GameAction>();

            IEnumerable<GameAction> ifFalseActions = actionData.Exists("IfFalse")
                ? actionData.AsList<JsonTypes.Action>("IfFalse")
                    .Select(z => ActionFromJson.CreateAction(z))
                : Array.Empty<GameAction>();

            a.ConditionsChecker = ConditionFromJson.CreateConditionChecker(actionData.As<JsonTypes.Condition>("Condition"));
            a.IfTrue = new ActionList() { Actions = ifTrueActions.ToArray() };
            a.IfFalse = new ActionList() { Actions = ifFalseActions.ToArray() };

            return a;
        }
    }
}
