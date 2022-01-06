using Kingmaker.Utility;

namespace PF_CallOfTheWild.CallOfTheWild.UndeadMechanics
{
    public class UnitPartConsiderUndeadForHealing : AdditiveUnitPart
    {
        public bool active()
        {
            return !buffs.Empty();
        }
    }
}
