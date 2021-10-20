using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Spellbook : JsonType
    {
        public Spellbook(JObject jObject) : base(jObject)
        {
            CastingAttribute = jObject.SelectToken("CastingAttribute", true).Value<string>();
            DisplayName = SelectString(jObject, "DisplayName");
            Description = SelectString(jObject, "Description", DisplayName);
            Icon = SelectString(jObject, "Icon");
            From = SelectString(jObject, "From");
            CharacterClass = SelectString(jObject, "CharacterClass");

            IsArcane = SelectBool(jObject, "IsArcane");
            IsSpontaneous = SelectBool(jObject, "IsSpontaneous");
            CanCopyScrolls = SelectBool(jObject, "CanCopyScrolls");
            AllSpellsKnown = SelectBool(jObject, "AllSpellsKnown");

            Cantrips = SelectString(jObject, "Cantrips");
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
                    SpellsKnown = jSpellsKnown.Value<string>();
                    SpellsKnownDefinition = null;
                }
                else
                {
                    SpellsKnown = string.Empty;
                    SpellsKnownDefinition = new SpellsTable(jSpellsKnown.Value<JObject>());
                }
            }
            else
            {
                SpellsKnown = string.Empty;
                SpellsKnownDefinition = null;
            }
        }

        private void SelectSpellsPerDay(JObject jObject)
        {
            JToken jSpellsPerDay = jObject.SelectToken("SpellsPerDay", true);
            if (jSpellsPerDay.Type == JTokenType.String)
            {
                SpellsPerDay = jSpellsPerDay.Value<string>();
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
                SpellList = jSpellList.Value<string>();
                SpellListDefinition = null;
            }
            else
            {
                SpellList = null;
                SpellListDefinition = new SpellList(jSpellList.Value<JObject>());
            }
        }

        public bool? IsArcane { get; }
        public bool? IsSpontaneous { get; }
        public bool? CanCopyScrolls { get; }
        public bool? AllSpellsKnown { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public string From { get; }
        public string Icon { get; }
        public string CharacterClass { get; }
        public string CastingAttribute { get; }
        public string Cantrips { get; }
        public string SpellList { get; private set; }
        public string SpellsKnown { get; private set; }
        public string SpellsPerDay { get; private set; }
        public int? SpellsPerLevel { get; }
        public int? CasterLevelModifier { get; }
        public bool HasSpellListDefinition { get { return SpellListDefinition != null; } }
        public bool HasSpellsPerDayDefinition { get { return SpellsPerDayDefinition != null; } }
        public bool HasSpellsKnownDefinition { get { return SpellsKnownDefinition != null; } }
        public SpellList SpellListDefinition { get; private set; }
        public SpellsTable SpellsKnownDefinition { get; private set; }
        public SpellsTable SpellsPerDayDefinition { get; private set; }
    }
}
