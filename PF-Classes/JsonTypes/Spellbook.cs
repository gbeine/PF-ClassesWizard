using System;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Spellbook : JsonType
    {
        public Spellbook(JObject jObject) : base(jObject)
        {
            CastingAttribute = jObject.SelectToken("CastingAttribute", true).Value<String>();

            IsArcane = SelectBool(jObject, "IsArcane", false);
            IsSpontaneous = SelectBool(jObject, "IsSpontaneous", false);
            CanCopyScrolls = SelectBool(jObject, "CanCopyScrolls", false);
            AllSpellsKnown = SelectBool(jObject, "AllSpellsKnown", false);

            Cantrips = SelectString(jObject, "Cantrips", "Cantrips");
            SpellsPerLevel = SelectInt(jObject, "SpellsPerLevel");
            CasterLevelModifier = SelectInt(jObject, "CasterLevelModifier");

            SelectSpellsKnown(jObject);
            SelectSpellsPerDay(jObject);
            SelectSpellList(jObject);
        }

        private void SelectSpellsKnown(JObject jObject)
        {
            JToken jSpellsKnown = jObject.SelectToken("SpellsKnown");
            if (jSpellsKnown != null)
            {
                if (jSpellsKnown.Type == JTokenType.String)
                {
                    SpellsKnown = jSpellsKnown.Value<String>();
                    SpellsKnownDefinition = null;
                }
                else
                {
                    SpellsKnown = String.Empty;
                    SpellsKnownDefinition = new SpellsTable(jSpellsKnown.Value<JObject>());
                }
            }
            else
            {
                SpellsKnown = String.Empty;
                SpellsKnownDefinition = null;
            }
        }

        private void SelectSpellsPerDay(JObject jObject)
        {
            JToken jSpellsPerDay = jObject.SelectToken("SpellsPerDay", true);
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

        private void SelectSpellList(JObject jObject)
        {
            JToken jSpellList = jObject.SelectToken("SpellList", true);
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

        public bool IsArcane { get; }
        public bool IsSpontaneous { get; }
        public bool CanCopyScrolls { get; }
        public bool AllSpellsKnown { get; }
        public string CastingAttribute { get; }
        public string Cantrips { get; }
        public string SpellList { get; private set; }
        public string SpellsKnown { get; private set; }
        public string SpellsPerDay { get; private set; }
        public int SpellsPerLevel { get; }
        public int CasterLevelModifier { get; }
        public bool HasSpellListDefinition { get { return SpellListDefinition != null; } }
        public bool HasSpellsPerDayDefinition { get { return SpellsPerDayDefinition != null; } }
        public bool HasSpellsKnownDefinition { get { return SpellsKnownDefinition != null; } }
        public SpellList SpellListDefinition { get; private set; }
        public SpellsTable SpellsKnownDefinition { get; private set; }
        public SpellsTable SpellsPerDayDefinition { get; private set; }
    }
}
