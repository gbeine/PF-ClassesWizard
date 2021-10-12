using System;
using System.Collections.Generic;

namespace PF_Core
{
    public class GuidStorage
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly GuidStorage __instance = new GuidStorage();

        private readonly Dictionary<string, string> guids_in_use = new Dictionary<string, string>();

        private GuidStorage() { }

        public static GuidStorage INSTANCE
        {
            get { return __instance;  }
        }

        public void addEntry(string name, string guid)
        {
            string original_guid;
            if (guids_in_use.TryGetValue(name, out original_guid))
            {
                if (original_guid != guid)
                {
                    String message = $"Asset: {name}, is already registered for object with another guid: {guid}";
                    _logger.Error(message);
                    throw new InvalidOperationException(message);
                }
            }
            else
            {
                guids_in_use.Add(name, guid);
            }
        }
    }
}
