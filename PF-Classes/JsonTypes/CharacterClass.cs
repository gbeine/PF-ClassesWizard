using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class CharacterClass
    {
        public CharacterClass(JObject jObject)
        {
            Guid = jObject.SelectToken("Guid", true).Value<String>();
            Name = jObject.SelectToken("Name", true).Value<String>();
            DisplayName = jObject.SelectToken("DisplayName", true).Value<String>();
            Icon = jObject.SelectToken("Icon", true).Value<String>();
            EquipmentEntities = jObject.SelectToken("EquipmentEntities", true).Value<String>();
            PrimaryColor = jObject.SelectToken("PrimaryColor", true).Value<int>();
            SecondaryColor = jObject.SelectToken("SecondaryColor", true).Value<int>();
            SkillPoints = jObject.SelectToken("SkillPoints", true).Value<int>();
            Progression = new Progression(jObject.SelectToken("Progression", true).Value<JObject>());

            JToken jDescription = jObject.SelectToken("Description");
            Description = jDescription != null
                ? jDescription.Value<String>()
                : DisplayName;
            JToken jMaleEquipmentEntities = jObject.SelectToken("MaleEquipmentEntities");
            MaleEquipmentEntities = jMaleEquipmentEntities != null
                ? jMaleEquipmentEntities.Value<String>()
                : EquipmentEntities;
            JToken jFemaleEquipmentEntities = jObject.SelectToken("FemaleEquipmentEntities");
            FemaleEquipmentEntities  = jFemaleEquipmentEntities != null
                ? jFemaleEquipmentEntities.Value<String>()
                : EquipmentEntities;

            JToken jHitDie = jObject.SelectToken("HitDie");
            HitDie = jHitDie != null
                ? jHitDie.Value<String>()
                : "D6";
            JToken jBaseAttackBonus = jObject.SelectToken("BaseAttackBonus");
            BaseAttackBonus = jBaseAttackBonus != null
                ? jBaseAttackBonus.Value<String>()
                : "BAB_LOW";
            JToken jFortitudeSave = jObject.SelectToken("FortitudeSave");
            FortitudeSave = jFortitudeSave != null
                ? jFortitudeSave.Value<String>()
                : "SAVES_LOW";
            JToken jWillSave = jObject.SelectToken("WillSave");
            WillSave = jWillSave != null
                ? jWillSave.Value<String>()
                : "SAVES_LOW";
            JToken jReflexSave = jObject.SelectToken("ReflexSave");
            ReflexSave = jReflexSave != null
                ? jReflexSave.Value<String>()
                : "SAVES_LOW";
            JToken jIsDivineCaster = jObject.SelectToken("IsDivineCaster");
            IsDivineCaster = jIsDivineCaster != null
                ? jIsDivineCaster.Value<bool>()
                : false;
            JToken jIsArcaneCaster = jObject.SelectToken("IsArcaneCaster");
            IsArcaneCaster = jIsArcaneCaster != null
                ? jIsArcaneCaster.Value<bool>()
                : false;
            JToken jStartingGold = jObject.SelectToken("StartingGold");
            StartingGold = jStartingGold != null
                    ? jStartingGold.Value<int>()
                    : 500;

            JToken jComponentsArray = jObject.SelectToken("ComponentsArray");
            ComponentsArray = jComponentsArray != null
                ? jComponentsArray.Value<String>()
                : null;
            JToken jStartingItems = jObject.SelectToken("StartingItems");
            StartingItems = jStartingItems != null
                ? jStartingItems.Value<String>()
                : null;

            JToken jAlignment = jObject.SelectToken("Alignment");
            Alignment = jAlignment != null ? jAlignment.Values<String>().ToList() : Array.Empty<String>().ToList();
            JToken jClassSkills = jObject.SelectToken("ClassSkills");
            ClassSkills = jClassSkills != null ? jClassSkills.Values<String>().ToList() : Array.Empty<String>().ToList();
            JToken jRecommendedAttributes = jObject.SelectToken("RecommendedAttributes");
            RecommendedAttributes = jRecommendedAttributes != null ? jRecommendedAttributes.Values<String>().ToList() : Array.Empty<String>().ToList();
            JToken jNotRecommendedAttributes = jObject.SelectToken("NotRecommendedAttributes");
            NotRecommendedAttributes = jNotRecommendedAttributes != null ? jNotRecommendedAttributes.Values<String>().ToList() : Array.Empty<String>().ToList();

            JToken jProficiencies = jObject.SelectToken("Proficiencies");
            if (jProficiencies != null)
            {
                Proficiencies = new Proficiencies(jProficiencies.Value<JObject>());
            }

            JToken jSpellbook = jObject.SelectToken("Spellbook");
            JToken jCantrips = jObject.SelectToken("Cantrips");
            if (jSpellbook != null)
            {
                if (jCantrips != null)
                {
                    Cantrips = new Cantrips(jCantrips.Value<JObject>());
                }
                Spellbook = new Spellbook(jSpellbook.Value<JObject>());
            }
        }
        public string Guid { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string EquipmentEntities { get; set; }
        public string MaleEquipmentEntities { get; set; }
        public string FemaleEquipmentEntities { get; set; }
        public int PrimaryColor { get; set; }
        public int SecondaryColor { get; set; }
        public int SkillPoints { get; set; }
        public string HitDie { get; set; }
        public string BaseAttackBonus { get; set; }
        public string FortitudeSave { get; set; }
        public string WillSave { get; set; }
        public string ReflexSave { get; set; }
        public bool IsDivineCaster { get; set; }
        public bool IsArcaneCaster { get; set; }
        public string ComponentsArray { get; set; }
        public int StartingGold { get; set; }
        public string StartingItems { get; set; }
        public List<String> Alignment { get; set; }
        public List<String> ClassSkills { get; set; }
        public List<String> RecommendedAttributes { get; set; }
        public List<String> NotRecommendedAttributes { get; set; }
        public Cantrips Cantrips { get; set; }
        public Proficiencies Proficiencies { get; set; }
        public Progression Progression { get; set; }
        public Spellbook Spellbook { get; set; }
    }
}
