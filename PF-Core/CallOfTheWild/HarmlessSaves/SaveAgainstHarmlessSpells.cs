using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;

namespace PF_Core.CallOfTheWild.HarmlessSaves
{
    [AllowedOn(typeof(BlueprintUnitFact))]
    public class SaveAgainstHarmlessSpells : OwnedGameLogicComponent<UnitDescriptor>, IUnitSubscriber
    {
        public override void OnTurnOn()
        {
            this.Owner.Ensure<UnitPartSaveAgainstHarmlessSpells>().addBuff(this.Fact);
        }


        public override void OnTurnOff()
        {
            this.Owner.Get<UnitPartSaveAgainstHarmlessSpells>()?.removeBuff(this.Fact);
        }
    }
}
