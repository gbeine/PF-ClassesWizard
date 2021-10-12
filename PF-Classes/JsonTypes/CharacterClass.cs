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
            Guid = jObject.SelectToken("Guid").Value<String>();
            Name = jObject.SelectToken("Name").Value<String>();
            DisplayName = jObject.SelectToken("DisplayName").Value<String>();
            Description = jObject.SelectToken("Description").Value<String>();
            Icon = jObject.SelectToken("Icon").Value<String>();
            EquipmentEntities = jObject.SelectToken("EquipmentEntities").Value<String>();
            MaleEquipmentEntities = jObject.SelectToken("MaleEquipmentEntities").Value<String>();
            FemaleEquipmentEntities = jObject.SelectToken("FemaleEquipmentEntities").Value<String>();
            PrimaryColor = jObject.SelectToken("PrimaryColor").Value<int>();
            SecondaryColor = jObject.SelectToken("SecondaryColor").Value<int>();
            SkillPoints = jObject.SelectToken("SkillPoints").Value<int>();
            HitDie = jObject.SelectToken("HitDie").Value<String>();
            BaseAttackBonus = jObject.SelectToken("BaseAttackBonus").Value<String>();
            FortitudeSave = jObject.SelectToken("FortitudeSave").Value<String>();
            WillSave = jObject.SelectToken("WillSave").Value<String>();
            ReflexSave = jObject.SelectToken("ReflexSave").Value<String>();
            IsDivineCaster = jObject.SelectToken("IsDivineCaster").Value<bool>();
            IsArcaneCaster = jObject.SelectToken("IsArcaneCaster").Value<bool>();
            ComponentsArray = jObject.SelectToken("ComponentsArray").Value<String>();
            StartingGold = jObject.SelectToken("StartingGold").Value<int>();
            StartingItems = jObject.SelectToken("StartingItems").Value<String>();

            Progression = new Progression(jObject.SelectToken("Progression").Value<JObject>());

            JToken alignment = jObject.SelectToken("Alignment"); 
            Alignment = alignment != null ? alignment.Values<String>().ToList() : Array.Empty<String>().ToList();
            JToken classSkills = jObject.SelectToken("ClassSkills"); 
            ClassSkills = classSkills != null ? classSkills.Values<String>().ToList() : Array.Empty<String>().ToList();
            JToken recommendedAttributes = jObject.SelectToken("RecommendedAttributes"); 
            RecommendedAttributes = recommendedAttributes != null ? recommendedAttributes.Values<String>().ToList() : Array.Empty<String>().ToList();
            JToken notRecommendedAttributes = jObject.SelectToken("NotRecommendedAttributes"); 
            NotRecommendedAttributes = notRecommendedAttributes != null ? notRecommendedAttributes.Values<String>().ToList() : Array.Empty<String>().ToList();
            

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
