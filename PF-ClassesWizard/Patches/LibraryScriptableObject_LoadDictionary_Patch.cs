using System;
using Kingmaker.Blueprints;
using PF_Classes;
using PF_Core;
using PF_Core.Facades;

namespace PF_ClassesWizard.Patches
{
    [Harmony12.HarmonyPatch(typeof(LibraryScriptableObject), "LoadDictionary")]
    [Harmony12.HarmonyPatch(typeof(LibraryScriptableObject), "LoadDictionary", new Type[0])]
    static class LibraryScriptableObject_LoadDictionary_Patch
    {
        private static Logger _logger = Logger.INSTANCE;

        static void Postfix(LibraryScriptableObject __instance)
        {
            _logger.Log("Patching library");

            Dumper.Dump(__instance);

            Library library = new Library(__instance);
            GreenprintsLoader.init();

            _logger.Log("DONE: Patching library");
        }
    }
}
