using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using PF_Core.Extensions;
using PF_Core.Facades;

namespace PF_Core.Factories
{
    public class AreaEffectFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        private static readonly AreaEffectFactory __instance = new AreaEffectFactory();

        private AreaEffectFactory() { }

        public static AreaEffectFactory INSTANCE
        {
            get { return __instance; }
        }

        public BlueprintAbilityAreaEffect CreateAreaEffectFrom(String name, String guid, String fromAssetId)
        {
            _logger.Debug($"Create AreaEffect {name} with id {guid} from {fromAssetId}");
            BlueprintAbilityAreaEffect original = _library.GetAreaEffect(fromAssetId);
            BlueprintAbilityAreaEffect areaEffect = UnityEngine.Object.Instantiate(original);
            areaEffect.SetAssetId(guid);
            areaEffect.name = name;

            _library.Add(areaEffect);

            _logger.Debug($"DONE: Create AreaEffect {name} with id {guid} from {fromAssetId}");
            return areaEffect;
        }
        public BlueprintAbilityAreaEffect CreateAreaEffectFrom(String name, String guid, String fromAssetId, ContextRankBaseValueType baseValueType)
        {
            _logger.Debug($"Create AreaEffect {name} with id {guid} from {fromAssetId}");
            BlueprintAbilityAreaEffect areaEffect = CreateAreaEffectFrom(name, guid, fromAssetId);
            IEnumerable<ContextRankConfig> configs = areaEffect.GetComponents<ContextRankConfig>();

            foreach (var config in configs)
            {
                ContextRankConfig new_config = config.CreateCopy(c =>
                    {
                        c.SetField("m_BaseValueType", baseValueType);
                    });
                areaEffect.ReplaceComponent(config, new_config);
            }

            _logger.Debug($"DONE: Create AreaEffect {name} with id {guid} from {fromAssetId}");
            return areaEffect;
        }
    }
}
