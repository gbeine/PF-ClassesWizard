using System;
using Kingmaker.Blueprints.Classes;
using PF_Core.Extensions;
using PF_Core.Facades;

namespace PF_Core.Factories
{
    public class CharacterClassFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        private static readonly LocalizationFactory _localizationFactory = new LocalizationFactory();

        public BlueprintCharacterClass CreateClass(String name, String guid)
        {
            _logger.Debug($"Create class {name} with id {guid}");

            BlueprintCharacterClass characterClass = _library.Create<BlueprintCharacterClass>();
            characterClass.SetAssetId(guid);
            characterClass.name = name;

            _library.Add(characterClass);

            _logger.Debug($"DONE: Create class {name} with id {guid}");
            return characterClass;
        }

        public BlueprintCharacterClass CreateClass(String name, String guid, String displayName, String description)
        {
            _logger.Debug($"Create class {name} with id {guid}");

            BlueprintCharacterClass characterClass = CreateClass(name, guid);
            characterClass.LocalizedName = _localizationFactory.CreateString($"{name}.Name", displayName);
            characterClass.LocalizedDescription = _localizationFactory.CreateString($"{name}.Description", description);

            _logger.Debug($"DONE: Create class {name} with id {guid}");
            return characterClass;
        }
    }
}
