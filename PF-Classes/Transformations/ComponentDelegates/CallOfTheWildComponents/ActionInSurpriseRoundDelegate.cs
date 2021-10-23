using System.Collections.Generic;
using Kingmaker.ElementsSystem;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;
using PF_Core.CallOfTheWild.InitiativeMechanics;

namespace PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents
{
    public class ActionInSurpriseRoundDelegate : AbstractComponentDelegate
    {
        public static ActionInSurpriseRound CreateComponent(Component componentData)
        {
            ActionInSurpriseRound c = _componentFactory.CreateComponent<ActionInSurpriseRound>();

            if (componentData.Exists("Actions"))
            {
                List<GameAction> actions = new List<GameAction>();
                foreach (var action in componentData.AsList<JsonTypes.Action>("Actions"))
                {
                    actions.Add(ActionFromJson.CreateAction(action));
                }

                c.actions = new ActionList() { Actions = actions.ToArray() };
            }

            return c;
        }
    }
}
