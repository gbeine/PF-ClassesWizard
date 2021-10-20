using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class AreaEffect : JsonType
    {
        public AreaEffect(JObject jObject) : base(jObject)
        {
            From = SelectString(jObject, "From");
            BaseValueType = SelectString(jObject, "BaseValueType");
        }

        public string From { get; }
        public string BaseValueType { get; }
    }
}
