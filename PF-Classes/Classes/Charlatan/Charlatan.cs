using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Alignments;
using PF_Classes.Identifier;
using PF_Core.Extensions;
using PF_Core.Filter;
using PF_Core;

namespace PF_Classes.Classes.Charlatan
{
    public class Charlatan : CharacterClass
    {
        public const String CHARLATAN_CLASS = "4da4a7d55cee43608a64babeb8d3ca73";
        public const String CHARLATAN_PROGRESSION = "3106acb568bb47a0b3d11adc6c378c14";
        public const String CHARLATAN_PROFICIENCIES = "6d817419f36c4ba7833466e434a7fbd9";
        public const String CHARLATAN_SPELLBOOK = "0fc2fdfb15ec4abd888ef5a7b7e59003";
        public const String CHARLATAN_CANTRIPS = "2464f9cfc5a34b608b5a9edb9c5f6e7b";

        private const String CHARLATAN_CLASS_NAME = "CharlatanClass";
        private const String CHARLATAN_CLASS_DISPLAY_NAME = "Charlatan";

        private const String CHARLATAN_CLASS_DESCRIPTION =
            "The charlatan is a jolly fellow who gets through life with a few tricks and a bit of magic. Only very rarely does a charlatan have to take up arms, in most cases she manages to get out of trouble through illusion or persuasion.";

        private const String CHARLATAN_PROGRESSION_NAME = "CharlatanProgression";
        private const String CHARLATAN_SPELLBOOK_NAME = "CharlatanSpellbook";

        private const String CHARLATAN_PROFICIENCIES_NAME = "CharlatanProficiencies";
        private const String CHARLATAN_PROFICIENCIES_DISPLAY_NAME = "Charlatan Proficiencies";

        private const String CHARLATAN_PROFICIENCIES_DESCRIPTION =
            "Charlatans are skilled in the use of all simple weapons, short swords, long swords, dueling swords, rapiers, and short bows. They are proficient with light armour and shields.";

        private static readonly Logger _logger = Logger.INSTANCE;

        public Charlatan()
        {
            BlueprintCharacterClass charlatan_class = CharlatanCharacterClass();

            charlatan_class.Spellbook = Spellbook(charlatan_class);

            BlueprintFeature charlatan_proficiencies = Proficiencies();
            BlueprintFeature charlatan_cantrips = Cantrips(charlatan_class);

            charlatan_class.Progression = Progession(charlatan_class);

            charlatan_class.ClassSkills = ClassSkills();
            charlatan_class.RecommendedAttributes = RecommendedAttributes();
            charlatan_class.NotRecommendedAttributes = NotRecommendedAttributes();
            charlatan_class.AddComponent(
                _prerequisitesFactory.CreatePrerequisiteAlignment(
                    AlignmentMaskType.NeutralGood |
                    AlignmentMaskType.ChaoticGood |
                    AlignmentMaskType.ChaoticNeutral |
                    AlignmentMaskType.TrueNeutral
                )
            );

            _classesRepository.RegisterCharacterClass(charlatan_class);
        }

        private BlueprintCharacterClass CharlatanCharacterClass()
        {
            BlueprintCharacterClass charlatan_class = _classFactoryFactory.CreateClass(CHARLATAN_CLASS_NAME,
                CHARLATAN_CLASS, CHARLATAN_CLASS_DISPLAY_NAME, CHARLATAN_CLASS_DESCRIPTION);

            BlueprintCharacterClass kineticist_class =
                _classesRepository.GetCharacterClass(PF_Classes.Identifier.CharacterClasses.KINETICIST);
            BlueprintCharacterClass rouge_class = _classesRepository.GetCharacterClass(PF_Classes.Identifier.CharacterClasses.ROGUE);

            charlatan_class.m_Icon = kineticist_class.Icon;
            charlatan_class.EquipmentEntities = kineticist_class.EquipmentEntities;
            charlatan_class.MaleEquipmentEntities = kineticist_class.MaleEquipmentEntities;
            charlatan_class.FemaleEquipmentEntities = kineticist_class.FemaleEquipmentEntities;
            charlatan_class.PrimaryColor = kineticist_class.PrimaryColor;
            charlatan_class.SecondaryColor = kineticist_class.SecondaryColor;
            charlatan_class.SkillPoints = rouge_class.SkillPoints;
            charlatan_class.HitDie = rouge_class.HitDie;
            charlatan_class.BaseAttackBonus = rouge_class.BaseAttackBonus;
            charlatan_class.FortitudeSave = kineticist_class.FortitudeSave;
            charlatan_class.WillSave = rouge_class.WillSave;
            charlatan_class.ReflexSave = rouge_class.ReflexSave;
            charlatan_class.IsDivineCaster = false;
            charlatan_class.IsArcaneCaster = true;
            charlatan_class.ComponentsArray = kineticist_class.ComponentsArray;
            charlatan_class.StartingGold = kineticist_class.StartingGold;
            charlatan_class.StartingItems = rouge_class.StartingItems;

            return charlatan_class;
        }

