using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Progression : JsonType
    {
        public Progression(JObject jObject) : base(jObject)
        {
            SelectUiDeterminatorsGroup(jObject);
            SelectUIGroups(jObject);
            SelectLevelEntries(jObject);
        }

        private void SelectUiDeterminatorsGroup(JObject jObject)
        {
            JToken jUiDeterminatorsGroup = jObject.SelectToken("UiDeterminatorsGroup");
            UiDeterminatorsGroup = jUiDeterminatorsGroup != null
                ? jUiDeterminatorsGroup.Value<JArray>().Values<String>().ToList()
                : Array.Empty<String>().ToList();
        }

        private void SelectUIGroups(JObject jObject)
        {
            JToken jUiGroups = jObject.SelectToken("UiGroups");

            if (jUiGroups == null)
            {
                UiGroups = Array.Empty<List<string>>().ToList();
            }
            else
            {
                UiGroups = new List<List<string>>();
                foreach (var jUiGroup in jUiGroups.Value<JArray>())
                {
                    UiGroups.Add(jUiGroup.Values<String>().ToList());
                }
            }
        }

        private void SelectLevelEntries(JObject jObject)
        {
            JToken jLevelEntries = jObject.SelectToken("LevelEntries");
            if (jLevelEntries == null)
            {
                LevelEntries = Array.Empty<List<string>>().ToList();
            }
            else
            {
                LevelEntries = new List<List<string>>(20);
                for (int i = 1; i < 21; i++)
                {
                    JToken jLevel = jLevelEntries.SelectToken(i.ToString());
                    List<String> levelEntries = jLevel != null
                        ? jLevel.Value<JArray>().Values<String>().ToList()
                        : Array.Empty<String>().ToList();
                    LevelEntries.Add(levelEntries);
                }
            }
        }

        public bool HasLevelEntries { get { return LevelEntries.Count > 0; } }
        public List<string> UiDeterminatorsGroup { get; private set; }
        public List<List<string>> UiGroups { get; private set; }
        public List<List<string>> LevelEntries { get; private set; }
    }
}
