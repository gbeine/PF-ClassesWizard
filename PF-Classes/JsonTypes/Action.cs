using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Action : JsonDynamicType
    {
        public Action(JObject jObject) : base(jObject)
        {
            Type = jObject.SelectToken("Type", true).Value<string>();
            Duration = new Duration(jObject);
        }

        public string Type { get; }
        public Duration Duration { get; }
    }
}
