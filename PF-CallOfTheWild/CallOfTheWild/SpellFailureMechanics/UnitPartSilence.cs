using Kingmaker.Utility;

namespace PF_CallOfTheWild.CallOfTheWild.SpellFailureMechanics
{
    class UnitPartSilence : AdditiveUnitPart
    {
        public bool active()
        {
            return buffs.Empty();
        }
    }
}
