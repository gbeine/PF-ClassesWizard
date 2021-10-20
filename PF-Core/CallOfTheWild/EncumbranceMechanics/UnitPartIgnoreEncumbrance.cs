using Kingmaker.Utility;

namespace PF_Core.CallOfTheWild.EncumbranceMechanics
{
    public class UnitPartIgnoreEncumbrance : AdditiveUnitPart
    {
        public bool active()
        {
            return !buffs.Empty();
        }
    }
}
