using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PF_Classes.JsonTypes;

namespace PF_Classes.JsonType
{
    [TestFixture]
    public class CharacterClassTest
    {
        [Test]
        public void TestCharacterClassNoOptional()
        {
            String jsonString = File.ReadAllText("CharlatanNoOptionals.json");
            JObject jObject = JObject.Parse(jsonString);
            CharacterClass characterClass = new CharacterClass(jObject);

            Assert.AreEqual("4da4a7d55cee43608a64babeb8d3ca73",characterClass.Guid);
            Assert.AreEqual("CharlatanClass",characterClass.Name);
            Assert.AreEqual("Charlatan",characterClass.DisplayName);
            Assert.AreEqual("ref:KINETICIST",characterClass.Icon);
            Assert.AreEqual("ref:KINETICIST",characterClass.EquipmentEntities);
            Assert.AreEqual(0,characterClass.PrimaryColor);
            Assert.AreEqual(19,characterClass.SecondaryColor);
            Assert.AreEqual(5,characterClass.SkillPoints);
            Assert.IsNotNull(characterClass.Progression);

            // defaults for optional values
            Assert.AreEqual("Charlatan",characterClass.Description);
            Assert.AreEqual("ref:KINETICIST",characterClass.MaleEquipmentEntities);
            Assert.AreEqual("ref:KINETICIST",characterClass.FemaleEquipmentEntities);
            Assert.AreEqual("D6",characterClass.HitDie);
            Assert.AreEqual("BAB_LOW",characterClass.BaseAttackBonus);
            Assert.AreEqual("SAVES_LOW",characterClass.FortitudeSave);
            Assert.AreEqual("SAVES_LOW",characterClass.WillSave);
            Assert.AreEqual("SAVES_LOW",characterClass.ReflexSave);
            Assert.AreEqual(411,characterClass.StartingGold);
            Assert.IsFalse(characterClass.IsArcaneCaster);
            Assert.IsFalse(characterClass.IsDivineCaster);
            Assert.Contains("Any", characterClass.Alignment);
            Assert.IsEmpty(characterClass.ClassSkills);
            Assert.IsEmpty(characterClass.RecommendedAttributes);
            Assert.IsEmpty(characterClass.NotRecommendedAttributes);
            Assert.IsEmpty(characterClass.ComponentsArray);
            Assert.IsEmpty(characterClass.StartingItems);
            Assert.IsNull(characterClass.Proficiencies);
            Assert.IsNull(characterClass.Cantrips);
            Assert.IsNull(characterClass.Spellbook);
        }

        [Test]
        public void TestCharacterOptionalsNoSpellbook()
        {
            String jsonString = File.ReadAllText("CharlatanOptionalsNoSpellbook.json");
            JObject jObject = JObject.Parse(jsonString);
            CharacterClass characterClass = new CharacterClass(jObject);

            Assert.AreEqual("4da4a7d55cee43608a64babeb8d3ca73",characterClass.Guid);
            Assert.AreEqual("CharlatanClass",characterClass.Name);
            Assert.AreEqual("Charlatan",characterClass.DisplayName);
            Assert.AreEqual("ref:KINETICIST",characterClass.Icon);
            Assert.AreEqual("ref:KINETICIST",characterClass.EquipmentEntities);
            Assert.AreEqual(0,characterClass.PrimaryColor);
            Assert.AreEqual(19,characterClass.SecondaryColor);
            Assert.AreEqual(5,characterClass.SkillPoints);
            Assert.IsNotNull(characterClass.Progression);

            // optional values
            Assert.AreEqual("The charlatan is a jolly fellow.",characterClass.Description);
            Assert.AreEqual("ref:ROGUE",characterClass.MaleEquipmentEntities);
            Assert.AreEqual("ref:BARD",characterClass.FemaleEquipmentEntities);
            Assert.AreEqual("D8",characterClass.HitDie);
            Assert.AreEqual("BAB_MEDIUM",characterClass.BaseAttackBonus);
            Assert.AreEqual("SAVES_HIGH",characterClass.FortitudeSave);
            Assert.AreEqual("SAVES_LOW",characterClass.WillSave);
            Assert.AreEqual("SAVES_HIGH",characterClass.ReflexSave);
            Assert.AreEqual(100,characterClass.StartingGold);
            Assert.IsTrue(characterClass.IsArcaneCaster);
            Assert.IsTrue(characterClass.IsDivineCaster);
            Assert.IsNotEmpty(characterClass.Alignment);
            Assert.IsNotEmpty(characterClass.ClassSkills);
            Assert.IsNotEmpty(characterClass.RecommendedAttributes);
            Assert.IsEmpty(characterClass.NotRecommendedAttributes);
            Assert.AreEqual("ref:ROGUE", characterClass.ComponentsArray);
            Assert.AreEqual("ref:ROGUE", characterClass.StartingItems);
            Assert.IsNotNull(characterClass.Proficiencies);

            Assert.IsNull(characterClass.Cantrips);
            Assert.IsNull(characterClass.Spellbook);
        }