        private new BlueprintFeature Proficiencies()
        {
            _logger.Log("Create proficiencies");

            BlueprintFeature charlatan_proficiencies = _featureFactory.CreateFeatureFrom(
                CHARLATAN_PROFICIENCIES_NAME,
                CHARLATAN_PROFICIENCIES,
                Identifier.Features.BARD_PROFICIENCIES,
                CHARLATAN_PROFICIENCIES_DISPLAY_NAME,
                CHARLATAN_PROFICIENCIES_DESCRIPTION);

            charlatan_proficiencies.AddComponent(
                _featureFactory.CreateAddFact(
                    _featuresRepository.GetFeature(Features.WEAPON_PROFICIENCY_DUELING_SWORD)));
            charlatan_proficiencies.AddComponent(
                _featureFactory.CreateAddFact(
                    _featuresRepository.GetFeature(Features.WEAPON_PROFICIENCY_ESTOC)));
            charlatan_proficiencies.AddComponent(
                _featureFactory.CreateAddWeaponProficiencies(
                    new WeaponCategory[] { WeaponCategory.Longbow, WeaponCategory.Starknife }));

            _logger.Log("DONE: Create proficiencies");
            return charlatan_proficiencies;
        }

        private new BlueprintFeature Cantrips(BlueprintCharacterClass charlatan_class)
        {
            _logger.Log("Create cantrips");

            BlueprintFeature charlatan_cantrips = _featureFactory.CreateCantrips(
                "CharlatanCantrips",
                CHARLATAN_CANTRIPS,
                "Cantrips",
                "Charlatans have cantrips ;-)",
                _featuresRepository.GetFeature(Features.ARCANE_SCHOOL_ILLUSION_BLINDING_RAY).Icon,
                charlatan_class,
                StatType.Intelligence,
                charlatan_class.Spellbook.SpellList.SpellsByLevel[0].Spells.ToArray()
            );

            _logger.Log("DONE: Create cantrips");
            return charlatan_cantrips;
        }

