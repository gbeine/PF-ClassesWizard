using System;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public abstract class JsonType
    {
        public JsonType(JObject jObject)
        {
            Guid = jObject.SelectToken("Guid", true).Value<String>();
            Name = jObject.SelectToken("Name", true).Value<String>();
        }

        protected bool SelectBool(JObject jObject, String key, bool defaultValue)
        {
            JToken jToken = jObject.SelectToken(key);
            return jToken != null
                ? jToken.Value<bool>()
                : defaultValue;
        }

        protected string SelectString(JObject jObject, string key, string defaultValue)
        {
            JToken jToken = jObject.SelectToken(key);
            return jToken != null
                ? jToken.Value<string>()
                : defaultValue;
        }

        protected string SelectString(JObject jObject, string key)
        {
            return SelectString(jObject, key, String.Empty);
        }

        protected int SelectInt(JObject jObject, string key, int defaultValue)
        {
            JToken jToken = jObject.SelectToken(key);
            return jToken != null
                ? jToken.Value<int>()
                : defaultValue;
        }

        protected int SelectInt(JObject jObject, string key)
        {
            return SelectInt(jObject, key, 0);
        }

        public string Guid { get; }
        public string Name { get; }
    }
}
