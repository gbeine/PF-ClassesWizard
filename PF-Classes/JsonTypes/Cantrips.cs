using System;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Cantrips
    {
        public Cantrips(JObject jObject)
        {
            Guid = jObject.SelectToken("Guid", true).Value<String>();
            Name = jObject.SelectToken("Name", true).Value<String>();
            DisplayName = jObject.SelectToken("DisplayName", true).Value<String>();
            Icon = jObject.SelectToken("Icon", true).Value<String>();

            JToken jDescription = jObject.SelectToken("Description");
            Description = jDescription != null
                ? jDescription.Value<String>()
                : DisplayName;
        }
        public string Guid { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
