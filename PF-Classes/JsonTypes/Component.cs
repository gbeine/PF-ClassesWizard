using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Component
    {
        private Dictionary<string, JToken> values = new Dictionary<string, JToken>();

        public Component(JObject jObject)
        {
            Type = jObject.SelectToken("Type", true).Value<String>();
            foreach (var entry in jObject)
            {
                values[entry.Key] = entry.Value;
            }
        }

        public string Type { get; }

        public bool Exists(string key)
        {
            return values.ContainsKey(key);
        }

        public IEnumerable<string> AsArray(string key)
        {
            return values[key].Values<string>();
        }

        public bool AsBool(string key)
        {
            return values[key].Value<bool>();
        }

        public int AsInt(string key)
        {
            return values[key].Value<int>();
        }

        public string AsString(string key)
        {
            return values[key].Value<String>();
        }
    }
}
