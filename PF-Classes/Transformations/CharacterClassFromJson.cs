using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Alignments;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core.Extensions;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class CharacterClassFromJson : JsonTransformation
    {
        private static readonly CharacterClassFactory _classFactoryFactory = new CharacterClassFactory();
        private static readonly PrerequisitesFactory _prerequisitesFactory = new PrerequisitesFactory();

        public static BlueprintCharacterClass GetCharacterClass(CharacterClass characterClassData)
        {
            _logger.Log($"Creating character class from JSON data {characterClassData.Guid}");

            BlueprintCharacterClass characterClass;
            if (!string.Empty.Equals(characterClassData.From))
                characterClass = _classFactoryFactory.CreateClassFrom(
                    characterClassData.Name, characterClassData.Guid,
                    characterClassData.From, characterClassData.DisplayName, characterClassData.Description);
            else
                characterClass = _classFactoryFactory.CreateClass(
                    characterClassData.Name,  characterClassData.Guid,
                    characterClassData.DisplayName,characterClassData.Description);

            characterClass.m_Icon = SpriteLookup.lookupFor(characterClassData.Icon);
            characterClass.EquipmentEntities = getCharacterClass(characterClassData.EquipmentEntities).EquipmentEntities;
            characterClass.MaleEquipmentEntities = getCharacterClass(characterClassData.MaleEquipmentEntities).MaleEquipmentEntities;
            characterClass.FemaleEquipmentEntities = getCharacterClass(characterClassData.FemaleEquipmentEntities).FemaleEquipmentEntities;
            characterClass.HitDie = EnumParser.parseDiceType(characterClassData.HitDie);
            characterClass.BaseAttackBonus = getStatProgression(characterClassData.BaseAttackBonus);
            characterClass.FortitudeSave = getStatProgression(characterClassData.FortitudeSave);
            characterClass.WillSave = getStatProgression(characterClassData.WillSave);
            characterClass.ReflexSave = getStatProgression(characterClassData.ReflexSave);

            if (characterClassData.PrimaryColor.HasValue)
                characterClass.PrimaryColor = characterClassData.PrimaryColor.Value;
            if (characterClassData.SecondaryColor.HasValue)
                characterClass.SecondaryColor = characterClassData.SecondaryColor.Value;
            if (characterClassData.SkillPoints.HasValue)
                characterClass.SkillPoints = characterClassData.SkillPoints.Value;
            if (characterClassData.IsArcaneCaster.HasValue)
                characterClass.IsArcaneCaster = characterClassData.IsArcaneCaster.Value;
            if (characterClassData.IsDivineCaster.HasValue)
                characterClass.IsDivineCaster = characterClassData.IsDivineCaster.Value;
            if (characterClassData.StartingGold.HasValue)
                characterClass.StartingGold = characterClassData.StartingGold.Value;

            characterClass.ClassSkills =
                characterClassData.ClassSkills
                    .Select(skill => EnumParser.parseStatType(skill)).ToArray();
            characterClass.RecommendedAttributes =
                characterClassData.RecommendedAttributes
                    .Select(skill => EnumParser.parseStatType(skill)).ToArray();
            characterClass.NotRecommendedAttributes =
                characterClassData.NotRecommendedAttributes
                    .Select(skill => EnumParser.parseStatType(skill)).ToArray();

            if (!String.Empty.Equals(characterClassData.ComponentsArray))
            {
                characterClass.ComponentsArray = getCharacterClass(characterClassData.ComponentsArray).ComponentsArray;
            }
            if (!String.Empty.Equals(characterClassData.StartingItems))
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
            if (characterClassData.Spellbook != null)
            {
                BlueprintSpellbook spellbook = SpellbookFromJson.GetSpellbook(characterClassData.Spellbook);
                characterClass.Spellbook = spellbook;
            }

            _logger.Log($"DONE: Creating character class from JSON data {characterClassData.Guid}");
            _identifierRegistry.Register(characterClass);
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
