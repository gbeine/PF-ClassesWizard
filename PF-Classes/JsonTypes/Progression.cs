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
            DisplayName = SelectString(jObject, "DisplayName");
            Description = SelectString(jObject, "Description", DisplayName);
            Icon = SelectString(jObject, "Icon");
            From = SelectString(jObject, "From");
            CharacterClass = SelectString(jObject, "CharacterClass");
            UiDeterminatorsGroup = SelectStringList(jObject, "UiDeterminatorsGroup");

            SelectUIGroups(jObject);
            SelectLevelEntries(jObject);
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

        public string DisplayName { get; }
        public string Description { get; }
        public string Icon { get; }
        public string From { get; }
        public string CharacterClass { get; }
        public bool HasLevelEntries { get { return LevelEntries.Count > 0; } }
        public List<string> UiDeterminatorsGroup { get; }
        public List<List<string>> UiGroups { get; private set; }
        public List<List<string>> LevelEntries { get; private set; }
    }
}
