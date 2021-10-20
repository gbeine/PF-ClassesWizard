using System;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using PF_Core.Extensions;
using PF_Core.Facades;
using UnityEngine;

namespace PF_Core.Factories
{
    public class ProgressionFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        public BlueprintProgression CreateProgressionFrom(String name, String guid, String fromAssetId)
        {
            _logger.Debug($"Create progression {name} with id {guid} based on {fromAssetId}");

            BlueprintProgression original = _library.GetProgression(fromAssetId);
            BlueprintProgression progression = UnityEngine.Object.Instantiate(original);
            progression.SetAssetId(guid);
            progression.name = name;

            _library.Add(progression);

            _logger.Debug($"DONE: Create progression {name} with id {guid} based on {fromAssetId}");
            return progression;

        }

        public BlueprintProgression CreateProgression(String name, String guid, String displayName, String description,
            Sprite icon, params BlueprintComponent[] components)
        {
            _logger.Debug($"Create progession {name} with id {guid}");

            BlueprintProgression progression = _library.Create<BlueprintProgression>();
            progression.SetAssetId(guid);
            progression.name = name;
            progression.SetNameDescriptionIcon(displayName, description, icon);
            progression.SetComponents(components);

            _library.Add(progression);

            _logger.Debug($"DONE: Create progession {name} with id {guid}");
            return progression;
        }

        public BlueprintProgression CreateEmptyProgression()
        {
            _logger.Debug("Create emptu=y progession");

            return _library.Create<BlueprintProgression>();
        }
    }
}
