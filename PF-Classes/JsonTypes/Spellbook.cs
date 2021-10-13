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
            Guid = jObject.SelectToken("Guid", true).Value<String>();
            Name = jObject.SelectToken("Name", true).Value<String>();
            CastingAttribute = jObject.SelectToken("CastingAttribute", true).Value<String>();

            JToken jIsArcane = jObject.SelectToken("IsArcane");
            IsArcane = jIsArcane != null
                ? jIsArcane.Value<bool>()
                : false;
            JToken jIsSpontaneous = jObject.SelectToken("IsSpontaneous");
            IsSpontaneous = jIsSpontaneous != null
                ? jIsSpontaneous.Value<bool>()
                : false;
            JToken jCanCopyScrolls = jObject.SelectToken("CanCopyScrolls");
            CanCopyScrolls = jCanCopyScrolls != null
                ? jCanCopyScrolls.Value<bool>()
                : false;
            JToken jAllSpellsKnown = jObject.SelectToken("AllSpellsKnown");
            AllSpellsKnown = jAllSpellsKnown != null
                ? jAllSpellsKnown.Value<bool>()
                : false;
            JToken jCantrips = jObject.SelectToken("Cantrips");
            Cantrips = jCantrips != null
                ? jCantrips.Value<String>()
                : "Cantrips";

            SpellList = new SpellList(jObject.SelectToken("SpellList", true).Value<JObject>());
            SpellsKnown = new SpellsTable(jObject.SelectToken("SpellsKnown", true).Value<JObject>());
            SpellsPerDay = new SpellsTable(jObject.SelectToken("SpellsPerDay", true).Value<JObject>());
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
