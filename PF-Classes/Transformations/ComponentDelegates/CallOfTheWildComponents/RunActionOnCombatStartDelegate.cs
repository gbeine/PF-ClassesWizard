using System.Collections.Generic;
using Kingmaker.ElementsSystem;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;
using PF_Core.CallOfTheWild.NewMechanics;

namespace PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents
{
    public class RunActionOnCombatStartDelegate : AbstractComponentDelegate
    {
        public static RunActionOnCombatStart CreateComponent(Component componentData)
        {
            RunActionOnCombatStart c = _componentFactory.CreateComponent<RunActionOnCombatStart>();

            if (componentData.Exists("Actions"))
            {
                List<GameAction> actions = new List<GameAction>();
                foreach (var action in componentData.AsList<JsonTypes.Action>("Actions"))
                {
                    actions.Add(ActionFromJson.CreateAction(action));
                }

                c.actions = new ActionList() {Actions = actions.ToArray()};
            }

            return c;
        }
    }
}
