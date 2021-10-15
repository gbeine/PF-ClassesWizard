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

            SelectSpellsKnown(jObject.SelectToken("SpellsKnown"));
            SelectSpellsPerDay(jObject.SelectToken("SpellsPerDay", true));
            SelectSpellList(jObject.SelectToken("SpellList", true));
        }

        private void SelectSpellsKnown(JToken jSpellsKnown)
        {
            if (jSpellsKnown != null)
            {
                if (jSpellsKnown.Type == JTokenType.String)
                {
                    SpellsKnown = jSpellsKnown.Value<String>();
                    SpellsKnownDefinition = null;
                }
                else
                {
                    SpellsKnown = null;
                    SpellsKnownDefinition = new SpellsTable(jSpellsKnown.Value<JObject>());
                }
            }
            else
            {
                SpellsKnown = null;
                SpellsKnownDefinition = null;
            }
        }

        private void SelectSpellsPerDay(JToken jSpellsPerDay)
        {
            if (jSpellsPerDay.Type == JTokenType.String)
            {
                SpellsPerDay = jSpellsPerDay.Value<String>();
                SpellsPerDayDefinition = null;
            }
            else
            {
                SpellsPerDay = null;
                SpellsPerDayDefinition = new SpellsTable(jSpellsPerDay.Value<JObject>());
            }
        }

        private void SelectSpellList(JToken jSpellList)
        {
            if (jSpellList.Type == JTokenType.String)
            {
                SpellList = jSpellList.Value<String>();
                SpellListDefinition = null;
            }
            else
            {
                SpellList = null;
                SpellListDefinition = new SpellList(jSpellList.Value<JObject>());
            }
        }

        public string Guid { get; set; }
        public string Name { get; set; }
        public bool IsArcane { get; set; }
        public bool IsSpontaneous { get; set; }
        public bool CanCopyScrolls { get; set; }
        public bool AllSpellsKnown { get; set; }
        public string CastingAttribute { get; set; }
        public string Cantrips { get; set; }
        public string SpellList { get; set; }
        public string SpellsKnown { get; set; }
        public string SpellsPerDay { get; set; }
        public bool HasSpellListDefinition { get { return SpellListDefinition != null; } }
        public bool HasSpellsPerDayDefinition { get { return SpellsPerDayDefinition != null; } }
        public bool HasSpellsKnownDefinition { get { return SpellsKnownDefinition != null; } }
        public SpellList SpellListDefinition { get; set; }
        public SpellsTable SpellsKnownDefinition { get; set; }
        public SpellsTable SpellsPerDayDefinition { get; set; }
    }
}
