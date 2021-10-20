using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components;
using PF_Core.Facades;

namespace PF_Core.Extensions
{
    public static class AbilityExecutionContextExtensions
    {
        private static readonly Harmony.FastGetter abilityExecutionContext_get_AssetId = Harmony.CreateFieldGetter<AbilityExecutionContext>("m_Ranks");

        public static int[] GetRanks(this AbilityExecutionContext context) =>
            (int[]) abilityExecutionContext_get_AssetId(context);
    }
}
