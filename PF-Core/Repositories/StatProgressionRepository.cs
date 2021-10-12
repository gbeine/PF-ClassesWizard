using System;
using System.Collections.Generic;
using Kingmaker.Blueprints.Classes;
using PF_Core.Facades;

namespace PF_Core.Repositories
{
    public class StatProgressionRepository
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        private static readonly StatProgressionRepository __instance = new StatProgressionRepository();

        private StatProgressionRepository() { }

        public static StatProgressionRepository INSTANCE
        {
            get { return __instance;  }
        }

        public List<BlueprintStatProgression> AllStatProgressions
        {
            get { return _library.GetStatProgressions(); }
        }

        public BlueprintStatProgression GetStatProgression(String assetId)
        {
            _logger.Debug($"Search for StatProgression {assetId}");
            return _library.GetStatProgression(assetId);
        }
    }
}
