using System;
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

namespace PF_Core.Factories
{
    public class FeatureFactory
    {
        private static readonly Harmony.FastSetter blueprintFeature_set_AssetId = Harmony.CreateFieldSetter<BlueprintFeature>("m_AssetGuid");

        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        public BlueprintFeature CreateFeature(String name, String guid)
        {
            _logger.Debug($"Create feature {name} with id {guid}");

            BlueprintFeature feature = _library.Create<BlueprintFeature>();
            blueprintFeature_set_AssetId(feature, guid);
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
            blueprintFeature_set_AssetId(feature, guid);
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

        public AddProficiencies CreateAddWeaponProficiencies(params WeaponCategory[] weapons)
        {
            _logger.Debug($"Create add weapon proficiencies {weapons.Length}");

            AddProficiencies addProficiencies = _library.Create<Kingmaker.UnitLogic.FactLogic.AddProficiencies>();
            addProficiencies.WeaponProficiencies = weapons;
            addProficiencies.ArmorProficiencies = new ArmorProficiencyGroup[0];

            _logger.Debug($"DONE: Create add weapon proficiencies {weapons.Length}");
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

        public BlueprintFeature CreateCantrips(String name, String guid, String displayName, String description,
            UnityEngine.Sprite icon, BlueprintCharacterClass characterClass, BlueprintSpellbook spellbook) =>
            CreateCantrips(name, guid, displayName, description, icon, characterClass,
                spellbook.CastingAttribute, spellbook.SpellList.SpellsByLevel[0].Spells.ToArray());

        public BlueprintFeature CreateCantrips(String name, String guid, String displayName, String description,
            UnityEngine.Sprite icon, BlueprintCharacterClass characterClass,
            StatType stat, BlueprintAbility[] spellList)
        {
            _logger.Debug($"Create cantrips {name} with id {guid}");

            LearnSpells learnSpells = _library.Create<LearnSpells>();
            learnSpells.CharacterClass = characterClass;
            learnSpells.Spells = spellList;

            BindAbilitiesToClass bind_spells = _library.Create<BindAbilitiesToClass>();
            bind_spells.Abilites = spellList;
            bind_spells.Stat = stat;
            bind_spells.CharacterClass = characterClass;
            bind_spells.Archetypes = Array.Empty<BlueprintArchetype>();
            bind_spells.AdditionalClasses = Array.Empty<BlueprintCharacterClass>();
            bind_spells.LevelStep = 1;
            bind_spells.Cantrip = true;

            AddFacts spells = _library.Create<AddFacts>();
            spells.Facts = spellList;

            BlueprintFeature cantrips = CreateFeature(name, guid);
            cantrips.SetNameDescriptionIcon(displayName, description, icon);
            cantrips.Groups = new FeatureGroup[] {FeatureGroup.None};
            cantrips.SetComponents(spells, learnSpells, bind_spells);

            _logger.Debug($"DONE: Create cantrips {name} with id {guid}");
            return cantrips;
        }

        public BlueprintFeature CreateEmptyFeature()
        {
            return _library.Create<BlueprintFeature>();
        }
    }
}
