using System;
using Kingmaker.Blueprints.Classes;
using PF_Core;
using PF_Core.Repositories;

namespace PF_Classes
{
    public class Loader
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static bool loaded = false;

        public static void init()
        {
            if (loaded)
            {
                _logger.Log("Already loaded...");
            }
            else
            {
                _logger.Log("Loading classes...");
                try
                {
                    CharacterClassLoader characterClassLoader = new CharacterClassLoader("Charlatan.json");
                    if (characterClassLoader.load())
                    {
                        BlueprintCharacterClass characterClass = characterClassLoader.CharacterClass;
                        CharacterClassesRepository.INSTANCE.RegisterCharacterClass(characterClass);
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e.Message);
                    _logger.Error(e.StackTrace);
                    throw;
                }
                _logger.Log("DONE: Loading classes...");

                loaded = true;
            }
        }
    }
}
