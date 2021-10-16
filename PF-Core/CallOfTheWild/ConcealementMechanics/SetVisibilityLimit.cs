using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;

namespace PF_Core.CallOfTheWild.ConcealementMechanics
{
    [AllowedOn(typeof(BlueprintUnitFact))]
    public class SetVisibilityLimit : OwnedGameLogicComponent<UnitDescriptor>, IUnitSubscriber
    {
        public Feet visibility_limit;
        public override void OnTurnOn()
        {
            Owner.Ensure<UnitPartVisibilityLimit>().addBuff(Fact);
        }

        public override void OnTurnOff()
        {
            Owner.Get<UnitPartVisibilityLimit>()?.removeBuff(Fact);
        }

    }
}
