using System;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Weapons;
using PF_Core.Facades;

namespace PF_Core.Repositories
{
    public class ItemRepository
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        private static readonly ItemRepository __instance = new ItemRepository();

        private ItemRepository() { }

        public static ItemRepository INSTANCE
        {
            get { return __instance;  }
        }

        public BlueprintItemWeapon GetWeapon(String assetId)
        {
            _logger.Debug($"Search for Weapon {assetId}");
            return _library.Get<BlueprintItemWeapon>(assetId);
        }
        public BlueprintItem GetItem(String assetId)
        {
            _logger.Debug($"Search for Item {assetId}");
            return _library.GetItem(assetId);
        }
    }
}
