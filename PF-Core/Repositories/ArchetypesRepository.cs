using System;
using System.Collections.Generic;
using Kingmaker.Blueprints.Classes;
using PF_Core.Facades;

namespace PF_Core.Repositories
{
    public class ArchetypesRepository
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;
        private static readonly ArchetypesRepository __instance = new ArchetypesRepository();
        
        private ArchetypesRepository() { }

        public static ArchetypesRepository INSTANCE
        {
            get { return __instance;  }
        }
        
        public List<BlueprintArchetype> AllArchetypes
        {
            get { return _library.GetArchtetypes(); }
        }
        
        public BlueprintArchetype GetArchetype(String assetId)
        {
            _logger.Debug($"Search for Archetype {assetId}");
            return _library.GetArchetype(assetId);
        }

    }
}
