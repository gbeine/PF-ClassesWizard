using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class SpellList
    {
        public SpellList(JObject jObject)
        {
            Guid = jObject.SelectToken("Guid").Value<String>();
            Name = jObject.SelectToken("Name").Value<String>();
            
            // level 0 is for cantrips
            // levels here are spell levels
            JObject jSpellsByLevel = jObject.SelectToken("SpellsByLevel").Value<JObject>();
            SpellsByLevel = new List<List<string>>(jSpellsByLevel.Count);
            for (int i = 0; i < jSpellsByLevel.Count; i++)
            {
                JArray jSpells = jSpellsByLevel.SelectToken(i.ToString()).Value<JArray>();
                SpellsByLevel.Add(jSpells.Values<String>().ToList());
            }
        }

        public string Guid { get; set; }
        public string Name { get; set; }
        public int Level
        {
            get { return SpellsByLevel.Capacity; }
        }
        public List<List<String>> SpellsByLevel { get; set; }

    }
}
