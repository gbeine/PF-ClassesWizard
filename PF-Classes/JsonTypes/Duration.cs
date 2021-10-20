using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Duration : JsonWrap
    {
        public Duration(JObject jObject) : base(jObject)
        {
            BonusValue = SelectString(jObject, "BonusValue", "Default");
            Rate = SelectString(jObject, "Rate", "Rounds");
            DiceType = SelectString(jObject, "DiceType", "Zero");
            DiceCountValue = SelectInt(jObject, "DiceCountValue", 0);
        }

        public string BonusValue { get; }
        public string Rate { get; }
        public string DiceType { get; }
        public int DiceCountValue { get; }
    }
}
