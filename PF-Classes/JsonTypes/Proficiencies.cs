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
            DisplayName = jObject.SelectToken("DisplayName", true).Value<string>();

            DisplayName = SelectString(jObject, "DisplayName");
            Icon = SelectString(jObject, "Icon");
            From = SelectString(jObject, "From");
            Description = SelectString(jObject, "Description", DisplayName);

            JToken jAdd = jObject.SelectToken("Add");
            if (jAdd != null)
            {
                AddFeatures = SelectStringList(jAdd, "Features");
                AddWeaponProficiencies = SelectStringList(jAdd, "WeaponProficiencies");
                AddArmorProficiencies = SelectStringList(jAdd, "ArmorProficiencies");
            }
            else
            {
                AddFeatures = Array.Empty<string>().ToList();
                AddWeaponProficiencies = Array.Empty<string>().ToList();
                AddArmorProficiencies = Array.Empty<string>().ToList();
            }
        }

        public string DisplayName { get; }
        public string Description { get; }
        public string Icon { get; }
        public string From { get; }
        public List<string> AddFeatures { get; }
        public List<string> AddWeaponProficiencies { get; }
        public List<string> AddArmorProficiencies { get; }
    }
}
