using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using PF_Core.Extensions;
using PF_Core.Facades;
using UnityEngine;

namespace PF_Core.Factories
{
    public class FeatureFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        public BlueprintFeature CreateFeature(String name, String guid)
        {
            _logger.Debug($"Create feature {name} with id {guid}");

            BlueprintFeature feature = _library.Create<BlueprintFeature>();
            feature.SetAssetId(guid);
            feature.name = name;

            _library.Add(feature);

            _logger.Debug($"DONE: Create feature {name} with id {guid}");
            return feature;
        }

        public BlueprintFeature CreateFeatureFrom(String name, String guid, String fromAssetId)
        {
            _logger.Debug($"Create feature {name} with id {guid} based on {fromAssetId}");

            BlueprintFeature original = _library.GetFeature(fromAssetId);
            BlueprintFeature feature = UnityEngine.Object.Instantiate(original);
            feature.SetAssetId(guid);
            feature.name = name;

            _library.Add(feature);

            _logger.Debug($"DONE: Create feature {name} with id {guid} based on {fromAssetId}");
            return feature;
        }

        public BlueprintFeature CreateEmptyFeature()
        {
            return _library.Create<BlueprintFeature>();
        }
    }
}
