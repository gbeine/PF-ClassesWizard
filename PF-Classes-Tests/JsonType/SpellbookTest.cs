using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PF_Classes.JsonTypes;

namespace PF_Classes.JsonType
{
    [TestFixture]
    public class SpellbookTest
    {
        [Test]
        public void TestSpellbook()
        {
            const string jsonString = "{ 'Guid': '0fc2fdfb15ec4abd888ef5a7b7e59003', 'Name': 'CharlatanSpellbook', 'CastingAttribute': 'Intelligence', SpellList: { 'Guid': '8b4fc86d687646648c551a740718118c', 'Name': 'CharlatanSpellList', 'SpellsByLevel': { '0': [ 'CURE_LIGHT_WOUNDS_CAST' ], '1': [ 'CURE_LIGHT_WOUNDS_CAST', 'SUMMON_MONSTER_I_SINGLE' ], '2': [ 'CURE_MODERATE_WOUNDS_CAST', 'SUMMON_MONSTER_II_BASE', 'MAGE_ARMOR', 'DELAY_POISON' ], '3': [ 'CURE_SERIOUS_WOUNDS_CAST', 'SUMMON_MONSTER_III_BASE', 'RESTORATION_LESSER' ], '4': [ 'CURE_CRITICAL_WOUNDS_CAST', 'SUMMON_MONSTER_IV_BASE', 'DELAY_POISON_COMMUNAL' ] } }, SpellsKnown: { 'Name': 'CharlatanSpellsKnown', 'Guid': '69b34210916a46fc8dd031950aa5d9b7', 'Table': { '0': [0, 0], '1': [0, 6], '2': [0, 6], '3': [0, 6, 2], '4': [0, 6, 2], '5': [0, 6, 2, 2], '6': [0, 6, 2, 2], '7': [0, 6, 4, 2, 2], '8': [0, 6, 4, 2, 2], '9': [0, 7, 4, 4, 2, 2], '10': [0, 7, 4, 4, 2, 2], '11': [0, 7, 6, 4, 4, 2, 2], '12': [0, 7, 6, 4, 4, 2, 2], '13': [0, 8, 6, 6, 4, 4, 2, 2], '14': [0, 8, 6, 6, 4, 4, 2, 2], '15': [0, 8, 7, 6, 6, 4, 4, 2, 2], '16': [0, 8, 7, 6, 6, 4, 4, 2, 2], '17': [0, 8, 7, 7, 6, 6, 4, 4, 2, 2], '18': [0, 8, 7, 7, 6, 6, 4, 4, 4, 2], '19': [0, 8, 8, 7, 7, 6, 6, 4, 4, 4], '20': [0, 8, 8, 7, 7, 6, 6, 4, 4, 4] } }, SpellsPerDay: { 'Name': 'CharlatanSpellsPerDay', 'Guid': '96b34210916a46fc8dd031950aa5d9b7', 'Table': { '0': [0, 0], '1': [0, 6], '2': [0, 6], '3': [0, 6, 2], '4': [0, 6, 2], '5': [0, 6, 2, 2], '6': [0, 6, 2, 2], '7': [0, 6, 4, 2, 2], '8': [0, 6, 4, 2, 2], '9': [0, 7, 4, 4, 2, 2], '10': [0, 7, 4, 4, 2, 2], '11': [0, 7, 6, 4, 4, 2, 2], '12': [0, 7, 6, 4, 4, 2, 2], '13': [0, 8, 6, 6, 4, 4, 2, 2], '14': [0, 8, 6, 6, 4, 4, 2, 2], '15': [0, 8, 7, 6, 6, 4, 4, 2, 2], '16': [0, 8, 7, 6, 6, 4, 4, 2, 2], '17': [0, 8, 7, 7, 6, 6, 4, 4, 2, 2], '18': [0, 8, 7, 7, 6, 6, 4, 4, 4, 2], '19': [0, 8, 8, 7, 7, 6, 6, 4, 4, 4], '20': [0, 8, 8, 7, 7, 6, 6, 4, 4, 4] } } }";
            JObject jObject = JObject.Parse(jsonString);
            Spellbook spellbook = new Spellbook(jObject);
            Assert.AreEqual("0fc2fdfb15ec4abd888ef5a7b7e59003",spellbook.Guid);
            Assert.AreEqual("CharlatanSpellbook",spellbook.Name);
            Assert.AreEqual("Intelligence",spellbook.CastingAttribute);
            Assert.IsFalse(spellbook.IsArcane);
            Assert.IsFalse(spellbook.IsSpontaneous);
            Assert.IsFalse(spellbook.CanCopyScrolls);
            Assert.IsFalse(spellbook.AllSpellsKnown);
            Assert.AreEqual("Cantrips",spellbook.Cantrips);
        }

