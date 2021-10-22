using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Condition : JsonDynamicType
    {
        public Condition(JObject jObject) : base(jObject)
        {
            Type = jObject.SelectToken("Type", true).Value<string>();
        }

        public string Type { get; }
    }
}
