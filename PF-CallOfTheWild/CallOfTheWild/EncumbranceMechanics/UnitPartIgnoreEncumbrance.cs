using Kingmaker.Utility;

namespace PF_CallOfTheWild.CallOfTheWild.EncumbranceMechanics
{
    public class UnitPartIgnoreEncumbrance : AdditiveUnitPart
    {
        public bool active()
        {
            return !buffs.Empty();
        }
    }
}
