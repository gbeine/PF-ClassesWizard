using System;
using System.IO;
using System.Reflection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Newtonsoft.Json.Linq;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations;
using PF_Core;

namespace PF_Classes
{
    public class BuffLoader
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private String _filename;
        private String _jsonString;
        private JObject _jObject;
        private Buff _buff;

        public BuffLoader(String filename)
        {
            _filename = filename;
        }

        public bool load()
        {
            _logger.Log("Loading buff");

            _logger.Debug($"Loading buff file {_filename}");
            _jsonString = File.ReadAllText(_filename);
            _jObject = JObject.Parse(_jsonString);

            _logger.Debug("Parsing buff");
            _buff = Deserialize();

            _logger.Log($"DONE: Loading buff {_buff.Guid}");
            return true;
        }

        public JObject JObject
        {
            get { return _jObject; }
        }

        public BlueprintBuff Buff
        {
            get { return BuffFromJson.GetBuff(_buff); }
        }

        private Buff Deserialize()
        {
            _logger.Log(_jObject.SelectToken("Guid"));
            return new Buff(_jObject);
        }
    }
}
