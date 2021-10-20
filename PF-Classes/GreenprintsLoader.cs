using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Epic.OnlineServices.Stats;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using PF_Core;
using PF_Core.Repositories;

namespace PF_Classes
{
    public class GreenprintsLoader
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly Dictionary<String, Action<String>> _loader = new Dictionary<string, Action<string>>();
        private static readonly String m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static readonly String path = $"{m_exePath}/Greenprints";
        private static readonly Regex regex = new Regex(".*(Buff|Class|Feature|Progression|Selection|SpellAbility).json");

        private static bool loaded = false;

        public static void init()
        {
            if (loaded)
            {
                _logger.Log("Already loaded...");
            }
            else
            {
                if (Directory.Exists(path))
                {
                    _logger.Log($"Loading from {path}");
                    IOrderedEnumerable<string> files = Directory.GetFiles(path, "*.json").OrderBy(f => f);
                    foreach (var file in files)
                    {
                        Match match = regex.Match(file);
                        if (match.Success)
                        {
                            try
                            {
                                _logger.Log("------------------------------------------------------------------------------");
                                _logger.Log($"Loading from file {file}");

                                String type = match.Groups[1].Value;

                                _loader[type](file);

                                _logger.Log($"DONE: Loading from file {file}");
                                _logger.Log("------------------------------------------------------------------------------");
                            }
                            catch (Exception e)
                            {
                                _logger.Error(e.Message);
                                _logger.Error(e.StackTrace);
                            }
                        }
                    }
                }
                _logger.Log("DONE: Loading really everything!");

                loaded = true;
            }
        }

        static GreenprintsLoader()
        {
            _loader.Add("Buff", file =>
            {
                BuffLoader buffLoader = new BuffLoader(file);
                if (buffLoader.load())
                {
                    BlueprintBuff buff = buffLoader.Buff;
                }
            });
            _loader.Add("Class", file =>
            {
                CharacterClassLoader characterClassLoader = new CharacterClassLoader(file);
                if (characterClassLoader.load())
                {
                    BlueprintCharacterClass characterClass = characterClassLoader.CharacterClass;
                    CharacterClassesRepository.INSTANCE.RegisterCharacterClass(characterClass);
                }
            });
            _loader.Add("Feature", file =>
            {
                FeatureLoader featureLoader = new FeatureLoader(file);
                if (featureLoader.load())
                {
                    BlueprintFeature feature = featureLoader.Feature;
                }
            });
            _loader.Add("Progression", file =>
            {
                ProgressionLoader progressionLoader = new ProgressionLoader(file);
                if (progressionLoader.load())
                {
                    BlueprintProgression progression = progressionLoader.Progression;
                }
            });
            _loader.Add("Selection", file =>
            {
                SelectionLoader selectionLoader = new SelectionLoader(file);
                if (selectionLoader.load())
                {
                    BlueprintFeatureSelection selection = selectionLoader.Selection;
                }
            });
            _loader.Add("SpellAbility", file =>
            {
                SpellLoader spellLoader = new SpellLoader(file);
                if (spellLoader.load())
                {
                    BlueprintAbility spell = spellLoader.Spell;
                }
            });
        }
    }
}
