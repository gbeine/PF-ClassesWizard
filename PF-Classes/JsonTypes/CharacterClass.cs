using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class CharacterClass : JsonType
    {
        private const string _docu = "https://github.com/gbeine/PF-ClassesWizard/blob/main/docs/CharacterClass.md";

        public CharacterClass(JObject jObject) : base(jObject)
        {
            DisplayName = SelectString(jObject, "DisplayName");
            From = SelectString(jObject, "From");
            Icon = SelectString(jObject, "Icon");
            EquipmentEntities = SelectString(jObject, "EquipmentEntities");
            PrimaryColor = SelectInt(jObject, "PrimaryColor");
            SecondaryColor = SelectInt(jObject, "SecondaryColor");
            SkillPoints = SelectInt(jObject, "SkillPoints");

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

            Alignment = SelectStringList(jObject, "Alignment");
            ClassSkills = SelectStringList(jObject, "ClassSkills");
            RecommendedAttributes = SelectStringList(jObject, "RecommendedAttributes");
            NotRecommendedAttributes = SelectStringList(jObject, "NotRecommendedAttributes");

            JToken jSpellbook = jObject.SelectToken("Spellbook");
            JToken jCantrips = jObject.SelectToken("Cantrips");
            if (jSpellbook != null)
            {
                Spellbook = new Spellbook(jSpellbook.Value<JObject>());
            }

            if (!isValid())
            {
                throw new InvalidDataException(
                    $"Character class {Name} need to define either from or other data fiels described here: {_docu}");
            }
        }

        protected bool isValid()
        {
            return !string.Empty.Equals(From)
                   || (!string.Empty.Equals(Icon)
                      && !string.Empty.Equals(EquipmentEntities)
                      && SkillPoints.HasValue
                      && PrimaryColor.HasValue
                      && SecondaryColor.HasValue);
        }

        public string From { get; }
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
