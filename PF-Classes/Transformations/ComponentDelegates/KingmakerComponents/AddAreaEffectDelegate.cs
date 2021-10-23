using Kingmaker.UnitLogic.Buffs.Components;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AddAreaEffectDelegate : AbstractComponentDelegate
    {
        public static AddAreaEffect CreateComponent(Component componentData)
        {
            AddAreaEffect c = _componentFactory.CreateComponent<AddAreaEffect>();

            c.AreaEffect = getAreaEffect(componentData.AsString("AreaEffect"));

            return c;
        }
    }
}