        [Test]
        public void TestCharacterFullSpecified()
        {
            String jsonString = File.ReadAllText("CharlatanFullSpecified.json");
            JObject jObject = JObject.Parse(jsonString);
            CharacterClass characterClass = new CharacterClass(jObject);

            Assert.AreEqual("4da4a7d55cee43608a64babeb8d3ca73",characterClass.Guid);
            Assert.AreEqual("CharlatanClass",characterClass.Name);
            Assert.AreEqual("Charlatan",characterClass.DisplayName);
            Assert.AreEqual("ref:KINETICIST",characterClass.Icon);
            Assert.AreEqual("ref:KINETICIST",characterClass.EquipmentEntities);
            Assert.AreEqual(0,characterClass.PrimaryColor);
            Assert.AreEqual(19,characterClass.SecondaryColor);
            Assert.AreEqual(5,characterClass.SkillPoints);
            Assert.IsNotNull(characterClass.Progression);

            // optional values
            Assert.IsTrue(characterClass.IsArcaneCaster);
            Assert.IsFalse(characterClass.IsDivineCaster);
            Assert.IsNotEmpty(characterClass.Alignment);
            Assert.IsNotEmpty(characterClass.ClassSkills);
            Assert.IsNotEmpty(characterClass.RecommendedAttributes);
            Assert.IsEmpty(characterClass.NotRecommendedAttributes);
            Assert.AreEqual("ref:ROGUE", characterClass.ComponentsArray);
            Assert.AreEqual("ref:ROGUE", characterClass.StartingItems);
            Assert.IsNotNull(characterClass.Proficiencies);
            Assert.IsNotNull(characterClass.Cantrips);
            Assert.IsNotNull(characterClass.Spellbook);
        }

        [Test]
        public void TestCharacterClassCantripsWithoutSpellbook()
        {
            String jsonString = File.ReadAllText("CharlatanCantripsWithoutSpellbook.json");
            JObject jObject = JObject.Parse(jsonString);
            CharacterClass characterClass = new CharacterClass(jObject);

            Assert.AreEqual("4da4a7d55cee43608a64babeb8d3ca73",characterClass.Guid);
            Assert.AreEqual("CharlatanClass",characterClass.Name);
            Assert.AreEqual("Charlatan",characterClass.DisplayName);
            Assert.AreEqual("ref:KINETICIST",characterClass.Icon);
            Assert.AreEqual("ref:KINETICIST",characterClass.EquipmentEntities);
            Assert.AreEqual(0,characterClass.PrimaryColor);
            Assert.AreEqual(19,characterClass.SecondaryColor);
            Assert.AreEqual(5,characterClass.SkillPoints);

            Assert.IsNull(characterClass.Cantrips);
            Assert.IsNull(characterClass.Spellbook);
        }

        [Test]
        public void TestMissingGuid()
        {
            String jsonString = File.ReadAllText("CharlatanNoOptionalsMissingGuid.json");
            JObject jObject = JObject.Parse(jsonString);
            CharacterClass characterClass;
            Assert.Throws<JsonException>(() => characterClass = new CharacterClass(jObject));
        }
    }
}
