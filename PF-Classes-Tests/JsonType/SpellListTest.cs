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
            const string jsonString = "{ 'Guid': '8b4fc86d687646648c551a740718118c', 'Name': 'CharlatanSpellList', 'SpellsByLevel': { '0': [ 'CURE_LIGHT_WOUNDS_CAST' ], '1': [ 'CURE_LIGHT_WOUNDS_CAST', 'SUMMON_MONSTER_I_SINGLE' ], '2': [ 'CURE_MODERATE_WOUNDS_CAST', 'SUMMON_MONSTER_II_BASE', 'MAGE_ARMOR', 'DELAY_POISON' ], '3': [ 'CURE_SERIOUS_WOUNDS_CAST', 'SUMMON_MONSTER_III_BASE', 'RESTORATION_LESSER' ], '4': [ 'CURE_CRITICAL_WOUNDS_CAST', 'SUMMON_MONSTER_IV_BASE', 'DELAY_POISON_COMMUNAL' ] } }";
            JObject jObject = JObject.Parse(jsonString);
            SpellList spellList = new SpellList(jObject);
            Assert.AreEqual("8b4fc86d687646648c551a740718118c",spellList.Guid);
            Assert.AreEqual("CharlatanSpellList",spellList.Name);
            Assert.AreEqual(5,spellList.Level);
            Assert.AreEqual(5,spellList.SpellsByLevel.Capacity);
            Assert.AreEqual(5,spellList.SpellsByLevel.Count);
            Assert.AreEqual(1,spellList.SpellsByLevel[0].Count);
            Assert.IsTrue(spellList.SpellsByLevel[0].Contains("CURE_LIGHT_WOUNDS_CAST"));
            Assert.AreEqual(2,spellList.SpellsByLevel[1].Count);
            Assert.IsTrue(spellList.SpellsByLevel[1].Contains("CURE_LIGHT_WOUNDS_CAST"));
            Assert.IsTrue(spellList.SpellsByLevel[1].Contains("SUMMON_MONSTER_I_SINGLE"));
            Assert.AreEqual(3,spellList.SpellsByLevel[4].Count);
            Assert.IsTrue(spellList.SpellsByLevel[4].Contains("CURE_CRITICAL_WOUNDS_CAST"));
            Assert.IsTrue(spellList.SpellsByLevel[4].Contains("SUMMON_MONSTER_IV_BASE"));
            Assert.IsTrue(spellList.SpellsByLevel[4].Contains("DELAY_POISON_COMMUNAL"));
        }

        [Test]
        public void TestMissingGuid()
        {
            const string jsonString = "{ 'Name': 'CharlatanSpellList', 'SpellsByLevel': { '0': [ 'CURE_LIGHT_WOUNDS_CAST' ], '1': [ 'CURE_LIGHT_WOUNDS_CAST', 'SUMMON_MONSTER_I_SINGLE' ], '2': [ 'CURE_MODERATE_WOUNDS_CAST', 'SUMMON_MONSTER_II_BASE', 'MAGE_ARMOR', 'DELAY_POISON' ], '3': [ 'CURE_SERIOUS_WOUNDS_CAST', 'SUMMON_MONSTER_III_BASE', 'RESTORATION_LESSER' ], '4': [ 'CURE_CRITICAL_WOUNDS_CAST', 'SUMMON_MONSTER_IV_BASE', 'DELAY_POISON_COMMUNAL' ] } }";
            JObject jObject = JObject.Parse(jsonString);
            SpellList spellList;
            Assert.Throws<JsonException>(() => spellList = new SpellList(jObject));
        }

        [Test]
        public void TestMissingSpellLevel()
        {
            const string jsonString = "{ 'Guid': '8b4fc86d687646648c551a740718118c', 'Name': 'CharlatanSpellList', 'SpellsByLevel': { '0': [ 'CURE_LIGHT_WOUNDS_CAST' ], '1': [ 'CURE_LIGHT_WOUNDS_CAST', 'SUMMON_MONSTER_I_SINGLE' ], '3': [ 'CURE_SERIOUS_WOUNDS_CAST', 'SUMMON_MONSTER_III_BASE', 'RESTORATION_LESSER' ], '4': [ 'CURE_CRITICAL_WOUNDS_CAST', 'SUMMON_MONSTER_IV_BASE', 'DELAY_POISON_COMMUNAL' ] } }";
            JObject jObject = JObject.Parse(jsonString);
            SpellList spellList;
            Assert.Throws<JsonException>(() => spellList = new SpellList(jObject));
        }

        [Test]
        public void TestSpellListLevelEmpty()
        {
            const string jsonString = "{ 'Guid': '8b4fc86d687646648c551a740718118c', 'Name': 'CharlatanSpellList', 'SpellsByLevel': { '0': [ ], '1': [ 'CURE_LIGHT_WOUNDS_CAST', 'SUMMON_MONSTER_I_SINGLE' ], '2': [ 'CURE_MODERATE_WOUNDS_CAST', 'SUMMON_MONSTER_II_BASE', 'MAGE_ARMOR', 'DELAY_POISON' ], '3': [ ], '4': [ 'CURE_CRITICAL_WOUNDS_CAST', 'SUMMON_MONSTER_IV_BASE', 'DELAY_POISON_COMMUNAL' ] } }";
            JObject jObject = JObject.Parse(jsonString);
            SpellList spellList = new SpellList(jObject);
            Assert.AreEqual("8b4fc86d687646648c551a740718118c",spellList.Guid);
            Assert.AreEqual("CharlatanSpellList",spellList.Name);
            Assert.AreEqual(5,spellList.Level);
            Assert.AreEqual(5,spellList.SpellsByLevel.Capacity);
            Assert.AreEqual(5,spellList.SpellsByLevel.Count);
            Assert.AreEqual(0,spellList.SpellsByLevel[0].Count);
            Assert.AreEqual(2,spellList.SpellsByLevel[1].Count);
            Assert.IsTrue(spellList.SpellsByLevel[1].Contains("CURE_LIGHT_WOUNDS_CAST"));
            Assert.IsTrue(spellList.SpellsByLevel[1].Contains("SUMMON_MONSTER_I_SINGLE"));
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
