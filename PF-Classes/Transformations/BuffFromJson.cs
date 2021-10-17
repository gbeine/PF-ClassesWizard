using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core;
using PF_Core.Extensions;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class BuffFromJson
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly BuffFactory _buffFactory = new BuffFactory();

        public static BlueprintBuff GetBuff(Buff buffData)
        {
            _logger.Log($"Creating component from JSON data {buffData.Name}");

            BlueprintBuff buff = _buffFactory.CreateBuff(buffData.Name, buffData.Guid,
                buffData.DisplayName, buffData.Description, SpriteLookup.lookupFor(buffData.Icon));

            List<BlueprintComponent> components = new List<BlueprintComponent>();
            foreach (var component in buffData.Components)
            {
                components.Add(ComponentFromJson.GetComponent(component));
            }

            buff.SetComponents(components.ToArray());

            _logger.Log($"DONE: Creating component from JSON data {buffData.Name}");
            IdentifierRegistry.INSTANCE.Register(buff);
            return buff;
        }
    }
}
