using System;
using System.IO;
using System.Reflection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using PF_Core;
using PF_Core.Repositories;

namespace PF_Classes
{
    public class Loader
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly String m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private static bool loaded = false;

        public static void init()
        {
            if (loaded)
            {
                _logger.Log("Already loaded...");
            }
            else
            {
                try
                {
                    _logger.Log("Loading buffs...");
                    string[] buffFiles = Directory.GetFiles($"{m_exePath}/Buffs", "*.json");
                    foreach (var file in buffFiles)
                    {
                        _logger.Log($"Loading from file {file}");
                        BuffLoader buffLoader = new BuffLoader(file);
                        if (buffLoader.load())
                        {
                            BlueprintBuff buff = buffLoader.Buff;
                            BuffRepository.INSTANCE.RegisterBuff(buff);
                        }
                    }
                    _logger.Log("DONE: Loading buffs...");
                    _logger.Log("Loading classes...");
                    string[] classesFiles = Directory.GetFiles($"{m_exePath}/Classes", "*.json");
                    foreach (var file in classesFiles)
                    {
                        _logger.Log($"Loading from file {file}");
                        CharacterClassLoader characterClassLoader = new CharacterClassLoader(file);
                        if (characterClassLoader.load())
                        {
                            BlueprintCharacterClass characterClass = characterClassLoader.CharacterClass;
                            CharacterClassesRepository.INSTANCE.RegisterCharacterClass(characterClass);
                        }
                    }
                    _logger.Log("DONE: Loading classes...");
                }
                catch (Exception e)
                {
                    _logger.Error(e.Message);
                    _logger.Error(e.StackTrace);
                    throw;
                }
                _logger.Log("DONE: Loading really everything!");

                loaded = true;
            }
        }
    }
}
