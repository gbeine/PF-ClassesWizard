using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Proficiencies : JsonType
    {
        public Proficiencies(JObject jObject) : base(jObject)
        {
            DisplayName = jObject.SelectToken("DisplayName", true).Value<String>();

            DisplayName = SelectString(jObject, "DisplayName");
            Icon = SelectString(jObject, "Icon");
            From = SelectString(jObject, "From");
            Description = SelectString(jObject, "Description", DisplayName);

            JToken jAdd = jObject.SelectToken("Add");
            if (jAdd != null)
            {
                JToken jAddFeatures = jAdd.SelectToken("Features");
                AddFeatures = jAddFeatures != null
                    ? jAddFeatures.Values<String>().ToList()
                    : Array.Empty<String>().ToList();
                JToken jAddWeapon = jAdd.SelectToken("WeaponProficiencies");
                AddWeaponProficiencies = jAddWeapon != null
                    ? jAddWeapon.Values<String>().ToList()
                    : Array.Empty<String>().ToList();
                JToken jAddArmor = jAdd.SelectToken("ArmorProficiencies");
                AddArmorProficiencies = jAddArmor != null
                    ? jAddArmor.Values<String>().ToList()
                    : Array.Empty<String>().ToList();
            }
            else
            {
                AddFeatures = Array.Empty<String>().ToList();
                AddWeaponProficiencies = Array.Empty<String>().ToList();
                AddArmorProficiencies = Array.Empty<String>().ToList();
            }
        }

        public string DisplayName { get; }
        public string Description { get; }
        public string Icon { get; }
        public string From { get; }
        public List<String> AddFeatures { get; }
        public List<String> AddWeaponProficiencies { get; }
        public List<String> AddArmorProficiencies { get; }
    }
}
