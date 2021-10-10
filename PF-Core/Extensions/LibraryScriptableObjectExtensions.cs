using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints;

namespace PF_Core.Extensions
{
    public static class LibraryScriptableObjectExtensions
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly GuidStorage _guidStorage = GuidStorage.INSTANCE;

        public static T Get<T>(this LibraryScriptableObject library, String assetId) where T : BlueprintScriptableObject
        {
            _logger.Debug($"Get {typeof(T)} with id {assetId}");
            return (T)library.BlueprintsByAssetId[assetId];
        }

        public static bool Exists<T>(this LibraryScriptableObject library, String assetId) where T : BlueprintScriptableObject
        {
            _logger.Debug($"Exists id {assetId} of {typeof(T)}");
            return library.BlueprintsByAssetId.ContainsKey(assetId)
                   && library.BlueprintsByAssetId[assetId].GetType() == typeof(T);
        }

        public static List<T> GetAll<T>(this LibraryScriptableObject library) where T : BlueprintScriptableObject
        {
            _logger.Debug($"Get all {typeof(T)}");
            return library.GetAllBlueprints()
                .Where(
                    bso => bso.GetType().Equals(typeof(T)))
                .Select(
                    bso => (T) bso)
                .ToList();
        }
        
        public static void AddAsset(this LibraryScriptableObject library, BlueprintScriptableObject blueprint)
        {
            _logger.Debug($"Adding {blueprint} with id {blueprint.AssetGuid}");

            BlueprintScriptableObject existing;
            if (library.BlueprintsByAssetId.TryGetValue(blueprint.AssetGuid, out existing))
            {
                String message =
                    $"Duplicate AssetId for {blueprint.name}, existing entry ID: {blueprint.AssetGuid}, name: {existing.name}, type: {existing.GetType().Name}";
                _logger.Error(message);
                return;
            }
            else if (blueprint.AssetGuid == "")
            {
                String message = $"Missing AssetId: {blueprint.AssetGuid}, name: {existing.name}, type: {existing.GetType().Name}";
                _logger.Error(message);
                return;
            }

            library.GetAllBlueprints().Add(blueprint);
            library.BlueprintsByAssetId[blueprint.AssetGuid] = blueprint;
            
            _guidStorage.addEntry(blueprint.name, blueprint.AssetGuid);

            _logger.Debug($"DONE: Adding {blueprint} with id {blueprint.AssetGuid}");
        }

    }
}
