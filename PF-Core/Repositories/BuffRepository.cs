using System;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using PF_Core.Facades;

namespace PF_Core.Repositories
{
    public class BuffRepository
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        private static readonly BuffRepository __instance = new BuffRepository();

        private BuffRepository() { }

        public static BuffRepository INSTANCE
        {
            get { return __instance;  }
        }
        public BlueprintBuff GetBuff(String assetId)
        {
            _logger.Debug($"Search for Buff {assetId}");
            return _library.GetBuff(assetId);
        }

        public void RegisterBuff(BlueprintBuff blueprintBuff)
        {
            _logger.Debug($"Add buff {blueprintBuff.Name}");
            // nothing to do here yet
            _logger.Debug($"DONE: Add buff {blueprintBuff.Name}");
        }
    }
}
