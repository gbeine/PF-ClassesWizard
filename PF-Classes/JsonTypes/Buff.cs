using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Buff : JsonType
    {
        public Buff(JObject jObject) : base(jObject)
        {
            DisplayName = SelectString(jObject, "DisplayName");
            Icon = SelectString(jObject, "Icon");
            Stacking = SelectString(jObject, "Stacking");
            Flags = SelectString(jObject, "Flags");

            Description = SelectString(jObject, "Description", DisplayName);
        }

        public string DisplayName { get; }
        public string Description { get; }
        public string Icon { get; }
        public string Stacking { get; }
        public string Flags { get; }
    }
}
