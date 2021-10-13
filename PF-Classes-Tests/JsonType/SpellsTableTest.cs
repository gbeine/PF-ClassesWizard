using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PF_Classes.JsonTypes;
using static Newtonsoft.Json.Linq.JObject;

namespace PF_Classes.JsonType
{
    [TestFixture]
    public class SpellsTableTest
    {
        [Test]
        public void TestSpellsTable()
        {
            const string jsonString = "{ 'Name': 'CharlatanSpellsKnown', 'Guid': '69b34210916a46fc8dd031950aa5d9b7', 'Table': { '0': [0, 0], '1': [0, 6], '2': [0, 6], '3': [0, 6, 2], '4': [0, 6, 2], '5': [0, 6, 2, 2], '6': [0, 6, 2, 2], '7': [0, 6, 4, 2, 2], '8': [0, 6, 4, 2, 2], '9': [0, 7, 4, 4, 2, 2], '10': [0, 7, 4, 4, 2, 2], '11': [0, 7, 6, 4, 4, 2, 2], '12': [0, 7, 6, 4, 4, 2, 2], '13': [0, 8, 6, 6, 4, 4, 2, 2], '14': [0, 8, 6, 6, 4, 4, 2, 2], '15': [0, 8, 7, 6, 6, 4, 4, 2, 2], '16': [0, 8, 7, 6, 6, 4, 4, 2, 2], '17': [0, 8, 7, 7, 6, 6, 4, 4, 2, 2], '18': [0, 8, 7, 7, 6, 6, 4, 4, 4, 2], '19': [0, 8, 8, 7, 7, 6, 6, 4, 4, 4], '20': [0, 8, 8, 7, 7, 6, 6, 4, 4, 4] } }";
            JObject jObject = Parse(jsonString);
            SpellsTable spellsTable = new SpellsTable(jObject);
            Assert.AreEqual("69b34210916a46fc8dd031950aa5d9b7",spellsTable.Guid);
            Assert.AreEqual("CharlatanSpellsKnown",spellsTable.Name);
            Assert.AreEqual(21,spellsTable.Table.Capacity);
            Assert.AreEqual(21,spellsTable.Table.Count);
        }

        [Test]
        public void TestMissingGuid()
        {
            const string jsonString = "{ 'Name': 'CharlatanSpellsKnown', 'Table': { '0': [0, 0], '1': [0, 6], '2': [0, 6], '3': [0, 6, 2], '4': [0, 6, 2], '5': [0, 6, 2, 2], '6': [0, 6, 2, 2], '7': [0, 6, 4, 2, 2], '8': [0, 6, 4, 2, 2], '9': [0, 7, 4, 4, 2, 2], '10': [0, 7, 4, 4, 2, 2], '11': [0, 7, 6, 4, 4, 2, 2], '12': [0, 7, 6, 4, 4, 2, 2], '13': [0, 8, 6, 6, 4, 4, 2, 2], '14': [0, 8, 6, 6, 4, 4, 2, 2], '15': [0, 8, 7, 6, 6, 4, 4, 2, 2], '16': [0, 8, 7, 6, 6, 4, 4, 2, 2], '17': [0, 8, 7, 7, 6, 6, 4, 4, 2, 2], '18': [0, 8, 7, 7, 6, 6, 4, 4, 4, 2], '19': [0, 8, 8, 7, 7, 6, 6, 4, 4, 4], '20': [0, 8, 8, 7, 7, 6, 6, 4, 4, 4] } }";
            JObject jObject = Parse(jsonString);
            SpellsTable spellsTable;
            Assert.Throws<JsonException>(() => spellsTable = new SpellsTable(jObject));
        }

        [Test]
        public void TestMissingTable()
        {
            const string jsonString = "{ 'Name': 'CharlatanSpellsKnown', 'Guid': '69b34210916a46fc8dd031950aa5d9b7' }";
            JObject jObject = Parse(jsonString);
            SpellsTable spellsTable;
            Assert.Throws<JsonException>(() => spellsTable = new SpellsTable(jObject));
        }

        [Test]
        public void TestMissingLevel()
        {
            const string jsonString = "{ 'Name': 'CharlatanSpellsKnown', 'Guid': '69b34210916a46fc8dd031950aa5d9b7', 'Table': { '0': [0, 0], '1': [0, 6], '3': [0, 6, 2], '4': [0, 6, 2], '5': [0, 6, 2, 2], '6': [0, 6, 2, 2], '7': [0, 6, 4, 2, 2], '8': [0, 6, 4, 2, 2], '9': [0, 7, 4, 4, 2, 2], '10': [0, 7, 4, 4, 2, 2], '11': [0, 7, 6, 4, 4, 2, 2], '12': [0, 7, 6, 4, 4, 2, 2], '13': [0, 8, 6, 6, 4, 4, 2, 2], '14': [0, 8, 6, 6, 4, 4, 2, 2], '15': [0, 8, 7, 6, 6, 4, 4, 2, 2], '16': [0, 8, 7, 6, 6, 4, 4, 2, 2], '17': [0, 8, 7, 7, 6, 6, 4, 4, 2, 2], '18': [0, 8, 7, 7, 6, 6, 4, 4, 4, 2], '19': [0, 8, 8, 7, 7, 6, 6, 4, 4, 4], '20': [0, 8, 8, 7, 7, 6, 6, 4, 4, 4] } }";
            JObject jObject = Parse(jsonString);
            SpellsTable spellsTable;
            Assert.Throws<JsonException>(() => spellsTable = new SpellsTable(jObject));
        }
    }
}
