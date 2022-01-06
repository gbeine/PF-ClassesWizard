using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;

namespace PF_CallOfTheWild.CallOfTheWild.UndeadMechanics
{
    [AllowedOn(typeof(BlueprintUnitFact))]
    public class ConsiderUndeadForHealing : OwnedGameLogicComponent<UnitDescriptor>, IUnitSubscriber
    {

        public override void OnTurnOn()
        {
            this.Owner.Ensure<UnitPartConsiderUndeadForHealing>().addBuff(this.Fact);
        }


        public override void OnTurnOff()
        {
            this.Owner.Ensure<UnitPartConsiderUndeadForHealing>().removeBuff(this.Fact);
        }
    }
}
