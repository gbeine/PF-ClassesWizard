using Kingmaker.UnitLogic.Buffs.Blueprints;
using PF_Core.CallOfTheWild;
using PF_Core.Facades;

namespace PF_Core.Extensions
{
    public static class BlueprintBuffExtensions
    {
        private static readonly Harmony.FastSetter blueprintBuff_set_Flags = Harmony.CreateFieldSetter<BlueprintBuff>("m_Flags");

        public static void SetFlags(this BlueprintBuff blueprintBuff, BuffFlags flags) =>
            blueprintBuff_set_Flags(blueprintBuff, (int)flags);
    }
}
