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

        public BlueprintFeature CreateFeature(String name, String guid, String displayName, String description)
        {
            _logger.Debug($"Create feature {name} with id {guid}");

            BlueprintFeature feature = CreateFeature(name, guid);
            feature.SetNameDescription(displayName, description);

            _logger.Debug($"DONE: Create feature {name} with id {guid}");
            return feature;
        }

        public BlueprintFeature CreateFeature(String name, String guid, String displayName, String description, Sprite icon)
        {
            _logger.Debug($"Create feature {name} with id {guid}");

            BlueprintFeature feature = CreateFeature(name, guid);
            feature.SetNameDescriptionIcon(displayName, description, icon);

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

        public BlueprintFeature CreateFeatureFrom(String name, String guid, String fromAssetId, String displayName, String description)
        {
            _logger.Debug($"Create feature {name} with id {guid} based on {fromAssetId}");

            BlueprintFeature feature = CreateFeatureFrom(name, guid, fromAssetId);
            feature.SetNameDescription(displayName, description);

            _logger.Debug($"DONE: Create feature {name} with id {guid} based on {fromAssetId}");
            return feature;
        }

        public BlueprintFeature CreateFeatureFrom(String name, String guid, String fromAssetId, String displayName, String description, Sprite icon)
        {
            _logger.Debug($"Create feature {name} with id {guid} based on {fromAssetId}");

            BlueprintFeature feature = CreateFeatureFrom(name, guid, fromAssetId);
            feature.SetNameDescriptionIcon(displayName, description, icon);

            _logger.Debug($"DONE: Create feature {name} with id {guid} based on {fromAssetId}");
            return feature;
        }

        public AddProficiencies CreateAddWeaponProficiencies(IEnumerable<WeaponCategory> weapons) =>
            CreateAddWeaponArmorProficiencies(weapons, Array.Empty<ArmorProficiencyGroup>().ToList());

        public AddProficiencies CreateAddArmorProficiencies(IEnumerable<ArmorProficiencyGroup> armor) =>
            CreateAddWeaponArmorProficiencies(Array.Empty<WeaponCategory>().ToList(), armor);

        public AddProficiencies CreateAddWeaponArmorProficiencies(IEnumerable<WeaponCategory> weapons, IEnumerable<ArmorProficiencyGroup> armor)
        {
            _logger.Debug($"Create add weapon {weapons.LongCount()} and amor {armor.LongCount()} proficiencies");

            AddProficiencies addProficiencies = _library.Create<AddProficiencies>();
            addProficiencies.WeaponProficiencies = weapons.ToArray();
            addProficiencies.ArmorProficiencies = armor.ToArray();

            _logger.Debug("DONE: Create add weapon and amor proficiencies");
            return addProficiencies;
        }

        public AddFacts CreateAddFact(BlueprintUnitFact fact) =>
            CreateAddFact(fact.name, new BlueprintUnitFact[] {fact});

        public AddFacts CreateAddFact(String name, params BlueprintUnitFact[] facts)
        {
            _logger.Debug($"Create add facts {name}");

            AddFacts result = _library.Create<AddFacts>();
            result.name = $"AddFacts${name}";
            result.Facts = facts;

            _logger.Debug($"DONE:Create add facts {name}");
            return result;
        }

        public BlueprintFeature CreateEmptyFeature()
        {
            return _library.Create<BlueprintFeature>();
        }
    }
}
