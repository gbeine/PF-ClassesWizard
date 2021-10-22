using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using PF_Core;

namespace PF_Classes.Identifier
{
    public class IdentifierRegistry
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly Dictionary<string, string> nameToGuidMap = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> guidToNameMap = new Dictionary<string, string>();
        private static readonly Dictionary<string, Type> nameToType = new Dictionary<string, Type>();

        private static readonly IdentifierRegistry __instance = new IdentifierRegistry();

        private IdentifierRegistry() { }

        public static IdentifierRegistry INSTANCE
        {
            get { return __instance;  }
        }

        internal void Register(BlueprintScriptableObject blueprintScriptableObject) =>
            Register(blueprintScriptableObject.name, blueprintScriptableObject.AssetGuid, blueprintScriptableObject.GetType());

        internal bool GuidExists(string assetId)
        {
            return guidToNameMap.ContainsKey(assetId);
        }

        internal string NameForGuid(string assetId)
        {
            return guidToNameMap[assetId];
        }

        internal bool NameExists(string name)
        {
            return nameToGuidMap.ContainsKey(name);
        }

        internal bool ExistsAndIsA(string name, Type type)
        {
            return nameToType.ContainsKey(name) && nameToType[name] == type;
        }

        internal string GuidForName(string name)
        {
            return nameToGuidMap[name];
        }

        private void Register(string name, string assetId, Type type)
        {
            if (GuidExists(assetId))
            {
                _logger.Warning($"GUID exists {assetId}, not adding {name}");
                return;
            }
            if (NameExists(name))
            {
                _logger.Warning($"Name exists {name}, not adding {assetId}");
                return;
            }
            nameToGuidMap[name] = assetId;
            guidToNameMap[assetId] = name;
            nameToType[name] = type;
        }
    }
}
