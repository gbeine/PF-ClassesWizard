using Kingmaker.Utility;
using PF_CallOfTheWild.CallOfTheWild.ConcealementMechanics;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

namespace PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents
{
    public class AddOutgoingConcealmentDelegate : AbstractComponentDelegate
    {
        public static AddOutgoingConcealment CreateComponent(Component componentData)
        {
            AddOutgoingConcealment c = _componentFactory.CreateComponent<AddOutgoingConcealment>();

            c.CheckDistance = !componentData.Exists("CheckDistance") || componentData.AsBool("CheckDistance");
            c.Descriptor = EnumParser.parseConcealmentDescriptor(componentData.AsString("Descriptor"));
            c.DistanceGreater = componentData.AsInt("DistanceGreater").Feet();
            c.Concealment = EnumParser.parseConcealment(componentData.AsString("Concealment"));

            return c;
        }
    }
}
