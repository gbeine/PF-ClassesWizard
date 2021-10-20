using System;
using System.IO;
using Newtonsoft.Json.Linq;
using PF_Core;

namespace PF_Classes
{
    public abstract class Loader
    {
        protected static readonly Logger _logger = Logger.INSTANCE;

        protected readonly String _filename;
        protected readonly String _jsonString;
        protected readonly JObject _jObject;

        public Loader(String filename)
        {
            _filename = filename;
            _logger.Debug($"Loading file {_filename}");
            _jsonString = File.ReadAllText(_filename);
            _jObject = JObject.Parse(_jsonString);
        }

        public abstract bool load();
    }
}
