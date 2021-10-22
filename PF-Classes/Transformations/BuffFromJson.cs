using Kingmaker.UnitLogic.Buffs.Blueprints;
using PF_Classes.JsonTypes;
using PF_Core.Extensions;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class BuffFromJson : JsonTransformation
    {
        private static readonly BuffFactory _buffFactory = new BuffFactory();

        public static BlueprintBuff GetBuff(Buff buffData)
        {
            _logger.Log($"Creating buff from JSON data {buffData.Name}");

            BlueprintBuff buff = _buffFactory.CreateBuff(buffData.Name, buffData.Guid,
                buffData.DisplayName, buffData.Description, SpriteLookup.lookupFor(buffData.Icon));

            if (!string.Empty.Equals(buffData.Stacking))
                buff.Stacking = EnumParser.parseStackingType(buffData.Stacking);

            if (!string.Empty.Equals(buffData.Flags))
                buff.SetFlags(EnumParser.parseBuffFlags(buffData.Flags));

            foreach (var component in buffData.Components)
            {
                _logger.Debug($"Adding component {component.Type}");
                ComponentFromJson.AddComponent(buff, component);
                _logger.Debug($"DONE: Adding component {component.Type}");
            }

            _logger.Log($"DONE: Creating buff from JSON data {buffData.Name}");
            _identifierRegistry.Register(buff);
            return buff;
        }
    }
}
