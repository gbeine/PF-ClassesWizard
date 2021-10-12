using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Spellbook
    {
        public Spellbook(JObject jObject)
        {
            Guid = jObject.SelectToken("Guid").Value<String>();
            Name = jObject.SelectToken("Name").Value<String>();
            IsArcane = jObject.SelectToken("IsArcane").Value<bool>();
            IsSpontaneous = jObject.SelectToken("IsSpontaneous").Value<bool>();
            CanCopyScrolls = jObject.SelectToken("CanCopyScrolls").Value<bool>();
            AllSpellsKnown = jObject.SelectToken("AllSpellsKnown").Value<bool>();
            CastingAttribute = jObject.SelectToken("CastingAttribute").Value<String>();
            Cantrips = jObject.SelectToken("Cantrips").Value<String>();
            SpellList = new SpellList(jObject.SelectToken("SpellList").Value<JObject>());
            SpellsKnown = new SpellsTable(jObject.SelectToken("SpellsKnown").Value<JObject>());
            SpellsPerDay = new SpellsTable(jObject.SelectToken("SpellsPerDay").Value<JObject>());
        }
        public string Guid { get; set; }
        public string Name { get; set; }
        public bool IsArcane { get; set; }
        public bool IsSpontaneous { get; set; }
        public bool CanCopyScrolls { get; set; }
        public bool AllSpellsKnown { get; set; }
        public string CastingAttribute { get; set; }
        public string Cantrips { get; set; }
        public SpellList SpellList { get; set; }
        public SpellsTable SpellsKnown { get; set; }
        public SpellsTable SpellsPerDay { get; set; }
    }
}
