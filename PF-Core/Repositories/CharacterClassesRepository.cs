using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints.Classes;
using PF_Core.Facades;

namespace PF_Core.Repositories
{
    public class CharacterClassesRepository
    {

        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        private static readonly CharacterClassesRepository __instance = new CharacterClassesRepository();

        private CharacterClassesRepository() { }

        public static CharacterClassesRepository INSTANCE
        {
            get { return __instance;  }
        }

        public List<BlueprintCharacterClass> AllCharacterClasses
        {
            get { return _library.GetCharacterClasses(); }
        }

        public List<BlueprintCharacterClass> PrestigeClasses
        {
            get
            {
                return _library.GetCharacterClasses()
                    .Where(
                        c => c.PrestigeClass)
                    .ToList();
            }
        }

        public BlueprintCharacterClass GetCharacterClass(String assetId)
        {
            _logger.Debug($"Search for CharacterClass {assetId}");
            return _library.GetCharacterClass(assetId);
        }

        public void RegisterCharacterClass(BlueprintCharacterClass blueprintCharacterClass)
        {
            _logger.Debug($"Add CharacterClass {blueprintCharacterClass.Name}");
            _library.RegisterCharacterClass(blueprintCharacterClass);
            _logger.Debug($"DONE: Add CharacterClass {blueprintCharacterClass.Name}");
        }
    }
}
