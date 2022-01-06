using PF_CallOfTheWild.CallOfTheWild.NewMechanics;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.KingmakerComponents;

namespace PF_Classes.Transformations.ComponentDelegates.CallOfTheWildComponents
{
    public class AddSpeedBonusBasedOnRaceSizeDelegate : AbstractComponentDelegate
    {
        public static AddSpeedBonusBasedOnRaceSize CreateComponent(Component componentData)
        {
            AddSpeedBonusBasedOnRaceSize c = _componentFactory.CreateComponent<AddSpeedBonusBasedOnRaceSize>();

            c.small_race_speed_bonus = componentData.AsInt("SmallRaceSpeedBonus");
            c.normal_race_speed_bonus = componentData.AsInt("NormalRaceSpeedBonus");

            return c;
        }
    }
}