        private new BlueprintSpellbook Spellbook(BlueprintCharacterClass charlatan_class)
        {
            _logger.Log("Create spellbook");

            BlueprintCharacterClass wizard_class = _classesRepository.GetCharacterClass(Identifier.CharacterClasses.WIZARD);
            BlueprintCharacterClass bard_class = _classesRepository.GetCharacterClass(Identifier.CharacterClasses.BARD);
            BlueprintCharacterClass ranger_class = _classesRepository.GetCharacterClass(Identifier.CharacterClasses.RANGER);

            BlueprintSpellList charlatanSpellList =
                _spellbookFactory.createSpellList("CharlatanSpellList", "8b4fc86d687646648c551a740718118c", 10);
            charlatanSpellList.SpellsByLevel[1].Spells.Add(_spellRepository.GetSpell(Spells.CURE_LIGHT_WOUNDS_CAST));
            charlatanSpellList.SpellsByLevel[1].Spells.Add(_spellRepository.GetSpell(Spells.SUMMON_MONSTER_I_SINGLE));
            charlatanSpellList.SpellsByLevel[2].Spells.Add(_spellRepository.GetSpell(Spells.CURE_MODERATE_WOUNDS_CAST));
            charlatanSpellList.SpellsByLevel[2].Spells.Add(_spellRepository.GetSpell(Spells.SUMMON_MONSTER_II_BASE));
            charlatanSpellList.SpellsByLevel[2].Spells.Add(_spellRepository.GetSpell(Spells.MAGE_ARMOR));
            charlatanSpellList.SpellsByLevel[2].Spells.Add(_spellRepository.GetSpell(Spells.DELAY_POISON));
            charlatanSpellList.SpellsByLevel[3].Spells.Add(_spellRepository.GetSpell(Spells.CURE_SERIOUS_WOUNDS_CAST));
            charlatanSpellList.SpellsByLevel[3].Spells.Add(_spellRepository.GetSpell(Spells.SUMMON_MONSTER_III_BASE));
            charlatanSpellList.SpellsByLevel[3].Spells.Add(_spellRepository.GetSpell(Spells.RESTORATION_LESSER));
            charlatanSpellList.SpellsByLevel[4].Spells.Add(_spellRepository.GetSpell(Spells.CURE_CRITICAL_WOUNDS_CAST));
            charlatanSpellList.SpellsByLevel[4].Spells.Add(_spellRepository.GetSpell(Spells.SUMMON_MONSTER_IV_BASE));
            charlatanSpellList.SpellsByLevel[4].Spells.Add(_spellRepository.GetSpell(Spells.DELAY_POISON_COMMUNAL));
            charlatanSpellList.SpellsByLevel[5].Spells.Add(_spellRepository.GetSpell(Spells.CURE_LIGHT_WOUNDS_MASS));
            charlatanSpellList.SpellsByLevel[5].Spells.Add(_spellRepository.GetSpell(Spells.SUMMON_MONSTER_V_BASE));
            charlatanSpellList.SpellsByLevel[5].Spells.Add(_spellRepository.GetSpell(Spells.RESTORATION));
            charlatanSpellList.SpellsByLevel[6].Spells.Add(_spellRepository.GetSpell(Spells.CURE_MODERATE_WOUNDS_MASS));
            charlatanSpellList.SpellsByLevel[6].Spells.Add(_spellRepository.GetSpell(Spells.SUMMON_MONSTER_VI_BASE));
            charlatanSpellList.SpellsByLevel[6].Spells.Add(_spellRepository.GetSpell(Spells.NEUTRALIZE_POISON));
            charlatanSpellList.SpellsByLevel[7].Spells.Add(_spellRepository.GetSpell(Spells.CURE_SERIOUS_WOUNDS_MASS));
            charlatanSpellList.SpellsByLevel[7].Spells.Add(_spellRepository.GetSpell(Spells.SUMMON_MONSTER_VII_BASE));
            charlatanSpellList.SpellsByLevel[5].Spells.Add(_spellRepository.GetSpell(Spells.RESTORATION_GREATER));
            charlatanSpellList.SpellsByLevel[8].Spells.Add(_spellRepository.GetSpell(Spells.CURE_CRITICAL_WOUNDS_MASS));
            charlatanSpellList.SpellsByLevel[8].Spells.Add(_spellRepository.GetSpell(Spells.SUMMON_MONSTER_VIII_BASE));
            charlatanSpellList.SpellsByLevel[9].Spells.Add(_spellRepository.GetSpell(Spells.SUMMON_MONSTER_IX_BASE));

            BlueprintSpellList spellList = new SpellListFilter(wizard_class.Spellbook.SpellList)
                .AddSpellsFromList(bard_class.Spellbook.SpellList)
                .AddSpellsFromList(ranger_class.Spellbook.SpellList)
                .ExcludeSpellsFromList(p => (
                    p.School == SpellSchool.Conjuration
                    || p.School == SpellSchool.Evocation
                    || p.School == SpellSchool.Necromancy))
                .AddSpellsFromList(charlatanSpellList)
                .SpellSpellList;

            BlueprintSpellbook spellbook = _spellbookFactory.CreateSpellbook(
                CHARLATAN_SPELLBOOK_NAME,
                CHARLATAN_SPELLBOOK,
                charlatan_class,
                true,
                true,
                true,
                false,
                StatType.Intelligence,
                CantripsType.Cantrips,
                SpellsKnown(),
                SpellsPerDay(),
                spellList
            );

            _logger.Log("DONE: Create spellbook");
            return spellbook;
        }

        private new BlueprintProgression Progession(BlueprintCharacterClass charlatan_class)
        {
            BlueprintProgression charlatan_progression = _progressionFactory.CreateProgression(
                CHARLATAN_PROGRESSION_NAME,
                CHARLATAN_PROGRESSION, charlatan_class.Name,
                charlatan_class.Description, charlatan_class.Icon, FeatureGroup.None, charlatan_class);
            charlatan_progression.LevelEntries = LevelEntries();
            charlatan_progression.UIDeterminatorsGroup = UiDeterminatorsGroup();
            charlatan_progression.UIGroups = UiGroups();

            return charlatan_progression;
        }

