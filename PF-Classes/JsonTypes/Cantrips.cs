using System;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Cantrips
    {
        public Cantrips(JObject jObject)
        {
            Guid = jObject.SelectToken("Guid").Value<String>();
            Name = jObject.SelectToken("Name").Value<String>();
            DisplayName = jObject.SelectToken("DisplayName").Value<String>();
            Description = jObject.SelectToken("Description").Value<String>();
            Icon = jObject.SelectToken("Icon").Value<String>();
        }
        public string Guid { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
