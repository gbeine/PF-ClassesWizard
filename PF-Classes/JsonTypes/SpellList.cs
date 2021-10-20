using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class SpellList : JsonType
    {
        public SpellList(JObject jObject) : base(jObject)
        {
            // level 0 is for cantrips
            // levels here are spell levels
            JObject jSpellsByLevel = jObject.SelectToken("SpellsByLevel", true).Value<JObject>();
            Level = jSpellsByLevel.Count > 0
                ? jSpellsByLevel.Count - 1
                : 0;
            SpellsByLevel = new List<List<string>>(jSpellsByLevel.Count);
            for (int i = 0; i < jSpellsByLevel.Count; i++)
            {
                JArray jSpells = jSpellsByLevel.SelectToken(i.ToString(), true).Value<JArray>();
                SpellsByLevel.Add(jSpells.Values<string>().ToList());
            }
        }

        public int Level { get; }
        public List<List<string>> SpellsByLevel { get; }
    }
}