        private new StatType[] ClassSkills()
        {
            return new StatType[]
            {
                StatType.SkillThievery,
                StatType.SkillStealth,
                StatType.SkillKnowledgeArcana,
                StatType.SkillKnowledgeWorld,
                StatType.SkillPerception,
                StatType.SkillPersuasion,
                StatType.SkillUseMagicDevice
            };
        }

        private new StatType[] RecommendedAttributes()
        {
            return new StatType[]
            {
                StatType.Dexterity,
                StatType.Intelligence,
                StatType.Charisma
            };
        }

        private new LevelEntry[] LevelEntries()
        {
            return new LevelEntry[]
            {
                _levelEntryFactory.CreateLevelEntry(1,
                    _featuresRepository.GetFeature(CHARLATAN_PROFICIENCIES),
                    _featuresRepository.GetFeature(Features.COMMON_DETECT_MAGIC),
                    _featuresRepository.GetFeature(Features.COMMON_SNEAK_ATTACK),
                    _featuresRepository.GetFeature(Features.ROGUE_WEAPON_FINESSE),
                    _featuresRepository.GetFeature(Features.ROGUE_TRAPFINDING),
                    _featuresRepository.GetFeature(Features.INQUISITOR_TACTICAL_LEADER_DIPLOMACY),
                    _featuresRepository.GetFeature(Features.BARD_BARDIC_KNOWLEDGE),
                    _featuresRepository.GetFeature(CHARLATAN_CANTRIPS)
                ),
                _levelEntryFactory.CreateLevelEntry(2,
                    _featuresRepository.GetFeature(Features.COMMON_EVASION),
                    _featuresRepository.GetFeature(Features.ROGUE_TALENT_SELECTION),
                    _featuresRepository.GetFeature(Features.WIZARD_FEAT_SELECTION)
                ),
                _levelEntryFactory.CreateLevelEntry(3,
                    _featuresRepository.GetFeature(Features.ARCANE_SCHOOL_ILLUSION_BLINDING_RAY),
                    _featuresRepository.GetFeature(Features.INQUISITOR_TEAMWORK_FEAT)
                ),
                _levelEntryFactory.CreateLevelEntry(4,
                    _featuresRepository.GetFeature(Features.COMMON_SNEAK_ATTACK),
                    _featuresRepository.GetFeature(Features.ROGUE_TALENT_SELECTION)
                ),
                _levelEntryFactory.CreateLevelEntry(5,
                    _featuresRepository.GetFeature(Features.COMMON_UNCANNY_DODGE)
                ),
                _levelEntryFactory.CreateLevelEntry(6,
                    _featuresRepository.GetFeature(Features.ROGUE_TALENT_SELECTION),
                    _featuresRepository.GetFeature(Features.WIZARD_FEAT_SELECTION),
                    _featuresRepository.GetFeature(Features.INQUISITOR_TEAMWORK_FEAT)
                ),
                _levelEntryFactory.CreateLevelEntry(7,
                    _featuresRepository.GetFeature(Features.COMMON_SNEAK_ATTACK)
                ),
                _levelEntryFactory.CreateLevelEntry(8,
                    _featuresRepository.GetFeature(Features.ARCANE_SCHOOL_ILLUSION_INVISIBILITY_FIELD),
                    _featuresRepository.GetFeature(Features.ROGUE_TALENT_SELECTION)
                ),
                _levelEntryFactory.CreateLevelEntry(9,
                    _featuresRepository.GetFeature(Features.INQUISITOR_TEAMWORK_FEAT)
                ),
                _levelEntryFactory.CreateLevelEntry(10,
                    _featuresRepository.GetFeature(Features.COMMON_SNEAK_ATTACK),
                    _featuresRepository.GetFeature(Features.ROGUE_TALENT_SELECTION),
                    _featuresRepository.GetFeature(Features.WIZARD_FEAT_SELECTION),
                    _featuresRepository.GetFeature(Features.COMMON_IMPROVED_EVASION),
                    _featuresRepository.GetFeature(Features.COMMON_ADVANCED_TALENTS)
                ),
                _levelEntryFactory.CreateLevelEntry(11,
                    _featuresRepository.GetFeature(Features.BARD_JACK_OF_ALL_TRADES)
                ),
                _levelEntryFactory.CreateLevelEntry(12,
                    _featuresRepository.GetFeature(Features.ROGUE_TALENT_SELECTION),
                    _featuresRepository.GetFeature(Features.INQUISITOR_TEAMWORK_FEAT)
                ),
                _levelEntryFactory.CreateLevelEntry(13,
                    _featuresRepository.GetFeature(Features.ROGUE_IMPROVED_UNCANNY_DODGE),
                    _featuresRepository.GetFeature(Features.COMMON_SNEAK_ATTACK)
                ),
                _levelEntryFactory.CreateLevelEntry(14,
                    _featuresRepository.GetFeature(Features.ROGUE_TALENT_SELECTION),
                    _featuresRepository.GetFeature(Features.WIZARD_FEAT_SELECTION)
                ),
                _levelEntryFactory.CreateLevelEntry(15,
                    _featuresRepository.GetFeature(Features.INQUISITOR_TEAMWORK_FEAT)
                ),
                _levelEntryFactory.CreateLevelEntry(16,
                    _featuresRepository.GetFeature(Features.COMMON_SNEAK_ATTACK),
                    _featuresRepository.GetFeature(Features.ROGUE_TALENT_SELECTION)
                ),
                _levelEntryFactory.CreateLevelEntry(17),
                _levelEntryFactory.CreateLevelEntry(18,
                    _featuresRepository.GetFeature(Features.ROGUE_TALENT_SELECTION),
                    _featuresRepository.GetFeature(Features.WIZARD_FEAT_SELECTION),
                    _featuresRepository.GetFeature(Features.INQUISITOR_TEAMWORK_FEAT)
                ),
                _levelEntryFactory.CreateLevelEntry(19,
                    _featuresRepository.GetFeature(Features.COMMON_SNEAK_ATTACK)
                ),
                _levelEntryFactory.CreateLevelEntry(20,
                    _featuresRepository.GetFeature(Features.ROGUE_TALENT_SELECTION)
                )
            };
        }

