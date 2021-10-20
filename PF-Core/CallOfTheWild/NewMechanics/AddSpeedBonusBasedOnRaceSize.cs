using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Newtonsoft.Json;

namespace PF_Core.CallOfTheWild.NewMechanics
{
    [AllowedOn(typeof(BlueprintUnitFact))]
    [AllowMultipleComponents]
    public class AddSpeedBonusBasedOnRaceSize : OwnedGameLogicComponent<UnitDescriptor>
    {
        [JsonProperty]
        private ModifiableValue.Modifier m_Modifier;
        public int small_race_speed_bonus;
        public int normal_race_speed_bonus;
        public ModifierDescriptor descriptor = ModifierDescriptor.Racial;

        public override void OnTurnOn()
        {
            var speed = Owner.Stats.Speed;
            var penalty = speed.Racial >= 30 ? normal_race_speed_bonus : small_race_speed_bonus;
            m_Modifier = speed.AddModifier(penalty, this, descriptor);
            base.OnTurnOn();
        }

        public override void OnTurnOff()
        {
            m_Modifier?.Remove();
            m_Modifier = null;
            base.OnTurnOff();
        }
    }
}
