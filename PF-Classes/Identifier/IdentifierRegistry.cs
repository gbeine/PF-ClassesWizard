using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using PF_Core;

namespace PF_Classes.Identifier
{
    public class IdentifierRegistry
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        internal static readonly IdentifierRegistry INSTANCE = new IdentifierRegistry();

        private static readonly Dictionary<String, String> nameToGuidMap = new Dictionary<String, String>();
        private static readonly Dictionary<String, String> guidToNameMap = new Dictionary<String, String>();

        private IdentifierRegistry() { }

        internal void Register(BlueprintScriptableObject blueprintScriptableObject) =>
            Register(blueprintScriptableObject.name, blueprintScriptableObject.AssetGuid);

        internal bool GuidExists(String assetId)
        {
            return guidToNameMap.ContainsKey(assetId);
        }

        internal String NameForGuid(String assetId)
        {
            return guidToNameMap[assetId];
        }

        internal bool NameExists(String name)
        {
            return nameToGuidMap.ContainsKey(name);
        }

        internal String GuidForName(String name)
        {
            return nameToGuidMap[name];
        }

        private void Register(String name, String assetId)
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
        }
    }
}
