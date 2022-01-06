using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;

namespace PF_CallOfTheWild.CallOfTheWild.SpellFailureMechanics
{
    [AllowedOn(typeof(BlueprintUnitFact))]
    public class Silence : OwnedGameLogicComponent<UnitDescriptor>, IUnitSubscriber
    {
        public override void OnTurnOn()
        {
            this.Owner.Ensure<UnitPartSilence>().addBuff(this.Fact);
        }

        public override void OnTurnOff()
        {
            this.Owner.Ensure<UnitPartSilence>().removeBuff(this.Fact);
        }
    }
}