        [Test]
        public void TestSpellbookOptionals()
        {
            const string jsonString = "{ 'IsArcane': true, 'IsSpontaneous': true, 'CanCopyScrolls': true, 'AllSpellsKnown': true, 'Cantrips': 'Something Else','Guid': '0fc2fdfb15ec4abd888ef5a7b7e59003', 'Name': 'CharlatanSpellbook', 'CastingAttribute': 'Intelligence', SpellList: { 'Guid': '8b4fc86d687646648c551a740718118c', 'Name': 'CharlatanSpellList', 'SpellsByLevel': { '0': [ 'CURE_LIGHT_WOUNDS_CAST' ], '1': [ 'CURE_LIGHT_WOUNDS_CAST', 'SUMMON_MONSTER_I_SINGLE' ], '2': [ 'CURE_MODERATE_WOUNDS_CAST', 'SUMMON_MONSTER_II_BASE', 'MAGE_ARMOR', 'DELAY_POISON' ], '3': [ 'CURE_SERIOUS_WOUNDS_CAST', 'SUMMON_MONSTER_III_BASE', 'RESTORATION_LESSER' ], '4': [ 'CURE_CRITICAL_WOUNDS_CAST', 'SUMMON_MONSTER_IV_BASE', 'DELAY_POISON_COMMUNAL' ] } }, SpellsKnown: { 'Name': 'CharlatanSpellsKnown', 'Guid': '69b34210916a46fc8dd031950aa5d9b7', 'Table': { '0': [0, 0], '1': [0, 6], '2': [0, 6], '3': [0, 6, 2], '4': [0, 6, 2], '5': [0, 6, 2, 2], '6': [0, 6, 2, 2], '7': [0, 6, 4, 2, 2], '8': [0, 6, 4, 2, 2], '9': [0, 7, 4, 4, 2, 2], '10': [0, 7, 4, 4, 2, 2], '11': [0, 7, 6, 4, 4, 2, 2], '12': [0, 7, 6, 4, 4, 2, 2], '13': [0, 8, 6, 6, 4, 4, 2, 2], '14': [0, 8, 6, 6, 4, 4, 2, 2], '15': [0, 8, 7, 6, 6, 4, 4, 2, 2], '16': [0, 8, 7, 6, 6, 4, 4, 2, 2], '17': [0, 8, 7, 7, 6, 6, 4, 4, 2, 2], '18': [0, 8, 7, 7, 6, 6, 4, 4, 4, 2], '19': [0, 8, 8, 7, 7, 6, 6, 4, 4, 4], '20': [0, 8, 8, 7, 7, 6, 6, 4, 4, 4] } }, SpellsPerDay: { 'Name': 'CharlatanSpellsPerDay', 'Guid': '96b34210916a46fc8dd031950aa5d9b7', 'Table': { '0': [0, 0], '1': [0, 6], '2': [0, 6], '3': [0, 6, 2], '4': [0, 6, 2], '5': [0, 6, 2, 2], '6': [0, 6, 2, 2], '7': [0, 6, 4, 2, 2], '8': [0, 6, 4, 2, 2], '9': [0, 7, 4, 4, 2, 2], '10': [0, 7, 4, 4, 2, 2], '11': [0, 7, 6, 4, 4, 2, 2], '12': [0, 7, 6, 4, 4, 2, 2], '13': [0, 8, 6, 6, 4, 4, 2, 2], '14': [0, 8, 6, 6, 4, 4, 2, 2], '15': [0, 8, 7, 6, 6, 4, 4, 2, 2], '16': [0, 8, 7, 6, 6, 4, 4, 2, 2], '17': [0, 8, 7, 7, 6, 6, 4, 4, 2, 2], '18': [0, 8, 7, 7, 6, 6, 4, 4, 4, 2], '19': [0, 8, 8, 7, 7, 6, 6, 4, 4, 4], '20': [0, 8, 8, 7, 7, 6, 6, 4, 4, 4] } } }";
            JObject jObject = JObject.Parse(jsonString);
            Spellbook spellbook = new Spellbook(jObject);
            Assert.AreEqual("0fc2fdfb15ec4abd888ef5a7b7e59003",spellbook.Guid);
            Assert.AreEqual("CharlatanSpellbook",spellbook.Name);
            Assert.AreEqual("Intelligence",spellbook.CastingAttribute);
            Assert.IsTrue(spellbook.IsArcane);
            Assert.IsTrue(spellbook.IsSpontaneous);
            Assert.IsTrue(spellbook.CanCopyScrolls);
            Assert.IsTrue(spellbook.AllSpellsKnown);
            Assert.AreEqual("Something Else",spellbook.Cantrips);
        }

        [Test]
        public void TestMissingGuid()
        {
            const string jsonString = "{ 'Name': 'CharlatanSpellbook', 'CastingAttribute': 'Intelligence' }";;
            JObject jObject = JObject.Parse(jsonString);
            Spellbook spellbook;
            Assert.Throws<JsonException>(() => spellbook = new Spellbook(jObject));
        }
    }
}
