using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PF_Classes.JsonTypes;

namespace PF_Classes.JsonType
{
    [TestFixture]
    public class CantripsTest
    {
        [Test]
        public void TestCantrips()
        {
            const string jsonString = "{ 'Guid': '2464f9cfc5a34b608b5a9edb9c5f6e7b', 'Name': 'CharlatanCantrips', 'DisplayName': 'Cantrips', 'Description': 'Charlatans have cantrips ;-)', 'Icon': 'ref:ARCANE_SCHOOL_ILLUSION_BLINDING_RAY' }";
            JObject jObject = JObject.Parse(jsonString);
            Cantrips cantrips = new Cantrips(jObject);
            Assert.AreEqual("2464f9cfc5a34b608b5a9edb9c5f6e7b",cantrips.Guid);
            Assert.AreEqual("CharlatanCantrips",cantrips.Name);
            Assert.AreEqual("Cantrips",cantrips.DisplayName);
            Assert.AreEqual("Charlatans have cantrips ;-)",cantrips.Description);
            Assert.AreEqual("ref:ARCANE_SCHOOL_ILLUSION_BLINDING_RAY",cantrips.Icon);
        }

        [Test]
        public void TestMissingGuid()
        {
            const string jsonString = "{ 'Name': 'CharlatanCantrips', 'DisplayName': 'Cantrips', 'Description': 'Charlatans have cantrips ;-)', 'Icon': 'ref:ARCANE_SCHOOL_ILLUSION_BLINDING_RAY' }";
            JObject jObject = JObject.Parse(jsonString);
            Cantrips cantrips;
            Assert.Throws<JsonException>(() => cantrips = new Cantrips(jObject));
        }

        [Test]
        public void TestDefaultDescription()
        {
            const string jsonString = "{ 'Guid': '2464f9cfc5a34b608b5a9edb9c5f6e7b', 'Name': 'CharlatanCantrips', 'DisplayName': 'Cantrips', 'Icon': 'ref:ARCANE_SCHOOL_ILLUSION_BLINDING_RAY' }";
            JObject jObject = JObject.Parse(jsonString);
            Cantrips cantrips = new Cantrips(jObject);
            Assert.AreEqual("2464f9cfc5a34b608b5a9edb9c5f6e7b",cantrips.Guid);
            Assert.AreEqual("CharlatanCantrips",cantrips.Name);
            Assert.AreEqual("Cantrips",cantrips.DisplayName);
            Assert.AreEqual("Cantrips",cantrips.Description);
            Assert.AreEqual("ref:ARCANE_SCHOOL_ILLUSION_BLINDING_RAY",cantrips.Icon);
        }
    }
}
