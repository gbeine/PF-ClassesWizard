using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;

namespace PF_Core.Extensions
{
    public static class BlueprintAbilityExtensions
    {
        public static bool HasAreaEffect(this BlueprintAbility spell)
        {
            return spell.AoERadius.Meters > 0f || spell.ProjectileType != AbilityProjectileType.Simple;
        }
    }
}
