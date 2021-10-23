using System;
using Kingmaker.ElementsSystem;
using PF_Classes.Transformations.ActionDelegates;

namespace PF_Classes.Transformations
{
    public class ActionFromJson : JsonTransformation
    {
        public static GameAction CreateAction(JsonTypes.Action actionData)
        {
            _logger.Debug($"Create action {actionData.Type}");

            GameAction action;

            if (CreateActionDelegates.CanCreate(actionData.Type))
                 action = CreateActionDelegates.Create(actionData);
            else
            {
                string message = $"Creatomg of {actionData.Type} not possible, no delegates known";
                _logger.Error(message);
                throw new InvalidOperationException(message);
            }

            _logger.Debug($"DONE: Create action {actionData.Type}");
            return action;
        }
    }
}
