using System.Collections.Generic;
using System.Linq;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AbilityAreaEffectRunActionDelegate : AbstractComponentDelegate
    {
        public static AbilityAreaEffectRunAction CreateComponent(Component componentData)
        {
            AbilityAreaEffectRunAction c = _componentFactory.CreateComponent<AbilityAreaEffectRunAction>();

            if (componentData.Exists("UnitEnter"))
            {
                IEnumerable<GameAction> actions = componentData.AsList<JsonTypes.Action>("UnitEnter")
                    .Select(a => ActionFromJson.CreateAction(a));

                c.UnitEnter = new ActionList() { Actions = actions.ToArray() };
            }
            if (componentData.Exists("UnitExit"))
            {
                IEnumerable<GameAction> actions = componentData.AsList<JsonTypes.Action>("UnitExit")
                    .Select(a => ActionFromJson.CreateAction(a));

                c.UnitExit = new ActionList() { Actions = actions.ToArray() };
            }
            if (componentData.Exists("UnitMove"))
            {
                IEnumerable<GameAction> actions = componentData.AsList<JsonTypes.Action>("UnitMove")
                    .Select(a => ActionFromJson.CreateAction(a));

                c.UnitMove = new ActionList() { Actions = actions.ToArray() };
            }
            if (componentData.Exists("Round"))
            {
                IEnumerable<GameAction> actions = componentData.AsList<JsonTypes.Action>("Round")
                    .Select(a => ActionFromJson.CreateAction(a));

                c.Round = new ActionList() { Actions = actions.ToArray() };
            }

            return c;
        }
    }
}
