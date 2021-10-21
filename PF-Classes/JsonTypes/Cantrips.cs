using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Cantrips : Feature
    {
        public Cantrips(JObject jObject) : base(jObject)
        {
            Spellbook = SelectString(jObject, "Spellbook");
        }

        public string Spellbook { get; }
    }
}
