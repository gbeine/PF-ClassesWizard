using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Progression : Feature
    {
        public Progression(JObject jObject) : base(jObject)
        {
            IsClassProgression = SelectBool(jObject, "IsClassProgression", false);
            UiDeterminatorsGroup = SelectStringList(jObject, "UiDeterminatorsGroup");

            SelectUIGroups(jObject);
            SelectLevelEntries(jObject);
        }

        private void SelectUIGroups(JObject jObject)
        {
            JToken jUiGroups = jObject.SelectToken("UiGroups");
            UiGroups = Array.Empty<List<string>>().ToList();

            if (jUiGroups != null)
            {
                foreach (var jUiGroup in jUiGroups.Value<JArray>())
                {
                    UiGroups.Add(jUiGroup.Values<string>().ToList());
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
                    List<string> levelEntries = jLevel != null
                        ? jLevel.Value<JArray>().Values<string>().ToList()
                        : Array.Empty<String>().ToList();
                    LevelEntries.Add(levelEntries);
                }
            }
        }

        public bool IsClassProgression { get; }
        public bool HasUiDeterminatorsGroup { get { return UiDeterminatorsGroup.Count > 0; } }
        public bool HasUiGroups { get { return UiGroups.Count > 0; } }
        public bool HasLevelEntries { get { return LevelEntries.Count > 0; } }
        public List<string> UiDeterminatorsGroup { get; }
        public List<List<string>> UiGroups { get; private set; }
        public List<List<string>> LevelEntries { get; private set; }
    }
}
