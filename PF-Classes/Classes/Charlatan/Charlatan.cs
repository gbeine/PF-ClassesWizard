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
                _prerequisitesFactory.createPrerequisiteAlignment(
                    AlignmentMaskType.Chaotic |
                    AlignmentMaskType.NeutralGood |
                    AlignmentMaskType.NeutralEvil |
                    AlignmentMaskType.TrueNeutral
                )
            );

            _classesRepository.RegisterCharacterClass(charlatan_class);
        }

        private BlueprintCharacterClass CharlatanCharacterClass()
        {
            BlueprintCharacterClass charlatan_class = _classFactoryFactory.createClass(CHARLATAN_CLASS_NAME,
                CHARLATAN_CLASS, CHARLATAN_CLASS_DISPLAY_NAME, CHARLATAN_CLASS_DESCRIPTION);

            var kineticist_class =
                _classesRepository.GetCharacterClass(PF_Classes.Identifier.CharacterClasses.KINETICIST);
            var rouge_class = _classesRepository.GetCharacterClass(PF_Classes.Identifier.CharacterClasses.ROUGE);

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

            BlueprintFeature charlatan_proficiencies = _featureFactory.createFeatureFrom(
                CHARLATAN_PROFICIENCIES_NAME,
                CHARLATAN_PROFICIENCIES,
                Identifier.Features.BARD_PROFICIENCIES,
                CHARLATAN_PROFICIENCIES_DISPLAY_NAME,
                CHARLATAN_PROFICIENCIES_DESCRIPTION);

            charlatan_proficiencies.AddComponent(
                _featureFactory.createAddFact(
                    _featuresRepository.GetFeature(Features.WEAPON_PROFICIENCY_DUELLING_SWORD)));
            charlatan_proficiencies.AddComponent(
                _featureFactory.createAddFact(_featuresRepository.GetFeature(Features.WEAPON_PROFICIENCY_ESTOC)));
            charlatan_proficiencies.AddComponent(
                _featureFactory.createAddWeaponProficiencies(
                    WeaponCategory.Longbow, WeaponCategory.Starknife)
            );

            _logger.Log("DONE: Create proficiencies");
            return charlatan_proficiencies;
        }

        private new BlueprintFeature Cantrips(BlueprintCharacterClass charlatan_class)
        {
            _logger.Log("Create cantrips");
            BlueprintFeature charlatan_cantrips = _featureFactory.createCantrips(
                "CharlatanCantrips",
                "Cantrips",
                "Charlatans have cantrips ;-)",
                _featuresRepository.GetFeature(Features.ARCANE_SCHOOL_ILLUSION_BLINDING_RAY).Icon,
                CHARLATAN_CANTRIPS,
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
            var wizard_class = _classesRepository.GetCharacterClass(Identifier.CharacterClasses.WIZARD);
            var bard_class = _classesRepository.GetCharacterClass(Identifier.CharacterClasses.BARD);
            var ranger_class = _classesRepository.GetCharacterClass(Identifier.CharacterClasses.RANGER);

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
                .addSpellsFromList(bard_class.Spellbook.SpellList)
                .addSpellsFromList(ranger_class.Spellbook.SpellList)
                .excludeSpellsFromList(p => (
                    p.School == SpellSchool.Conjuration
                    || p.School == SpellSchool.Evocation
                    || p.School == SpellSchool.Necromancy))
                .addSpellsFromList(charlatanSpellList)
                .SpellSpellList;

            BlueprintSpellbook spellbook = _spellbookFactory.createSpellbook(
                CHARLATAN_SPELLBOOK_NAME,
                CHARLATAN_SPELLBOOK,
                charlatan_class,
                true,
                true,
                true,
                false,
                StatType.Intelligence,
                SpellsKnown(),
                SpellsPerDay(),
                CantripsType.Cantrips,
                spellList
            );
            
            _logger.Log("DONE: Create spellbook");
            return spellbook;
        }

        private new BlueprintProgression Progession(BlueprintCharacterClass charlatan_class)
        {
            BlueprintProgression charlatan_progression = _progressionFactory.createProgression(
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
                _levelEntryFactory.LevelEntry(1,
                    _featuresRepository.GetFeature(CHARLATAN_PROFICIENCIES),
                    _featuresRepository.GetFeature(Features.COMMON_DETECT_MAGIC),
                    _featuresRepository.GetFeature(Features.COMMON_SNEAK_ATTACK),
                    _featuresRepository.GetFeature(Features.ROUGE_WEAPON_FINESSE),
                    _featuresRepository.GetFeature(Features.ROUGE_TRAPFINDING),
                    _featuresRepository.GetFeature(Features.INQUISITOR_TACTICAL_LEADER_LEADERS_WORDS),
                    _featuresRepository.GetFeature(Features.BARD_BARDIC_KNOWLEDGE),
                    _featuresRepository.GetFeature(CHARLATAN_CANTRIPS)
                ),
                _levelEntryFactory.LevelEntry(2,
                    _featuresRepository.GetFeature(Features.COMMON_EVASION),
                    _featuresRepository.GetFeature(Features.ROUGE_TALENT),
                    _featuresRepository.GetFeature(Features.WIZARD_BONUS)
                ),
                _levelEntryFactory.LevelEntry(3,
                    _featuresRepository.GetFeature(Features.ARCANE_SCHOOL_ILLUSION_BLINDING_RAY),
                    _featuresRepository.GetFeature(Features.INQUISITOR_TEAMWORK)
                ),
                _levelEntryFactory.LevelEntry(4,
                    _featuresRepository.GetFeature(Features.COMMON_SNEAK_ATTACK),
                    _featuresRepository.GetFeature(Features.ROUGE_TALENT)
                ),
                _levelEntryFactory.LevelEntry(5,
                    _featuresRepository.GetFeature(Features.COMMON_UNCANNY_DODGE)
                ),
                _levelEntryFactory.LevelEntry(6,
                    _featuresRepository.GetFeature(Features.ROUGE_TALENT),
                    _featuresRepository.GetFeature(Features.WIZARD_BONUS),
                    _featuresRepository.GetFeature(Features.INQUISITOR_TEAMWORK)
                ),
                _levelEntryFactory.LevelEntry(7,
                    _featuresRepository.GetFeature(Features.COMMON_SNEAK_ATTACK)
                ),
                _levelEntryFactory.LevelEntry(8,
                    _featuresRepository.GetFeature(Features.ARCANE_SCHOOL_ILLUSION_INVISIBILITY_FIELD),
                    _featuresRepository.GetFeature(Features.ROUGE_TALENT)
                ),
                _levelEntryFactory.LevelEntry(9,
                    _featuresRepository.GetFeature(Features.INQUISITOR_TEAMWORK)
                ),
                _levelEntryFactory.LevelEntry(10,
                    _featuresRepository.GetFeature(Features.COMMON_SNEAK_ATTACK),
                    _featuresRepository.GetFeature(Features.ROUGE_TALENT),
                    _featuresRepository.GetFeature(Features.WIZARD_BONUS),
                    _featuresRepository.GetFeature(Features.COMMON_IMPROVED_EVASION),
                    _featuresRepository.GetFeature(Features.COMMON_ADVANCED_TALENTS)
                ),
                _levelEntryFactory.LevelEntry(11,
                    _featuresRepository.GetFeature(Features.BARD_JACK_OF_ALL_TRADES)
                ),
                _levelEntryFactory.LevelEntry(12,
                    _featuresRepository.GetFeature(Features.ROUGE_TALENT),
                    _featuresRepository.GetFeature(Features.INQUISITOR_TEAMWORK)
                ),
                _levelEntryFactory.LevelEntry(13,
                    _featuresRepository.GetFeature(Features.ROUGE_IMPROVED_UNCANNY_DODGE),
                    _featuresRepository.GetFeature(Features.COMMON_SNEAK_ATTACK)
                ),
                _levelEntryFactory.LevelEntry(14,
                    _featuresRepository.GetFeature(Features.ROUGE_TALENT),
                    _featuresRepository.GetFeature(Features.WIZARD_BONUS)
                ),
                _levelEntryFactory.LevelEntry(15,
                    _featuresRepository.GetFeature(Features.INQUISITOR_TEAMWORK)
                ),
                _levelEntryFactory.LevelEntry(16,
                    _featuresRepository.GetFeature(Features.COMMON_SNEAK_ATTACK),
                    _featuresRepository.GetFeature(Features.ROUGE_TALENT)
                ),
                _levelEntryFactory.LevelEntry(17),
                _levelEntryFactory.LevelEntry(18,
                    _featuresRepository.GetFeature(Features.ROUGE_TALENT),
                    _featuresRepository.GetFeature(Features.WIZARD_BONUS),
                    _featuresRepository.GetFeature(Features.INQUISITOR_TEAMWORK)
                ),
                _levelEntryFactory.LevelEntry(19,
                    _featuresRepository.GetFeature(Features.COMMON_SNEAK_ATTACK)
                ),
                _levelEntryFactory.LevelEntry(20,
                    _featuresRepository.GetFeature(Features.ROUGE_TALENT)
                )
            };
        }

        private new BlueprintFeatureBase[] UiDeterminatorsGroup()
        {
            return new BlueprintFeatureBase[]
            {
                _featuresRepository.GetFeature(Features.ROUGE_TRAPFINDING),
                _featuresRepository.GetFeature(Features.ROUGE_WEAPON_FINESSE),
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
                    _featuresRepository.GetFeature(Features.ROUGE_TALENT)
                ),
                _uiGroupFactory.CreateUIGroup(
                    _featuresRepository.GetFeature(Features.ARCANE_SCHOOL_ILLUSION_BLINDING_RAY),
                    _featuresRepository.GetFeature(Features.ARCANE_SCHOOL_ILLUSION_INVISIBILITY_FIELD),
                    _featuresRepository.GetFeature(Features.COMMON_ADVANCED_TALENTS)
                ),
                _uiGroupFactory.CreateUIGroup(
                    _featuresRepository.GetFeature(Features.INQUISITOR_TACTICAL_LEADER_LEADERS_WORDS),
                    _featuresRepository.GetFeature(Features.INQUISITOR_TEAMWORK)
                ),
                _uiGroupFactory.CreateUIGroup(
                    _featuresRepository.GetFeature(Features.BARD_BARDIC_KNOWLEDGE),
                    _featuresRepository.GetFeature(Features.BARD_JACK_OF_ALL_TRADES)
                ),
                _uiGroupFactory.CreateUIGroup(
                    _featuresRepository.GetFeature(Features.COMMON_EVASION),
                    _featuresRepository.GetFeature(Features.COMMON_UNCANNY_DODGE),
                    _featuresRepository.GetFeature(Features.COMMON_IMPROVED_EVASION),
                    _featuresRepository.GetFeature(Features.ROUGE_IMPROVED_UNCANNY_DODGE)
                )

            };
        }

        private BlueprintSpellsTable SpellsPerDay()
        {
            return _spellbookFactory.createSpellsTable("CharlatanSpellsPerDay", "d9adb154906244f39fd7439a5f4d6ac2",
                _spellbookFactory.createSpellsLevelEntry(), // 0
                _spellbookFactory.createSpellsLevelEntry(0, 1), //1
                _spellbookFactory.createSpellsLevelEntry(0, 2), //2
                _spellbookFactory.createSpellsLevelEntry(0, 2, 1), //3
                _spellbookFactory.createSpellsLevelEntry(0, 3, 2), //4
                _spellbookFactory.createSpellsLevelEntry(0, 3, 2, 1), //5
                _spellbookFactory.createSpellsLevelEntry(0, 3, 3, 2), //6
                _spellbookFactory.createSpellsLevelEntry(0, 4, 3, 2, 1), //7
                _spellbookFactory.createSpellsLevelEntry(0, 4, 3, 3, 2), //8
                _spellbookFactory.createSpellsLevelEntry(0, 4, 4, 3, 2, 1), //9
                _spellbookFactory.createSpellsLevelEntry(0, 4, 4, 3, 3, 2), //10
                _spellbookFactory.createSpellsLevelEntry(0, 4, 4, 4, 3, 2, 1), //11
                _spellbookFactory.createSpellsLevelEntry(0, 4, 4, 4, 3, 3, 2), //12
                _spellbookFactory.createSpellsLevelEntry(0, 4, 4, 4, 4, 3, 2, 1), //13
                _spellbookFactory.createSpellsLevelEntry(0, 4, 4, 4, 4, 3, 3, 2), //14
                _spellbookFactory.createSpellsLevelEntry(0, 4, 4, 4, 4, 4, 3, 2, 1), //15
                _spellbookFactory.createSpellsLevelEntry(0, 4, 4, 4, 4, 4, 3, 3, 2), //16
                _spellbookFactory.createSpellsLevelEntry(0, 4, 4, 4, 4, 4, 4, 3, 2, 1), //17
                _spellbookFactory.createSpellsLevelEntry(0, 4, 4, 4, 4, 4, 4, 3, 3, 2), //18
                _spellbookFactory.createSpellsLevelEntry(0, 4, 4, 4, 4, 4, 4, 4, 3, 3), //19
                _spellbookFactory.createSpellsLevelEntry(0, 4, 4, 4, 4, 4, 4, 4, 4, 4) //20
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
                _spellbookFactory.createSpellsLevelEntry(0, 7, 4, 2, 2), //7
                _spellbookFactory.createSpellsLevelEntry(0, 7, 4, 2, 2), //8
                _spellbookFactory.createSpellsLevelEntry(0, 7, 4, 4, 2, 2), //9
                _spellbookFactory.createSpellsLevelEntry(0, 7, 4, 4, 2, 2), //10
                _spellbookFactory.createSpellsLevelEntry(0, 8, 6, 4, 4, 2, 2), //11
                _spellbookFactory.createSpellsLevelEntry(0, 8, 6, 4, 4, 2, 2), //12
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
