using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Weapons;
using PF_Core.Facades;

namespace PF_Core.Repositories
{
    public class ProgressionRepository
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        private static readonly ProgressionRepository __instance = new ProgressionRepository();

        private ProgressionRepository() { }

        public static ProgressionRepository INSTANCE
        {
            get { return __instance;  }
        }

        public BlueprintProgression GetProgression(String assetId)
        {
            _logger.Debug($"Search for Progression {assetId}");
            return _library.GetProgression(assetId);
        }
    }
}
