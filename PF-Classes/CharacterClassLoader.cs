using System;
using System.IO;
using System.Reflection;
using Kingmaker.Blueprints.Classes;
using Newtonsoft.Json.Linq;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations;
using PF_Core;

namespace PF_Classes
{
    public class CharacterClassLoader
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private String _filename;
        private String _jsonString;
        private JObject _jObject;
        private CharacterClass _characterClass;
        
        public CharacterClassLoader(String filename)
        {
            _filename = filename;
        }
        
        public bool load()
        {
            _logger.Log("Loading character class");

            _logger.Debug($"Loading character class file {_filename}");
            _jsonString = File.ReadAllText(_filename);
            _jObject = JObject.Parse(_jsonString);

            _logger.Debug("Parsing character class");
            _characterClass = Deserialize();

            _logger.Log($"DONE: Loading character class {_characterClass.Guid}");
            return true;
        }

        public JObject JObject
        {
            get { return _jObject; }
        }

        public BlueprintCharacterClass CharacterClass
        {
            get { return CharacterClassFromJson.GetCharacterClass(_characterClass); }
        }

        private CharacterClass Deserialize()
        {
            _logger.Log(_jObject.SelectToken("Guid"));
            return new CharacterClass(_jObject);
        }
    }
}
