using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Feature : JsonType
    {
        public Feature(JObject jObject) : base(jObject)
        {
            SelectComponents(jObject);
            SelectProgression(jObject);

            DisplayName = SelectString(jObject, "DisplayName");
            Icon = SelectString(jObject, "Icon");
            From = SelectString(jObject, "From");
            Description = SelectString(jObject, "Description", DisplayName);
            FeatureGroup = SelectString(jObject, "FeatureGroup", "None");
        }

        private void SelectProgression(JObject jObject)
        {
            JToken jProgression = jObject.SelectToken("Progression");
            if (jProgression == null)
            {
                IsProgression = false;
            }
            else
            {
                IsProgression = true;
                Progression = new Progression(jObject);
            }
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
        public string FeatureGroup { get; }
        public string From { get; }
        public bool IsProgression { get; private set; }
        public Progression Progression { get; private set; }
        public List<Component> Components { get; private set; }
    }
}
