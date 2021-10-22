using System;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using PF_Core.Facades;

namespace PF_Core.Repositories
{
    public class AreaEffectRepository
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        private static readonly AreaEffectRepository __instance = new AreaEffectRepository();

        private AreaEffectRepository() { }

        public static AreaEffectRepository INSTANCE
        {
            get { return __instance;  }
        }
        public BlueprintAbilityAreaEffect GetAreaEffect(String assetId)
        {
            _logger.Debug($"Search for AreaEffect {assetId}");
            return _library.GetAreaEffect(assetId);
        }

        public void RegisterAreaEffect(BlueprintAbilityAreaEffect blueprintAreaEffect)
        {
            _logger.Debug($"Add AreaEffect {blueprintAreaEffect.name}");
            // nothing to do name yet
            _logger.Debug($"DONE: Add AreaEffect {blueprintAreaEffect.name}");
        }
    }
}
