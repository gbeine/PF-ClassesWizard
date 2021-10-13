using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PF_Classes.JsonTypes;

namespace PF_Classes.JsonType
{
    [TestFixture]
    public class ProgessionTest
    {
        [Test]
        public void TestProgession()
        {
            const string jsonString = "{ 'Guid': '3106acb568bb47a0b3d11adc6c378c14', 'Name': 'CharlatanProgression', 'LevelEntries': { '1': [ 'INQUISITOR_TACTICAL_LEADER_DIPLOMACY', 'BARD_BARDIC_KNOWLEDGE' ], '2': [ 'COMMON_EVASION', 'WIZARD_FEAT_SELECTION' ], '20': [ 'ROGUE_TALENT_SELECTION' ] } }";
            JObject jObject = JObject.Parse(jsonString);
            Progression progression = new Progression(jObject);
            Assert.AreEqual("3106acb568bb47a0b3d11adc6c378c14",progression.Guid);
            Assert.AreEqual("CharlatanProgression",progression.Name);
            Assert.NotNull(progression.LevelEntries);
            Assert.AreEqual(20, progression.LevelEntries.Count);
            Assert.AreEqual(2, progression.LevelEntries[0].Count);
            Assert.AreEqual(2, progression.LevelEntries[1].Count);
            Assert.AreEqual(0, progression.LevelEntries[2].Count);
            Assert.AreEqual(0, progression.LevelEntries[5].Count);
            Assert.AreEqual(1, progression.LevelEntries[19].Count);
            Assert.AreEqual(0, progression.UiDeterminatorsGroup.Count);
            Assert.AreEqual(0, progression.UiGroups.Count);
        }

        [Test]
        public void TestMissingGuid()
        {
            const string jsonString = "{ 'Name': 'CharlatanProgression', 'LevelEntries': {} }";
            JObject jObject = JObject.Parse(jsonString);
            Progression progression;
            Assert.Throws<JsonException>(() => progression = new Progression(jObject));
        }

        [Test]
        public void TestEmptyProgession()
        {
            const string jsonString = "{ 'Guid': '3106acb568bb47a0b3d11adc6c378c14', 'Name': 'CharlatanProgression', 'LevelEntries': { } }";
            JObject jObject = JObject.Parse(jsonString);
            Progression progression = new Progression(jObject);
            Assert.AreEqual("3106acb568bb47a0b3d11adc6c378c14",progression.Guid);
            Assert.AreEqual("CharlatanProgression",progression.Name);
            Assert.NotNull(progression.LevelEntries);
            Assert.AreEqual(20, progression.LevelEntries.Count);
            Assert.AreEqual(0, progression.LevelEntries[0].Count);
            Assert.AreEqual(0, progression.LevelEntries[5].Count);
            Assert.AreEqual(0, progression.LevelEntries[19].Count);
        }

        [Test]
        public void TestProgessionWithUiDetermination()
        {
            const string jsonString = "{ 'Guid': '3106acb568bb47a0b3d11adc6c378c14', 'Name': 'CharlatanProgression', 'LevelEntries': { '1': [ 'INQUISITOR_TACTICAL_LEADER_DIPLOMACY', 'BARD_BARDIC_KNOWLEDGE' ], '2': [ 'COMMON_EVASION', 'WIZARD_FEAT_SELECTION' ], '20': [ 'ROGUE_TALENT_SELECTION' ] }, 'UiDeterminatorsGroup': [ 'ROGUE_TRAPFINDING', 'ROGUE_WEAPON_FINESSE', 'COMMON_DETECT_MAGIC' ] }";
            JObject jObject = JObject.Parse(jsonString);
            Progression progression = new Progression(jObject);
            Assert.AreEqual("3106acb568bb47a0b3d11adc6c378c14",progression.Guid);
            Assert.AreEqual("CharlatanProgression",progression.Name);
            Assert.AreEqual(3, progression.UiDeterminatorsGroup.Count);
            Assert.IsTrue(progression.UiDeterminatorsGroup.Contains("ROGUE_TRAPFINDING"));
        }

        [Test]
        public void TestProgessionWithUiGroups()
        {
            const string jsonString = "{ 'Guid': '3106acb568bb47a0b3d11adc6c378c14', 'Name': 'CharlatanProgression', 'LevelEntries': { '1': [ 'INQUISITOR_TACTICAL_LEADER_DIPLOMACY', 'BARD_BARDIC_KNOWLEDGE' ], '2': [ 'COMMON_EVASION', 'WIZARD_FEAT_SELECTION' ], '20': [ 'ROGUE_TALENT_SELECTION' ] }, 'UiGroups': [ [ 'ROGUE_TALENT_SELECTION' ], [ 'WIZARD_FEAT_SELECTION', 'ARCANE_SCHOOL_ILLUSION_INVISIBILITY_FIELD' ] ] }";
            JObject jObject = JObject.Parse(jsonString);
            Progression progression = new Progression(jObject);
            Assert.AreEqual("3106acb568bb47a0b3d11adc6c378c14",progression.Guid);
            Assert.AreEqual("CharlatanProgression",progression.Name);
            Assert.AreEqual(2, progression.UiGroups.Count);
            Assert.AreEqual(1, progression.UiGroups[0].Count);
            Assert.AreEqual(2, progression.UiGroups[1].Count);
            Assert.IsTrue(progression.UiGroups[1].Contains("WIZARD_FEAT_SELECTION"));
        }
    }
}
