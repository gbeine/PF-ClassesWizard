using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PF_Classes.JsonTypes;

namespace PF_Classes.JsonType
{
    [TestFixture]
    public class ProficienciesTest
    {
        [Test]
        public void TestProficiencies()
        {
            const string jsonString = "{ 'Guid': '6d817419f36c4ba7833466e434a7fbd9', 'Name': 'CharlatanProficiencies', 'DisplayName': 'Charlatan Proficiencies', 'Description': 'Proficiencies description', 'From': 'BARD_PROFICIENCIES', 'Add': { 'Features': [ 'WEAPON_PROFICIENCY_DUELING_SWORD', 'WEAPON_PROFICIENCY_ESTOC'],'WeaponProficiencies': [ 'Longbow', 'Starknife' ], 'ArmorProficiencies': [ 'Light', 'Buckler' ] } }";
            JObject jObject = JObject.Parse(jsonString);
            Proficiencies proficiencies = new Proficiencies(jObject);
            Assert.AreEqual("6d817419f36c4ba7833466e434a7fbd9",proficiencies.Guid);
            Assert.AreEqual("CharlatanProficiencies",proficiencies.Name);
            Assert.AreEqual("Charlatan Proficiencies",proficiencies.DisplayName);
            Assert.AreEqual("Proficiencies description",proficiencies.Description);
            Assert.AreEqual("BARD_PROFICIENCIES",proficiencies.From);
            Assert.IsNull(proficiencies.Icon);
            Assert.IsNotNull(proficiencies.AddFeatures);
            Assert.AreEqual(2, proficiencies.AddFeatures.Count);
            Assert.IsTrue(proficiencies.AddFeatures.Contains("WEAPON_PROFICIENCY_DUELING_SWORD"));
            Assert.IsTrue(proficiencies.AddFeatures.Contains("WEAPON_PROFICIENCY_ESTOC"));
            Assert.IsNotNull(proficiencies.AddWeaponProficiencies);
            Assert.AreEqual(2, proficiencies.AddWeaponProficiencies.Count);
            Assert.IsTrue(proficiencies.AddWeaponProficiencies.Contains("Longbow"));
            Assert.IsTrue(proficiencies.AddWeaponProficiencies.Contains("Starknife"));
            Assert.IsNotNull(proficiencies.AddArmorProficiencies);
            Assert.AreEqual(2, proficiencies.AddArmorProficiencies.Count);
            Assert.IsTrue(proficiencies.AddArmorProficiencies.Contains("Light"));
            Assert.IsTrue(proficiencies.AddArmorProficiencies.Contains("Buckler"));
        }

        [Test]
        public void TestMissingGuid()
        {
            const string jsonString = "{ 'Name': 'CharlatanProficiencies', 'DisplayName': 'Charlatan Proficiencies' }";
            JObject jObject = JObject.Parse(jsonString);
            Proficiencies proficiencies;
            Assert.Throws<JsonException>(() => proficiencies = new Proficiencies(jObject));
        }

        [Test]
        public void TestProficienciesOnlyRequired()
        {
            const string jsonString = "{ 'Guid': '6d817419f36c4ba7833466e434a7fbd9', 'Name': 'CharlatanProficiencies', 'DisplayName': 'Charlatan Proficiencies' }";
            JObject jObject = JObject.Parse(jsonString);
            Proficiencies proficiencies = new Proficiencies(jObject);
            Assert.AreEqual("6d817419f36c4ba7833466e434a7fbd9",proficiencies.Guid);
            Assert.AreEqual("CharlatanProficiencies",proficiencies.Name);
            Assert.AreEqual("Charlatan Proficiencies",proficiencies.DisplayName);
            Assert.AreEqual("Charlatan Proficiencies",proficiencies.Description);
            Assert.IsNull(proficiencies.From);
            Assert.IsNull(proficiencies.Icon);
            Assert.IsNotNull(proficiencies.AddFeatures);
            Assert.AreEqual(0, proficiencies.AddFeatures.Count);
            Assert.IsNotNull(proficiencies.AddWeaponProficiencies);
            Assert.AreEqual(0, proficiencies.AddWeaponProficiencies.Count);
            Assert.IsNotNull(proficiencies.AddArmorProficiencies);
            Assert.AreEqual(0, proficiencies.AddArmorProficiencies.Count);
        }
    }
}
