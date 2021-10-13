using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PF_Classes.JsonTypes;

namespace PF_Classes.JsonType
{
    [TestFixture]
    public class SpellListTest
    {
        [Test]
        public void TestSpellList()
        {
            const string jsonString = "{ 'Guid': '8b4fc86d687646648c551a740718118c', 'Name': 'CharlatanSpellList', 'SpellsByLevel': { '0': [ 'CONJURATION_CURE_LIGHT_WOUNDS_CAST' ], '1': [ 'CONJURATION_CURE_LIGHT_WOUNDS_CAST', 'CONJURATION_SUMMON_MONSTER_I_SINGLE' ], '2': [ 'CONJURATION_CURE_MODERATE_WOUNDS_CAST', 'CONJURATION_SUMMON_MONSTER_II_BASE', 'CONJURATION_MAGE_ARMOR', 'CONJURATION_DELAY_POISON' ], '3': [ 'CONJURATION_CURE_SERIOUS_WOUNDS_CAST', 'CONJURATION_SUMMON_MONSTER_III_BASE', 'CONJURATION_RESTORATION_LESSER' ], '4': [ 'CONJURATION_CURE_CRITICAL_WOUNDS_CAST', 'CONJURATION_SUMMON_MONSTER_IV_BASE', 'CONJURATION_DELAY_POISON_COMMUNAL' ] } }";
            JObject jObject = JObject.Parse(jsonString);
            SpellList spellList = new SpellList(jObject);
            Assert.AreEqual("8b4fc86d687646648c551a740718118c",spellList.Guid);
            Assert.AreEqual("CharlatanSpellList",spellList.Name);
            Assert.AreEqual(4,spellList.Level);
            Assert.AreEqual(5,spellList.SpellsByLevel.Capacity);
            Assert.AreEqual(5,spellList.SpellsByLevel.Count);
            Assert.AreEqual(1,spellList.SpellsByLevel[0].Count);
            Assert.IsTrue(spellList.SpellsByLevel[0].Contains("CONJURATION_CURE_LIGHT_WOUNDS_CAST"));
            Assert.AreEqual(2,spellList.SpellsByLevel[1].Count);
            Assert.IsTrue(spellList.SpellsByLevel[1].Contains("CONJURATION_CURE_LIGHT_WOUNDS_CAST"));
            Assert.IsTrue(spellList.SpellsByLevel[1].Contains("CONJURATION_SUMMON_MONSTER_I_SINGLE"));
            Assert.AreEqual(3,spellList.SpellsByLevel[4].Count);
            Assert.IsTrue(spellList.SpellsByLevel[4].Contains("CONJURATION_CURE_CRITICAL_WOUNDS_CAST"));
            Assert.IsTrue(spellList.SpellsByLevel[4].Contains("CONJURATION_SUMMON_MONSTER_IV_BASE"));
            Assert.IsTrue(spellList.SpellsByLevel[4].Contains("CONJURATION_DELAY_POISON_COMMUNAL"));
        }

        [Test]
        public void TestMissingGuid()
        {
            const string jsonString = "{ 'Name': 'CharlatanSpellList', 'SpellsByLevel': { '0': [ 'CONJURATION_CURE_LIGHT_WOUNDS_CAST' ], '1': [ 'CONJURATION_CURE_LIGHT_WOUNDS_CAST', 'CONJURATION_SUMMON_MONSTER_I_SINGLE' ], '2': [ 'CONJURATION_CURE_MODERATE_WOUNDS_CAST', 'CONJURATION_SUMMON_MONSTER_II_BASE', 'CONJURATION_MAGE_ARMOR', 'CONJURATION_DELAY_POISON' ], '3': [ 'CONJURATION_CURE_SERIOUS_WOUNDS_CAST', 'CONJURATION_SUMMON_MONSTER_III_BASE', 'CONJURATION_RESTORATION_LESSER' ], '4': [ 'CONJURATION_CURE_CRITICAL_WOUNDS_CAST', 'CONJURATION_SUMMON_MONSTER_IV_BASE', 'CONJURATION_DELAY_POISON_COMMUNAL' ] } }";
            JObject jObject = JObject.Parse(jsonString);
            SpellList spellList;
            Assert.Throws<JsonException>(() => spellList = new SpellList(jObject));
        }

