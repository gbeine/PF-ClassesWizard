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
            Guid = jObject.SelectToken("Guid", true).Value<String>();
            Name = jObject.SelectToken("Name", true).Value<String>();

            // level 0 is for cantrips
            // levels here are spell levels
            JObject jSpellsByLevel = jObject.SelectToken("SpellsByLevel", true).Value<JObject>();
            Level = jSpellsByLevel.Count;
            SpellsByLevel = new List<List<string>>(Level);
            for (int i = 0; i < Level; i++)
            {
                JArray jSpells = jSpellsByLevel.SelectToken(i.ToString(), true).Value<JArray>();
                SpellsByLevel.Add(jSpells.Values<String>().ToList());
            }
        }

        public string Guid { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public List<List<String>> SpellsByLevel { get; set; }

    }
}
