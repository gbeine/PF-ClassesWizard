using Kingmaker.Utility;

namespace PF_Core.CallOfTheWild.HarmlessSaves
{
    internal class UnitPartSaveAgainstHarmlessSpells : AdditiveUnitPart
    {
        public bool active()
        {
            return !buffs.Empty();
        }
    }
}
