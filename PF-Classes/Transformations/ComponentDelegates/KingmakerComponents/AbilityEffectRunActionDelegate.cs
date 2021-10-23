using System.Collections.Generic;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities.Components;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AbilityEffectRunActionDelegate : AbstractComponentDelegate
    {

        // This parses JSON components containing actions
        //
        //    "Type": "AbilityEffectRunAction",
        //    "Actions": [
        //        {                                                      // this part is done via ActionFromJson
        //          "Type": "ContextActionSpawnAreaEffect",
        //          "AreaEffect": "loc:WallOfFireSpellAbilityArea",
        //          "BonusValue": "Default",
        //          "Rate": "Rounds",
        //          "DiceType": "Zero",
        //          "DiceCountValue": "0"
        //        }
        //    ]
        public static AbilityEffectRunAction CreateComponent(Component componentData)
        {
            AbilityEffectRunAction c = _componentFactory.CreateComponent<AbilityEffectRunAction>();

            if (componentData.Exists("Actions"))
            {
                List<GameAction> actions = new List<GameAction>();
                foreach (var action in componentData.AsList<JsonTypes.Action>("Actions"))
                {
                    actions.Add(ActionFromJson.CreateAction(action));
                }

                c.Actions = new ActionList() {Actions = actions.ToArray()};
            }

            return c;
        }
    }
}
