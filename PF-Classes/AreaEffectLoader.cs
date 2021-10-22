using Kingmaker.UnitLogic.Abilities.Blueprints;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations;

namespace PF_Classes
{
    public class AreaEffectLoader : Loader
    {
        private AreaEffect _AreaEffect;

        public AreaEffectLoader(string filename) : base(filename) { }

        public override bool load()
        {
            _logger.Debug("Parsing AreaEffect");
            _AreaEffect = Deserialize();
            _logger.Log($"DONE: Parsing AreaEffect {_AreaEffect.Guid}");
            return true;
        }

        public BlueprintAbilityAreaEffect AreaEffect
        {
            get { return AreaEffectFromJson.GetAreaEffect(_AreaEffect); }
        }

        private AreaEffect Deserialize()
        {
            return new AreaEffect(_jObject);
        }
    }
}
