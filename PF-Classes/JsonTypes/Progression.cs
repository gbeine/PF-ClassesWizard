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
            Guid = jObject.SelectToken("Guid").Value<String>();
            Name = jObject.SelectToken("Name").Value<String>();

            JArray jUiDeterminatorsGroup = jObject.SelectToken("UiDeterminatorsGroup").Value<JArray>();
            UiDeterminatorsGroup = jUiDeterminatorsGroup.Values<String>().ToList();
            
            JArray jUiGroups = jObject.SelectToken("UiGroups").Value<JArray>();
            UiGroups = new List<List<string>>();
            foreach (var jUiGroup in jUiGroups)
            {
                UiGroups.Add(jUiGroup.Values<String>().ToList());
            }
            
            JObject jLevelEntries = jObject.SelectToken("LevelEntries").Value<JObject>();
            LevelEntries = new List<List<string>>();
            for (int i = 1; i < 21; i++)
            {
                JArray jLevel = jLevelEntries.SelectToken(i.ToString()).Value<JArray>();
                LevelEntries.Add(jLevel.Values<String>().ToList());
            }
        }
        public string Guid { get; set; }
        public string Name { get; set; }
        public List<string> UiDeterminatorsGroup { get; set; }
        public List<List<string>> UiGroups { get; set; }
        public List<List<string>> LevelEntries { get; set; }
    }
}
