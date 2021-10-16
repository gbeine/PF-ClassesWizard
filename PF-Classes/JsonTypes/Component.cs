using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Component
    {
        private Dictionary<string, string> values = new Dictionary<string, string>();

        public Component(JObject jObject)
        {
            Type = jObject.SelectToken("Type", true).Value<String>();
            foreach (var entry in jObject)
            {
                values[entry.Key] = entry.Value.ToString();
            }
        }

        public string Type { get; }

        public bool Exists(string key)
        {
            return values.ContainsKey(key);
        }

        public string AsString(string key)
        {
            return values[key];
        }

        public bool AsBool(string key)
        {
            return bool.Parse(values[key]);
        }

        public int AsInt(string key)
        {
            return int.Parse(values[key]);
        }
    }
}
