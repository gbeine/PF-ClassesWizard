using System;
using System.Collections.Generic;
using Kingmaker.Blueprints.Classes;

namespace PF_Core.Factories
{
    public class LevelEntryFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        public LevelEntry CreateLevelEntry(int level) =>
            CreateLevelEntry(level, Array.Empty<BlueprintFeature>() );

        public LevelEntry CreateLevelEntry(int level, BlueprintFeatureBase feature) =>
            CreateLevelEntry(level, new BlueprintFeatureBase[] { feature });

        public LevelEntry CreateLevelEntry(int level, params BlueprintFeatureBase[] features) =>
            CreateLevelEntry(level, (IEnumerable<BlueprintFeatureBase>)features);

        public LevelEntry CreateLevelEntry(int level, IEnumerable<BlueprintFeatureBase> features)
        {
            LevelEntry entry = new LevelEntry() { Level = level };
            entry.Features.AddRange(features);
            return entry;
        }
    }
}
