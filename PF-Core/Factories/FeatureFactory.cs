using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using PF_Core.Extensions;
using PF_Core.Facades;

namespace PF_Core.Factories
{
    public class FeatureFactory
    {
        private static readonly Harmony.FastSetter blueprintFeature_set_AssetId = Harmony.CreateFieldSetter<BlueprintFeature>("m_AssetGuid");

        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;
        
        public BlueprintFeature createFeature(String name, String guid)
        {
            _logger.Debug($"Create feature {name} with id {guid}");

            BlueprintFeature feature = _library.Create<BlueprintFeature>();
            blueprintFeature_set_AssetId(feature, guid);
            feature.name = name;

            _library.Add(feature);

            _logger.Debug($"DONE: Create feature {name} with id {guid}");
            return feature;
        }

        public BlueprintFeature createFeatureFrom(String name, String guid, String fromAssetId)
        {
            _logger.Debug($"Create feature {name} with id {guid} based on {fromAssetId}");

            BlueprintFeature original = _library.GetFeature(fromAssetId);
            BlueprintFeature clone = UnityEngine.Object.Instantiate(original);
            blueprintFeature_set_AssetId(clone, guid);
            clone.name = name;

            _library.Add(clone);

            _logger.Debug($"DONE: Create feature {name} with id {guid} based on {fromAssetId}");
            return clone;
        }

        public BlueprintFeature createFeatureFrom(String name, String guid, String fromAssetId, String displayName, String description)
        {
            BlueprintFeature feature = createFeatureFrom(name, guid, fromAssetId);
            feature.SetNameDescription(displayName, description);
            return feature;
        }

        public AddProficiencies createAddWeaponProficiencies(params WeaponCategory[] weapons)
        {
            var addProficiencies = _library.Create<Kingmaker.UnitLogic.FactLogic.AddProficiencies>();
            addProficiencies.WeaponProficiencies = weapons;
            addProficiencies.ArmorProficiencies = new ArmorProficiencyGroup[0];
            return addProficiencies;
        }

        public AddFacts createAddFact(BlueprintUnitFact fact) =>
            createAddFact(fact.name, new BlueprintUnitFact[] {fact});
        
        public AddFacts createAddFact(String name, params BlueprintUnitFact[] facts)
        {
            AddFacts result = _library.Create<AddFacts>();
            result.name = $"AddFacts${name}";
            result.Facts = facts;
            return result;
        }
        
        public BlueprintFeature createCantrips(String name, String displayName, string description,
            UnityEngine.Sprite icon, string guid, BlueprintCharacterClass character_class,
            StatType stat, BlueprintAbility[] spells)
        {
            var learnSpells = _library.Create<LearnSpells>();
            learnSpells.CharacterClass = character_class;
            learnSpells.Spells = spells;

            var bind_spells = _library.Create<BindAbilitiesToClass>();
            bind_spells.Abilites = spells;
            bind_spells.Stat = stat;
            bind_spells.CharacterClass = character_class;
            bind_spells.Archetypes = Array.Empty<BlueprintArchetype>();
            bind_spells.AdditionalClasses = Array.Empty<BlueprintCharacterClass>();
            bind_spells.LevelStep = 1;
            bind_spells.Cantrip = true;

            var spls = _library.Create<AddFacts>();
            spls.Facts = spells;

            var feature = createFeature(name, guid);
            feature.SetNameDescriptionIcon(displayName, description, icon);
            feature.Groups = new FeatureGroup[] {FeatureGroup.None};
            feature.SetComponents(spls, learnSpells, bind_spells);

            return feature;
        }

        public BlueprintFeature createEmptyFeature()
        {
            return _library.Create<BlueprintFeature>();
        }
    }
}
