using System;
using System.Collections.Generic;
using Kingmaker.Blueprints.Classes;

namespace PF_Core.Factories
{
    public class LevelEntryFactory
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        public LevelEntry LevelEntry(int level) =>
            LevelEntry(level, Array.Empty<BlueprintFeature>() );
        
        public LevelEntry LevelEntry(int level, BlueprintFeatureBase feature) =>
            LevelEntry(level, new BlueprintFeatureBase[] { feature });
        
        public LevelEntry LevelEntry(int level, params BlueprintFeatureBase[] features) =>
            LevelEntry(level, (IEnumerable<BlueprintFeatureBase>)features);

        public LevelEntry LevelEntry(int level, IEnumerable<BlueprintFeatureBase> features)
        {
            LevelEntry entry = new LevelEntry() { Level = level };
            entry.Features.AddRange(features);
            return entry;
        }
    }
}
