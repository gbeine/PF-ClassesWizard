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

            Description = SelectString(jObject, "Description", DisplayName);
            SelectComponents(jObject);
        }

        private void SelectComponents(JObject jObject)
        {
            JToken jComponents = jObject.SelectToken("Components");
            if (jComponents == null)
            {
                Components = Array.Empty<Component>().ToList();
            }
            else
            {
                Components = new List<Component>();
                foreach (var jComponent in jComponents.Value<JArray>())
                {
                    Components.Add(new Component(jComponent.Value<JObject>()));
                }
            }
        }

        public string DisplayName { get; }
        public string Description { get; }
        public string Icon { get; }
        public string Stacking { get; }
        public List<Component> Components { get; private set; }
    }
}
