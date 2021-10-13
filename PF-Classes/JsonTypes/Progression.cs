using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Progression
    {
        public Progression(JObject jObject)
        {
            Guid = jObject.SelectToken("Guid", true).Value<String>();
            Name = jObject.SelectToken("Name", true).Value<String>();

            JToken jUiDeterminatorsGroup = jObject.SelectToken("UiDeterminatorsGroup");
            UiDeterminatorsGroup = jUiDeterminatorsGroup != null
                ? jUiDeterminatorsGroup.Value<JArray>().Values<String>().ToList()
                : Array.Empty<String>().ToList();


            UiGroups = new List<List<string>>();
            JToken jUiGroups = jObject.SelectToken("UiGroups");
            if (jUiGroups != null)
            {
                foreach (var jUiGroup in jUiGroups.Value<JArray>())
                {
                    UiGroups.Add(jUiGroup.Values<String>().ToList());
                }
            }

            JObject jLevelEntries = jObject.SelectToken("LevelEntries", true).Value<JObject>();
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
        public string Guid { get; set; }
        public string Name { get; set; }
        public List<string> UiDeterminatorsGroup { get; set; }
        public List<List<string>> UiGroups { get; set; }
        public List<List<string>> LevelEntries { get; set; }
    }
}
