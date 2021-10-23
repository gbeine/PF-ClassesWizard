using Kingmaker.UnitLogic.FactLogic;
using PF_Classes.JsonTypes;
using PF_Core.Extensions;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AddMechanicsFeatureDelegate : AbstractComponentDelegate
    {
        public static AddMechanicsFeature CreateComponent(Component componentData)
        {
            AddMechanicsFeature c = _componentFactory.CreateComponent<AddMechanicsFeature>();

            c.SetFeature(EnumParser.parseMechanicsFeatureType(componentData.AsString("Mechanics")));

            return c;
        }
    }
}
