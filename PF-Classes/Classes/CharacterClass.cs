using System;
using Harmony12;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using PF_Core.Factories;
using PF_Core.Repositories;

namespace PF_Classes.Classes
{
    public abstract class CharacterClass
    {
        protected static readonly CharacterClassesRepository _classesRepository = CharacterClassesRepository.INSTANCE;
        protected static readonly FeaturesRepository _featuresRepository = FeaturesRepository.INSTANCE;
        protected static readonly SpellbookRepository _spellbookRepository = SpellbookRepository.INSTANCE;

        protected static readonly CharacterClassFactory _classFactoryFactory = new CharacterClassFactory();
        protected static readonly CantripsFactory _cantripsFactory = new CantripsFactory();
        protected static readonly FeatureFactory _featureFactory = new FeatureFactory();
        protected static readonly LevelEntryFactory _levelEntryFactory = new LevelEntryFactory();
        protected static readonly ProgressionFactory _progressionFactory = new ProgressionFactory();
        protected static readonly UIGroupFactory _uiGroupFactory = new UIGroupFactory();
        protected static readonly SpellbookFactory _spellbookFactory = new SpellbookFactory();
        protected static readonly PrerequisitesFactory _prerequisitesFactory = new PrerequisitesFactory();

        protected BlueprintFeature Proficiencies() => _featureFactory.CreateEmptyFeature();
        protected BlueprintFeature Cantrips(BlueprintCharacterClass characterClass) => _featureFactory.CreateEmptyFeature();
        protected BlueprintSpellbook Spellbook(BlueprintCharacterClass characterClass) => _spellbookFactory.createEmptySpellbook();
        protected BlueprintProgression Progession(BlueprintCharacterClass characterClass) => _progressionFactory.CreateEmptyProgression();

        protected LevelEntry[] LevelEntries()
        {
            LevelEntry[] levelEntries = Array.Empty<LevelEntry>();
            for (int i = 1; i < 21; i++)
            {
                levelEntries.Add(_levelEntryFactory.CreateLevelEntry(i));
            }
            return levelEntries;
        }

        protected BlueprintFeatureBase[] UiDeterminatorsGroup() => Empty<BlueprintFeatureBase>();
        protected UIGroup[] UiGroups() => Empty<UIGroup>();
        protected StatType[] ClassSkills() => Empty<StatType>();
        protected StatType[] RecommendedAttributes() => Empty<StatType>();
        protected StatType[] NotRecommendedAttributes() => Empty<StatType>();

        private T[] Empty<T>()
        {
            return Array.Empty<T>();
        }
    }
}
