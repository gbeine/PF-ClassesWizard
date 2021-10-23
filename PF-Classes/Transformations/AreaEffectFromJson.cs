using System;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using PF_Classes.JsonTypes;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class AreaEffectFromJson : JsonTransformation
    {
        private static readonly AreaEffectFactory _areaEffectFactory = AreaEffectFactory.INSTANCE;

        public static BlueprintAbilityAreaEffect GetAreaEffect(AreaEffect areaEffectData)
        {
            _logger.Log($"Creating AreaEffect from JSON data {areaEffectData.Name}");

            BlueprintAbilityAreaEffect areaEffect;
            if (!string.Empty.Equals(areaEffectData.From) && !string.Empty.Equals(areaEffectData.BaseValueType))
            {
                areaEffect = _areaEffectFactory.CreateAreaEffectFrom(
                    areaEffectData.Name, areaEffectData.Guid, _identifierLookup.lookupAbilityAreaEffect(areaEffectData.From),
                    EnumParser.parseContextRankBaseValueType(areaEffectData.BaseValueType));
            }
            else if (!string.Empty.Equals(areaEffectData.From))
            {
                areaEffect = _areaEffectFactory.CreateAreaEffectFrom(
                    areaEffectData.Name, areaEffectData.Guid, _identifierLookup.lookupAbilityAreaEffect(areaEffectData.From));
            }
            else
            {
                throw new InvalidOperationException("Not implemented");
            }

            ComponentFromJson.ProcessComponents(areaEffect, areaEffectData);

            _logger.Log($"DONE: Creating AreaEffect from JSON data {areaEffectData.Name}");
            _identifierRegistry.Register(areaEffect);
            return areaEffect;
        }
    }
}
