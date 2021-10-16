using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using PF_Core.Extensions;
using PF_Core.Facades;

namespace PF_Core.Factories
{
    public class CantripsFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly Library _library = Library.INSTANCE;

        private static readonly FeatureFactory _featureFactory = new FeatureFactory();

        public BlueprintFeature CreateCantripsFrom(String name, String guid, String fromAssetId,
            String displayName, String description, UnityEngine.Sprite icon, BlueprintCharacterClass characterClass, BlueprintSpellbook spellbook) =>
            CreateCantripsFrom(name, guid, fromAssetId, displayName, description, icon, characterClass,
                spellbook.CastingAttribute, spellbook.SpellList.SpellsByLevel[0].Spells.ToArray());

        public BlueprintFeature CreateCantrips(String name, String guid, String displayName, String description,
            UnityEngine.Sprite icon, BlueprintCharacterClass characterClass, BlueprintSpellbook spellbook) =>
            CreateCantrips(name, guid, displayName, description, icon, characterClass,
                spellbook.CastingAttribute, spellbook.SpellList.SpellsByLevel[0].Spells.ToArray());

        public BlueprintFeature CreateCantripsFrom(String name, String guid, String fromAssetId,
            String displayName, String description, UnityEngine.Sprite icon, BlueprintCharacterClass characterClass,
            StatType stat, BlueprintAbility[] spellList)
        {
            _logger.Debug($"Create cantrips {name} with id {guid} based on {fromAssetId}");

            BlueprintFeature cantrips = CreateCantrips(
                _featureFactory.CreateFeatureFrom(name, guid, fromAssetId, displayName, description, icon),
                characterClass, stat, spellList);

            _logger.Debug($"Binding cantrips {name} to class {characterClass.name}");

            cantrips.ReplaceComponent<BindAbilitiesToClass>(c =>
            {
                c.CharacterClass = characterClass;
                c.Stat = stat;
            });

            _logger.Debug($"DONE: Create cantrips {name} with id {guid} based on {fromAssetId}");
            return cantrips;
        }

        public BlueprintFeature CreateCantrips(String name, String guid, String displayName, String description,
            UnityEngine.Sprite icon, BlueprintCharacterClass characterClass,
            StatType stat, BlueprintAbility[] spellList)
        {
            _logger.Debug($"Create cantrips {name} with id {guid}");

            BlueprintFeature cantrips = CreateCantrips(
                _featureFactory.CreateFeature(name, guid, displayName, description, icon),
                characterClass, stat, spellList);

            _logger.Debug($"Binding cantrips {name} to class {characterClass.name}");
            BindAbilitiesToClass bind = new BindAbilitiesToClass();
            bind.CharacterClass = characterClass;
            bind.Stat = stat;

            cantrips.SetComponents(bind);

            _logger.Debug($"DONE: Create cantrips {name} with id {guid}");
            return cantrips;
        }

        private BlueprintFeature CreateCantrips(BlueprintFeature cantrips, BlueprintCharacterClass characterClass,
            StatType stat, BlueprintAbility[] spellList)
        {
            _logger.Debug($"Create cantrips {cantrips.name} with id {cantrips.AssetGuid}");

            _logger.Debug($"Create learn spells");
            LearnSpells learnSpells = _library.Create<LearnSpells>();
            learnSpells.CharacterClass = characterClass;
            learnSpells.Spells = spellList;

            _logger.Debug($"Create bind abilities to class");
            BindAbilitiesToClass bind_spells = _library.Create<BindAbilitiesToClass>();
            bind_spells.Abilites = spellList;
            bind_spells.Stat = stat;
            bind_spells.CharacterClass = characterClass;
            bind_spells.Archetypes = Array.Empty<BlueprintArchetype>();
            bind_spells.AdditionalClasses = Array.Empty<BlueprintCharacterClass>();
            bind_spells.LevelStep = 1;
            bind_spells.Cantrip = true;

            _logger.Debug($"Create add facts");
            AddFacts spells = _library.Create<AddFacts>();
            spells.Facts = spellList;

            _logger.Debug($"Set groups and components");
            cantrips.Groups = new [] {FeatureGroup.None};
            cantrips.SetComponents(spells, learnSpells, bind_spells);

            _logger.Debug($"Create cantrips {cantrips.name} with id {cantrips.AssetGuid}");
            return cantrips;
        }
    }
}
