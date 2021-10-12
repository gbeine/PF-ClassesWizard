using System;
using System.Reflection;
using PF_Core;
using UnityModManagerNet;

namespace PF_ClassesWizard
{
    static class Main
    {
        private static bool enabled;
        private static Harmony12.HarmonyInstance _harmony;
        private static Logger _logger = Logger.INSTANCE;
        
        static bool Load(UnityModManager.ModEntry modEntry)
        {
            _logger.init(modEntry);
            _logger.Log("Loading Classes Wizard");

            try
            {
                _harmony = Harmony12.HarmonyInstance.Create(modEntry.Info.Id);
                _harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                throw;
            }

            modEntry.OnToggle = OnToggle;

            return true;
        }
        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            _logger.Debug("Toggeled!");
            enabled = value;
            return true;
        }
    }
}