        [Test]
        public void TestMissingSpellLevel()
        {
            const string jsonString = "{ 'Guid': '8b4fc86d687646648c551a740718118c', 'Name': 'CharlatanSpellList', 'SpellsByLevel': { '0': [ 'CONJURATION_CURE_LIGHT_WOUNDS_CAST' ], '1': [ 'CONJURATION_CURE_LIGHT_WOUNDS_CAST', 'CONJURATION_SUMMON_MONSTER_I_SINGLE' ], '3': [ 'CONJURATION_CURE_SERIOUS_WOUNDS_CAST', 'CONJURATION_SUMMON_MONSTER_III_BASE', 'CONJURATION_RESTORATION_LESSER' ], '4': [ 'CONJURATION_CURE_CRITICAL_WOUNDS_CAST', 'CONJURATION_SUMMON_MONSTER_IV_BASE', 'CONJURATION_DELAY_POISON_COMMUNAL' ] } }";
            JObject jObject = JObject.Parse(jsonString);
            SpellList spellList;
            Assert.Throws<JsonException>(() => spellList = new SpellList(jObject));
        }

        [Test]
        public void TestSpellListLevelEmpty()
        {
            const string jsonString = "{ 'Guid': '8b4fc86d687646648c551a740718118c', 'Name': 'CharlatanSpellList', 'SpellsByLevel': { '0': [ ], '1': [ 'CONJURATION_CURE_LIGHT_WOUNDS_CAST', 'CONJURATION_SUMMON_MONSTER_I_SINGLE' ], '2': [ 'CONJURATION_CURE_MODERATE_WOUNDS_CAST', 'CONJURATION_SUMMON_MONSTER_II_BASE', 'CONJURATION_MAGE_ARMOR', 'CONJURATION_DELAY_POISON' ], '3': [ ], '4': [ 'CONJURATION_CURE_CRITICAL_WOUNDS_CAST', 'CONJURATION_SUMMON_MONSTER_IV_BASE', 'CONJURATION_DELAY_POISON_COMMUNAL' ] } }";
            JObject jObject = JObject.Parse(jsonString);
            SpellList spellList = new SpellList(jObject);
            Assert.AreEqual("8b4fc86d687646648c551a740718118c",spellList.Guid);
            Assert.AreEqual("CharlatanSpellList",spellList.Name);
            Assert.AreEqual(4,spellList.Level);
            Assert.AreEqual(5,spellList.SpellsByLevel.Capacity);
            Assert.AreEqual(5,spellList.SpellsByLevel.Count);
            Assert.AreEqual(0,spellList.SpellsByLevel[0].Count);
            Assert.AreEqual(2,spellList.SpellsByLevel[1].Count);
            Assert.IsTrue(spellList.SpellsByLevel[1].Contains("CONJURATION_CURE_LIGHT_WOUNDS_CAST"));
            Assert.IsTrue(spellList.SpellsByLevel[1].Contains("CONJURATION_SUMMON_MONSTER_I_SINGLE"));
            Assert.AreEqual(0,spellList.SpellsByLevel[3].Count);
        }

        [Test]
        public void TestSpellListEmpty()
        {
            const string jsonString = "{ 'Guid': '8b4fc86d687646648c551a740718118c', 'Name': 'CharlatanSpellList', 'SpellsByLevel': { } } ";
            JObject jObject = JObject.Parse(jsonString);
            SpellList spellList = new SpellList(jObject);
            Assert.AreEqual("8b4fc86d687646648c551a740718118c",spellList.Guid);
            Assert.AreEqual("CharlatanSpellList",spellList.Name);
            Assert.AreEqual(0,spellList.Level);
            Assert.AreEqual(0,spellList.SpellsByLevel.Capacity);
            Assert.AreEqual(0,spellList.SpellsByLevel.Count);
        }
    }
}
