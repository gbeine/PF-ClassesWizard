using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Component : JsonDynamicType
    {
        public Component(JObject jObject) : base(jObject)
        {
            Type = jObject.SelectToken("Type", true).Value<string>();
        }

        public string Type { get; }
    }
}
