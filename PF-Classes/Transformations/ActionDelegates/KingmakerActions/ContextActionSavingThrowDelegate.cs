using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Mechanics.Actions;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

namespace PF_Classes.Transformations.ActionDelegates.KingmakerActions
{
    public class ContextActionSavingThrowDelegate : AbstractActionDelegate
    {
        public static ContextActionSavingThrow CreateAction(JsonTypes.Action actionData)
        {
            ContextActionSavingThrow a = _actionFactory.CreateAction<ContextActionSavingThrow>();

            List<GameAction> actions = Array.Empty<GameAction>().ToList();
            foreach (var action in actionData.AsList<JsonTypes.Action>("Actions"))
            {
                actions.Add(ActionFromJson.CreateAction(action));
            }
            a.Actions = new ActionList() { Actions = actions.ToArray() };

            a.Type = actionData.Exists("SavingThrowType")
                ? EnumParser.parseSavingThrowType(actionData.AsString("SavingThrowType"))
                : SavingThrowType.Unknown;

            return a;
        }
    }
}
