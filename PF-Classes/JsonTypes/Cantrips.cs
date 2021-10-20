using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Cantrips : JsonType
    {
        public Cantrips(JObject jObject) : base(jObject)
        {
            DisplayName = jObject.SelectToken("DisplayName", true).Value<string>();

            Description = SelectString(jObject, "Description", DisplayName);
            Icon = SelectString(jObject, "Icon");
            From = SelectString(jObject, "From");
        }

        public string DisplayName { get; }
        public string Description { get; }
        public string Icon { get; }
        public string From { get; }
    }
}
