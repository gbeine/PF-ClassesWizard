using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class JsonDynamicType : JsonWrap
    {
        protected Dictionary<string, JToken> values = new Dictionary<string, JToken>();

        public JsonDynamicType(JObject jObject) : base(jObject)
        {
            foreach (var entry in jObject)
            {
                values[entry.Key] = entry.Value;
            }
        }

        public bool Exists(string key)
        {
            return values.ContainsKey(key);
        }

        public T As<T>(string key) where T : JsonWrap
        {
            return (T) Activator.CreateInstance(
                typeof(T), values[key]);
        }

        public List<T> AsList<T>(string key) where T : JsonWrap
        {
            List<T> list = new List<T>();
            foreach (var jObject in values[key].Values<JObject>())
            {
                list.Add((T) Activator.CreateInstance(
                    typeof(T), jObject));
            }

            return list;
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
            return values[key].Value<string>();
        }
    }
}
