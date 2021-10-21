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

            DisplayName = SelectString(jObject, "DisplayName");
            Icon = SelectString(jObject, "Icon");
            From = SelectString(jObject, "From");
            Description = SelectString(jObject, "Description", DisplayName);
            FeatureGroup = SelectString(jObject, "FeatureGroup");
            Class = SelectString(jObject, "Class");
            ReapplyOnLevelUp = SelectBool(jObject, "ReapplyOnLevelUp");
            HideInUI = SelectBool(jObject, "HideInUI");
            IsClassFeature = SelectBool(jObject, "IsClassFeature");
            RemoveComponents = SelectStringList(jObject, "RemoveComponents");
        }

        private void SelectComponents(JObject jObject)
        {
            JToken jComponents = jObject.SelectToken("Components");
            Components = Array.Empty<Component>().ToList();
            if (jComponents != null)
            {
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
        public string Class { get; }
        public bool? IsClassFeature { get; }
        public bool? ReapplyOnLevelUp { get; }
        public bool? HideInUI { get; }
        public List<string> RemoveComponents { get; }
        public List<Component> Components { get; private set; }
    }
}
