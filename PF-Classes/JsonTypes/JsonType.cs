using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public abstract class JsonType : JsonWrap
    {
        protected JsonType(JObject jObject) : base(jObject)
        {
            Guid = jObject.SelectToken("Guid", true).Value<string>();
            Name = jObject.SelectToken("Name", true).Value<string>();
        }

        public string Guid { get; }
        public string Name { get; }
    }
}