        private new BlueprintFeatureBase[] UiDeterminatorsGroup()
        {
            return new BlueprintFeatureBase[]
            {
                _featuresRepository.GetFeature(Features.ROGUE_TRAPFINDING),
                _featuresRepository.GetFeature(Features.ROGUE_WEAPON_FINESSE),
                _featuresRepository.GetFeature(Features.COMMON_DETECT_MAGIC),
                _featuresRepository.GetFeature(CHARLATAN_CANTRIPS),
                _featuresRepository.GetFeature(CHARLATAN_PROFICIENCIES)
            };
        }

        private new UIGroup[] UiGroups()
        {
            return new UIGroup[]
            {
                _uiGroupFactory.CreateUIGroup(
                    _featuresRepository.GetFeature(Features.ROGUE_TALENT_SELECTION)
                ),
                _uiGroupFactory.CreateUIGroup(
                    _featuresRepository.GetFeature(Features.ARCANE_SCHOOL_ILLUSION_BLINDING_RAY),
                    _featuresRepository.GetFeature(Features.ARCANE_SCHOOL_ILLUSION_INVISIBILITY_FIELD),
                    _featuresRepository.GetFeature(Features.COMMON_ADVANCED_TALENTS)
                ),
                _uiGroupFactory.CreateUIGroup(
                    _featuresRepository.GetFeature(Features.INQUISITOR_TACTICAL_LEADER_DIPLOMACY),
                    _featuresRepository.GetFeature(Features.INQUISITOR_TEAMWORK_FEAT)
                ),
                _uiGroupFactory.CreateUIGroup(
                    _featuresRepository.GetFeature(Features.BARD_BARDIC_KNOWLEDGE),
                    _featuresRepository.GetFeature(Features.BARD_JACK_OF_ALL_TRADES)
                ),
                _uiGroupFactory.CreateUIGroup(
                    _featuresRepository.GetFeature(Features.COMMON_EVASION),
                    _featuresRepository.GetFeature(Features.COMMON_UNCANNY_DODGE),
                    _featuresRepository.GetFeature(Features.COMMON_IMPROVED_EVASION),
                    _featuresRepository.GetFeature(Features.ROGUE_IMPROVED_UNCANNY_DODGE)
                )

            };
        }

