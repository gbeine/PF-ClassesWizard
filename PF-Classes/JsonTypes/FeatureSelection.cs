using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class FeatureSelection : JsonType
    {
        public FeatureSelection(JObject jObject) : base(jObject)
        {
            DisplayName = jObject.SelectToken("DisplayName", true).Value<String>();
            Features = jObject.SelectToken("Features", true).Value<JArray>().Values<String>().ToList();

            Description = SelectString(jObject, "Description", DisplayName);
            FeatureGroup = SelectString(jObject, "FeatureGroup", "None");
        }

        public string DisplayName { get; }
        public string Description { get; }
        public string FeatureGroup { get; }
        public List<string> Features { get; }
    }
}
