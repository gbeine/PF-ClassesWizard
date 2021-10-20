using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;

namespace PF_Core.CallOfTheWild.EncumbranceMechanics
{
    [AllowedOn(typeof(BlueprintUnitFact))]
    public class IgnoreEncumbrence : OwnedGameLogicComponent<UnitDescriptor>, IUnitSubscriber
    {
        public Feet visibility_limit;
        public override void OnTurnOn()
        {
            this.Owner.Ensure<UnitPartIgnoreEncumbrance>().addBuff(this.Fact);
        }

        public override void OnTurnOff()
        {
            this.Owner.Ensure<UnitPartIgnoreEncumbrance>().removeBuff(this.Fact);
        }
    }
}