        private BlueprintSpellsTable SpellsPerDay()
        {
            return _spellbookFactory.createSpellsTable("CharlatanSpellsPerDay", "d9adb154906244f39fd7439a5f4d6ac2",
                _spellbookFactory.createSpellsLevelEntry(), // 0
                _spellbookFactory.createSpellsLevelEntry(0, 3), //1
                _spellbookFactory.createSpellsLevelEntry(0, 4), //2
                _spellbookFactory.createSpellsLevelEntry(0, 5, 3), //3
                _spellbookFactory.createSpellsLevelEntry(0, 6, 4), //4
                _spellbookFactory.createSpellsLevelEntry(0, 6, 5, 3), //5
                _spellbookFactory.createSpellsLevelEntry(0, 6, 6, 4), //6
                _spellbookFactory.createSpellsLevelEntry(0, 6, 6, 5, 3), //7
                _spellbookFactory.createSpellsLevelEntry(0, 6, 6, 6, 4), //8
                _spellbookFactory.createSpellsLevelEntry(0, 6, 6, 6, 5, 3), //9
                _spellbookFactory.createSpellsLevelEntry(0, 6, 6, 6, 6, 4), //10
                _spellbookFactory.createSpellsLevelEntry(0, 6, 6, 6, 6, 5, 3), //11
                _spellbookFactory.createSpellsLevelEntry(0, 6, 6, 6, 6, 6, 4), //12
                _spellbookFactory.createSpellsLevelEntry(0, 6, 6, 6, 6, 6, 5, 3), //13
                _spellbookFactory.createSpellsLevelEntry(0, 6, 6, 6, 6, 6, 6, 4), //14
                _spellbookFactory.createSpellsLevelEntry(0, 6, 6, 6, 6, 6, 6, 5, 3), //15
                _spellbookFactory.createSpellsLevelEntry(0, 6, 6, 6, 6, 6, 6, 6, 4), //16
                _spellbookFactory.createSpellsLevelEntry(0, 6, 6, 6, 6, 6, 6, 6, 5, 3), //17
                _spellbookFactory.createSpellsLevelEntry(0, 6, 6, 6, 6, 6, 6, 6, 6, 4), //18
                _spellbookFactory.createSpellsLevelEntry(0, 6, 6, 6, 6, 6, 6, 6, 6, 5), //19
                _spellbookFactory.createSpellsLevelEntry(0, 6, 6, 6, 6, 6, 6, 6, 6, 6) //20
            );
        }

        private BlueprintSpellsTable SpellsKnown()
        {
            return _spellbookFactory.createSpellsTable("CharlatanSpellsKnown", "69b34210916a46fc8dd031950aa5d9b7",
                _spellbookFactory.createSpellsLevelEntry(0, 0), // 0
                _spellbookFactory.createSpellsLevelEntry(0, 6), //1
                _spellbookFactory.createSpellsLevelEntry(0, 6), //2
                _spellbookFactory.createSpellsLevelEntry(0, 6, 2), //3
                _spellbookFactory.createSpellsLevelEntry(0, 6, 2), //4
                _spellbookFactory.createSpellsLevelEntry(0, 6, 2, 2), //5
                _spellbookFactory.createSpellsLevelEntry(0, 6, 2, 2), //6
                _spellbookFactory.createSpellsLevelEntry(0, 6, 4, 2, 2), //7
                _spellbookFactory.createSpellsLevelEntry(0, 6, 4, 2, 2), //8
                _spellbookFactory.createSpellsLevelEntry(0, 7, 4, 4, 2, 2), //9
                _spellbookFactory.createSpellsLevelEntry(0, 7, 4, 4, 2, 2), //10
                _spellbookFactory.createSpellsLevelEntry(0, 7, 6, 4, 4, 2, 2), //11
                _spellbookFactory.createSpellsLevelEntry(0, 7, 6, 4, 4, 2, 2), //12
                _spellbookFactory.createSpellsLevelEntry(0, 8, 6, 6, 4, 4, 2, 2), //13
                _spellbookFactory.createSpellsLevelEntry(0, 8, 6, 6, 4, 4, 2, 2), //14
                _spellbookFactory.createSpellsLevelEntry(0, 8, 7, 6, 6, 4, 4, 2, 2), //15
                _spellbookFactory.createSpellsLevelEntry(0, 8, 7, 6, 6, 4, 4, 2, 2), //16
                _spellbookFactory.createSpellsLevelEntry(0, 8, 7, 7, 6, 6, 4, 4, 2, 2), //17
                _spellbookFactory.createSpellsLevelEntry(0, 8, 7, 7, 6, 6, 4, 4, 4, 2), //18
                _spellbookFactory.createSpellsLevelEntry(0, 8, 8, 7, 7, 6, 6, 4, 4, 4), //19
                _spellbookFactory.createSpellsLevelEntry(0, 8, 8, 7, 7, 6, 6, 4, 4, 4) //20
            );
        }
    }
}
