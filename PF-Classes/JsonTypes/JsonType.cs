using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public abstract class JsonType : JsonWrap
    {
        protected JsonType(JObject jObject) : base(jObject)
        {
            Guid = jObject.SelectToken("Guid", true).Value<string>();
            Name = jObject.SelectToken("Name", true).Value<string>();

            ResetComponents = SelectBool(jObject, "ResetComponents", false);
            RemoveComponents = SelectStringList(jObject, "RemoveComponents");
            SelectComponents(jObject);
            SelectComponentsFrom(jObject);
        }

        protected bool isValid()
        {
            return false;
        }

        private void SelectComponents(JObject jObject)
        {
            JToken jComponents = jObject.SelectToken("Components");
            Components = Array.Empty<Component>().ToList();
            if (jComponents != null)
            {
                Components = new List<Component>();
                foreach (var jComponent in jComponents.Value<JArray>())
                {
                    Components.Add(new Component(jComponent.Value<JObject>()));
                }
            }
        }

        private void SelectComponentsFrom(JObject jObject)
        {
            JToken jComponents = jObject.SelectToken("ComponentsFrom");
            ComponentsFrom = Array.Empty<Component>().ToList();
            if (jComponents != null)
            {
                foreach (var jComponent in jComponents.Value<JArray>())
                {
                    ComponentsFrom.Add(new Component(jComponent.Value<JObject>()));
                }
            }
        }

        public string Guid { get; }
        public string Name { get; }
        public bool ResetComponents { get; }
        public List<string> RemoveComponents { get; }
        public List<Component> Components { get; private set; }
        public List<Component> ComponentsFrom { get; private set; }
    }
}
