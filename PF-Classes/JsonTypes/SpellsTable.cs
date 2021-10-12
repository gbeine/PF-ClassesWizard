using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class SpellsTable
    {
        public SpellsTable(JObject jObject)
        {
            Guid = jObject.SelectToken("Guid").Value<String>();
            Name = jObject.SelectToken("Name").Value<String>();

            // first int in each line is for cantrips
            // levels here are character levels
            JObject jTable = jObject.SelectToken("Table").Value<JObject>();
            Table = new List<List<int>>(21);
            for (int i = 0; i < 21; i++)
            {
                JArray jLevelEntry = jTable.SelectToken(i.ToString()).Value<JArray>();
                Table.Add(jLevelEntry.Values<int>().ToList());
            }
        }
        public string Guid { get; set; }
        public string Name { get; set; }
        public List<List<int>> Table { get; set; }
    }
}
