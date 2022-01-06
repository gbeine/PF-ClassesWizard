using Kingmaker.Utility;

namespace PF_CallOfTheWild.CallOfTheWild.HarmlessSaves
{
    internal class UnitPartSaveAgainstHarmlessSpells : AdditiveUnitPart
    {
        public bool active()
        {
            return !buffs.Empty();
        }
    }
}
