using System;
using Kingmaker.Blueprints;
using PF_Classes;
using PF_Core;
using PF_Core.Facades;
using PF_Core.Repositories;

namespace PF_ClassesWizard.Patches
{
    [Harmony12.HarmonyPatch(typeof(LibraryScriptableObject), "LoadDictionary")]
    [Harmony12.HarmonyPatch(typeof(LibraryScriptableObject), "LoadDictionary", new Type[0])]
    static class LibraryScriptableObject_LoadDictionary_Patch
    {
        private static Logger _logger = Logger.INSTANCE;

        static void Postfix(LibraryScriptableObject __instance)
        {
            Library library = new Library(__instance);
            Loader.init();
        }
    }
}
