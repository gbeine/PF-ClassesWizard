using System;
using Kingmaker.Blueprints.Classes;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations;

namespace PF_Classes
{
    public class CharacterClassLoader : Loader
    {
        private CharacterClass _characterClass;

        public CharacterClassLoader(String filename) : base(filename) { }

        public override bool load()
        {
            _logger.Debug("Parsing character class");
            _characterClass = Deserialize();
            _logger.Log($"DONE: Parsing character class {_characterClass.Guid}");
            return true;
        }

        public BlueprintCharacterClass CharacterClass
        {
            get { return CharacterClassFromJson.GetCharacterClass(_characterClass); }
        }

        private CharacterClass Deserialize()
        {
            return new CharacterClass(_jObject);
        }
    }
}
