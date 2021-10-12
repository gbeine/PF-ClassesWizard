using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Proficiencies
    {
        public Proficiencies(JObject jObject)
        {
            Guid = jObject.SelectToken("Guid").Value<String>();
            Name = jObject.SelectToken("Name").Value<String>();
            DisplayName = jObject.SelectToken("DisplayName").Value<String>();
            Description = jObject.SelectToken("Description").Value<String>();

            JToken from = jObject.SelectToken("from");
            From = from != null ? from.Value<String>() : null;

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
       }

        public string Guid { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public List<String> AddFeatures { get; set; }
        public List<String> AddWeaponProficiencies { get; set; }
        public List<String> AddArmorProficiencies { get; set; }
    }
}
