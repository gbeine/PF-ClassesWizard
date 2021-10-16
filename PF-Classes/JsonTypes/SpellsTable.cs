using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class SpellsTable : JsonType
    {
        public SpellsTable(JObject jObject) : base(jObject)
        {
            // first int in each line is for cantrips
            // levels here are character levels
            JObject jTable = jObject.SelectToken("Table", true).Value<JObject>();
            Table = new List<List<int>>(21);
            for (int i = 0; i < 21; i++)
            {
                JArray jLevelEntry = jTable.SelectToken(i.ToString(), true).Value<JArray>();
                Table.Add(jLevelEntry.Values<int>().ToList());
            }
        }

        public List<List<int>> Table { get; }
    }
}
