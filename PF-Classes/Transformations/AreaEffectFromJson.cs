using System;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using PF_Classes.JsonTypes;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class AreaEffectFromJson : JsonTransformation
    {
        private static readonly AreaEffectFactory _areaEffectFactory = AreaEffectFactory.INSTANCE;

        public BlueprintAbilityAreaEffect GetAreaEffect(AreaEffect areaEffectData)
        {

            BlueprintAbilityAreaEffect areaEffect;
            if (!String.Empty.Equals(areaEffectData.From))
            {
                areaEffect = _areaEffectFactory.CreateAreaEffectFrom(
                    areaEffectData.Name, areaEffectData.Guid, areaEffectData.From,
                    EnumParser.parseContextRankBaseValueType(areaEffectData.BaseValueType));
            }
            else
            {
                throw new InvalidOperationException("Not implemented");
            }

            return areaEffect;
        }
    }
}
