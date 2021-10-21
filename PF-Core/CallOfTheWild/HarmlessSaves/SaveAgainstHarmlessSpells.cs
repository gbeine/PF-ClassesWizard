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
            Owner.Ensure<UnitPartSaveAgainstHarmlessSpells>().addBuff(Fact);
        }


        public override void OnTurnOff()
        {
            Owner.Get<UnitPartSaveAgainstHarmlessSpells>()?.removeBuff(Fact);
        }
    }
}
