using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Proficiencies : Feature
    {
        public Proficiencies(JObject jObject) : base(jObject) { }
    }
}
