using Kingmaker.UnitLogic.Buffs.Blueprints;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations;

namespace PF_Classes
{
    public class BuffLoader : Loader
    {
        private Buff _buff;

        public BuffLoader(string filename) : base(filename) { }

        public override bool load()
        {
            _logger.Debug("Parsing buff");
            _buff = Deserialize();
            _logger.Log($"DONE: Parsing buff {_buff.Guid}");
            return true;
        }

        public BlueprintBuff Buff
        {
            get { return BuffFromJson.GetBuff(_buff); }
        }

        private Buff Deserialize()
        {
            return new Buff(_jObject);
        }
    }
}
