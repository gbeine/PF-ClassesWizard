using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class CharacterClass : JsonType
    {
        public CharacterClass(JObject jObject) : base(jObject)
        {
            DisplayName = jObject.SelectToken("DisplayName", true).Value<string>();
            Icon = jObject.SelectToken("Icon", true).Value<string>();
            EquipmentEntities = jObject.SelectToken("EquipmentEntities", true).Value<string>();
            PrimaryColor = jObject.SelectToken("PrimaryColor", true).Value<int>();
            SecondaryColor = jObject.SelectToken("SecondaryColor", true).Value<int>();
            SkillPoints = jObject.SelectToken("SkillPoints", true).Value<int>();

            Description = SelectString(jObject, "Description", DisplayName);
            MaleEquipmentEntities = SelectString(jObject, "MaleEquipmentEntities", EquipmentEntities);
            FemaleEquipmentEntities = SelectString(jObject, "FemaleEquipmentEntities", EquipmentEntities);
            ComponentsArray = SelectString(jObject, "ComponentsArray");
            StartingItems = SelectString(jObject, "StartingItems");

            HitDie = SelectString(jObject, "HitDie");
            BaseAttackBonus = SelectString(jObject, "BaseAttackBonus");
            FortitudeSave = SelectString(jObject, "FortitudeSave");
            WillSave = SelectString(jObject, "WillSave");
            ReflexSave = SelectString(jObject, "ReflexSave");

            IsDivineCaster = SelectBool(jObject, "IsDivineCaster");
            IsArcaneCaster = SelectBool(jObject, "IsArcaneCaster");

            StartingGold = SelectInt(jObject, "StartingGold");

            Alignment = SelectStringList(jObject, "Alignment", new []{ "Any" });
            ClassSkills = SelectStringList(jObject, "ClassSkills");
            RecommendedAttributes = SelectStringList(jObject, "RecommendedAttributes");
            NotRecommendedAttributes = SelectStringList(jObject, "NotRecommendedAttributes");

            JToken jSpellbook = jObject.SelectToken("Spellbook");
            JToken jCantrips = jObject.SelectToken("Cantrips");
            if (jSpellbook != null)
            {
                Spellbook = new Spellbook(jSpellbook.Value<JObject>());
            }
        }

        public string DisplayName { get; }
        public string Description { get; }
        public string Icon { get; }
        public string EquipmentEntities { get; }
        public string MaleEquipmentEntities { get; }
        public string FemaleEquipmentEntities { get; }
        public string HitDie { get; }
        public string BaseAttackBonus { get; }
        public string FortitudeSave { get; }
        public string WillSave { get; }
        public string ReflexSave { get; }
        public string ComponentsArray { get; }
        public string StartingItems { get; }
        public int? PrimaryColor { get; }
        public int? SecondaryColor { get; }
        public int? SkillPoints { get; }
        public bool? IsDivineCaster { get; }
        public bool? IsArcaneCaster { get; }
        public int? StartingGold { get; }
        public List<string> Alignment { get; }
        public List<string> ClassSkills { get; }
        public List<string> RecommendedAttributes { get; }
        public List<string> NotRecommendedAttributes { get; }
        public Spellbook Spellbook { get; }
    }
}
