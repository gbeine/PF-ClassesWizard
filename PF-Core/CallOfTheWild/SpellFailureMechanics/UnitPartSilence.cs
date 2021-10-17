using Kingmaker.Utility;

namespace PF_Core.CallOfTheWild.SpellFailureMechanics
{
    class UnitPartSilence : AdditiveUnitPart
    {
        public bool active()
        {
            return buffs.Empty();
        }
    }
}
