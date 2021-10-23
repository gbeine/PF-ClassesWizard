using Kingmaker.UnitLogic.Mechanics.Actions;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

namespace PF_Classes.Transformations.ActionDelegates.KingmakerActions
{
    public class ContextActionRemoveBuffSingleStackDelegate : AbstractActionDelegate
    {
        public static ContextActionRemoveBuffSingleStack CreateAction(Action actionData)
        {
            ContextActionRemoveBuffSingleStack a = _actionFactory.CreateAction<ContextActionRemoveBuffSingleStack>();

            a.TargetBuff = getBuff(actionData.AsString("TargetBuff"));

            return a;
        }
    }
}
