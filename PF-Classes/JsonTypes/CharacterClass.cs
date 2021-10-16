using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class CharacterClass : JsonType
    {
        public CharacterClass(JObject jObject) : base(jObject)
        {
            DisplayName = jObject.SelectToken("DisplayName", true).Value<String>();
            Icon = jObject.SelectToken("Icon", true).Value<String>();
            EquipmentEntities = jObject.SelectToken("EquipmentEntities", true).Value<String>();
            PrimaryColor = jObject.SelectToken("PrimaryColor", true).Value<int>();
            SecondaryColor = jObject.SelectToken("SecondaryColor", true).Value<int>();
            SkillPoints = jObject.SelectToken("SkillPoints", true).Value<int>();
            Progression = new Progression(jObject.SelectToken("Progression", true).Value<JObject>());

            Description = SelectString(jObject, "Description", DisplayName);
            MaleEquipmentEntities = SelectString(jObject, "MaleEquipmentEntities", EquipmentEntities);
            FemaleEquipmentEntities = SelectString(jObject, "FemaleEquipmentEntities", EquipmentEntities);
            ComponentsArray = SelectString(jObject, "ComponentsArray");
            StartingItems = SelectString(jObject, "StartingItems");

            HitDie = SelectString(jObject, "HitDie", "D6");
            BaseAttackBonus = SelectString(jObject, "BaseAttackBonus", "BAB_LOW");
            FortitudeSave = SelectString(jObject, "FortitudeSave", "SAVES_LOW");
            WillSave = SelectString(jObject, "WillSave", "SAVES_LOW");
            ReflexSave = SelectString(jObject, "ReflexSave", "SAVES_LOW");

            IsDivineCaster = SelectBool(jObject, "IsDivineCaster", false);
            IsArcaneCaster = SelectBool(jObject, "IsArcaneCaster", false);

            StartingGold = SelectInt(jObject, "StartingGold", 411);

            JToken jAlignment = jObject.SelectToken("Alignment");
            Alignment = jAlignment != null ? jAlignment.Values<String>().ToList() : new List<String>{ "Any" };
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

            SelectFeatures(jObject.SelectToken("Features"));
            SelectFeatureSelections(jObject.SelectToken("FeatureSelections"));
        }

        private void SelectFeatures(JToken jFeatures)
        {
            if (jFeatures != null)
            {
                Features = new List<Feature>();
                foreach (var jFeature in jFeatures.Value<JArray>())
                {
                    Features.Add(new Feature(jFeature.Value<JObject>()));
                }
            }
            else
            {
                Features = Array.Empty<Feature>().ToList();
            }
        }

        private void SelectFeatureSelections(JToken jFeatureSelections)
        {
            if (jFeatureSelections != null)
            {
                FeatureSelections = new List<FeatureSelection>();
                foreach (var jFeatureSelection in jFeatureSelections.Value<JArray>())
                {
                    FeatureSelections.Add(new FeatureSelection(jFeatureSelection.Value<JObject>()));
                }
            }
            else
            {
                FeatureSelections = Array.Empty<FeatureSelection>().ToList();
            }
        }

        public string DisplayName { get; }
        public string Description { get; }
        public string Icon { get; }
        public string EquipmentEntities { get; }
        public string MaleEquipmentEntities { get; }
        public string FemaleEquipmentEntities { get; }
        public int PrimaryColor { get; }
        public int SecondaryColor { get; }
        public int SkillPoints { get; }
        public string HitDie { get; }
        public string BaseAttackBonus { get; }
        public string FortitudeSave { get; }
        public string WillSave { get; }
        public string ReflexSave { get; }
        public bool IsDivineCaster { get; }
        public bool IsArcaneCaster { get; }
        public string ComponentsArray { get; }
        public int StartingGold { get; }
        public string StartingItems { get; }
        public List<String> Alignment { get; }
        public List<String> ClassSkills { get; }
        public List<String> RecommendedAttributes { get; }
        public List<String> NotRecommendedAttributes { get; }
        public List<Feature> Features { get; private set; }
        public List<FeatureSelection> FeatureSelections { get; private set; }
        public Cantrips Cantrips { get; }
        public Proficiencies Proficiencies { get; }
        public Progression Progression { get; }
        public Spellbook Spellbook { get; }
    }
}
