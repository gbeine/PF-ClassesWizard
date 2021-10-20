using Kingmaker.Utility;

namespace PF_Core.CallOfTheWild.UndeadMechanics
{
    public class UnitPartConsiderUndeadForHealing : AdditiveUnitPart
    {
        public bool active()
        {
            return !buffs.Empty();
        }
    }
}
