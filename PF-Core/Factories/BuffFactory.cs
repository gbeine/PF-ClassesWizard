using System;
using Kingmaker.Blueprints;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using PF_Core.Extensions;
using PF_Core.Facades;
using UnityEngine;

namespace PF_Core.Factories
{
    public class BuffFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        public BlueprintBuff CreateBuff(String name, String guid, String displayName, String description, Sprite icon,
            params BlueprintComponent[] components) =>
            CreateBuff(name, guid, displayName, description, icon, new PrefabLink(), new PrefabLink(), components);

        public BlueprintBuff CreateBuff(String name, String guid, String displayName, String description, Sprite icon,
            PrefabLink fxOnStart, PrefabLink fxOnRemove, params BlueprintComponent[] components)
        {
            _logger.Debug($"Create buff {name} with id {guid}");
            BlueprintBuff buff = _library.Create<BlueprintBuff>();
            buff.SetAssetId(guid);
            buff.name = name;
            buff.SetNameDescriptionIcon(displayName,description,icon);
            buff.SetComponents(components);
            buff.FxOnStart = fxOnStart;
            buff.FxOnRemove = fxOnRemove;

            _library.Add(buff);

            _logger.Debug($"DONE: Create buff {name} with id {guid}");
            return buff;
        }
    }
}
