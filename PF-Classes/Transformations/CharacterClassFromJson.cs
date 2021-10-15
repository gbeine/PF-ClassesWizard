using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Alignments;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core;
using PF_Core.Extensions;
using PF_Core.Factories;
using PF_Core.Repositories;

namespace PF_Classes.Transformations
{
    public class CharacterClassFromJson
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly CharacterClassesRepository _characterClassesRepository = CharacterClassesRepository.INSTANCE;
        private static readonly StatProgressionRepository _statProgressionRepository = StatProgressionRepository.INSTANCE;

        private static readonly CharacterClassFactory _classFactoryFactory = new CharacterClassFactory();
        private static readonly PrerequisitesFactory _prerequisitesFactory = new PrerequisitesFactory();

        public static BlueprintCharacterClass GetCharacterClass(CharacterClass characterClassData)
        {
            _logger.Log($"Creating character class from JSON data {characterClassData.Guid}");

            List<BlueprintFeature> startFeatures = new List<BlueprintFeature>();

            BlueprintCharacterClass characterClass = _classFactoryFactory.CreateClass(characterClassData.Name,
                characterClassData.Guid, characterClassData.DisplayName,characterClassData.Description);

            characterClass.m_Icon = getCharacterClass(characterClassData.Icon).Icon;
            characterClass.EquipmentEntities = getCharacterClass(characterClassData.EquipmentEntities).EquipmentEntities;
            characterClass.MaleEquipmentEntities = getCharacterClass(characterClassData.MaleEquipmentEntities).MaleEquipmentEntities;
            characterClass.FemaleEquipmentEntities = getCharacterClass(characterClassData.FemaleEquipmentEntities).FemaleEquipmentEntities;
            characterClass.PrimaryColor = characterClassData.PrimaryColor;
            characterClass.SecondaryColor = characterClassData.SecondaryColor;
            characterClass.SkillPoints = characterClassData.SkillPoints;
            characterClass.HitDie = EnumParser.parseDiceType(characterClassData.HitDie);
            characterClass.BaseAttackBonus = getStatProgression(characterClassData.BaseAttackBonus);
            characterClass.FortitudeSave = getStatProgression(characterClassData.FortitudeSave);
            characterClass.WillSave = getStatProgression(characterClassData.WillSave);
            characterClass.ReflexSave = getStatProgression(characterClassData.ReflexSave);
            characterClass.IsDivineCaster = characterClassData.IsDivineCaster;
            characterClass.IsArcaneCaster = characterClassData.IsArcaneCaster;
            characterClass.StartingGold = characterClassData.StartingGold;

            characterClass.ClassSkills =
                characterClassData.ClassSkills
                    .Select(skill => EnumParser.parseStatType($"Skill{skill}")).ToArray();
            characterClass.RecommendedAttributes =
                characterClassData.RecommendedAttributes
                    .Select(skill => EnumParser.parseStatType(skill)).ToArray();
            characterClass.NotRecommendedAttributes =
                characterClassData.NotRecommendedAttributes
                    .Select(skill => EnumParser.parseStatType(skill)).ToArray();

            if (characterClassData.ComponentsArray != null)
            {
                characterClass.ComponentsArray = getCharacterClass(characterClassData.ComponentsArray).ComponentsArray;
            }
            if (characterClassData.StartingItems != null)
            {
                characterClass.StartingItems = getCharacterClass(characterClassData.StartingItems).StartingItems;
            }

            List<String> alignmentList = characterClassData.Alignment;
            if (alignmentList != null)
            {
                AlignmentMaskType alignmentMask = AlignmentMaskType.None;
                _logger.Log($"Alignment: {alignmentMask}");
                alignmentList.ForEach(a => alignmentMask |= EnumParser.parseAlignment(a));
                _logger.Log($"Alignment: {alignmentMask}");
                characterClass.AddComponent(
                    _prerequisitesFactory.CreatePrerequisiteAlignment(alignmentMask));
            }

            if (characterClassData.Proficiencies != null)
            {
                BlueprintFeature proficiencies =
                    ProficienciesFromJson.GetProficiencies(characterClassData.Proficiencies);
                startFeatures.Add(proficiencies);
            }

            if ( characterClassData.Spellbook != null )
            {
                BlueprintSpellbook spellbook = SpellbookFromJson.GetSpellbook(characterClassData.Spellbook, characterClass);
                characterClass.Spellbook = spellbook;

                if (characterClassData.Cantrips != null)
                {
                    BlueprintFeature cantrips = CantripsFromJson.GetCantrips(characterClassData.Cantrips, characterClass, spellbook);
                    startFeatures.Add(cantrips);
                }
            }

            BlueprintProgression progression = ProgressionFromJson.GetProgression(characterClassData.Progression, characterClass, startFeatures);
            characterClass.Progression = progression;

            _logger.Log($"DONE: Creating character class from JSON data {characterClassData.Guid}");
            IdentifierRegistry.INSTANCE.Register(characterClass);
            return characterClass;
        }

        private static BlueprintStatProgression getStatProgression(String value) =>
            _statProgressionRepository.GetStatProgression(
                IdentifierLookup.INSTANCE.lookupStatProgession(value));
        private static BlueprintCharacterClass getCharacterClass(String value) =>
            _characterClassesRepository.GetCharacterClass(
                IdentifierLookup.INSTANCE.lookupCharacterClass(value));
    }
}
