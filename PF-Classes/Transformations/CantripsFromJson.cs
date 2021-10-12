using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core;
using PF_Core.Factories;
using PF_Core.Repositories;

namespace PF_Classes.Transformations
{
    public class CantripsFromJson
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly FeaturesRepository _featuresRepository = FeaturesRepository.INSTANCE;

        private static readonly FeatureFactory _featureFactory = new FeatureFactory();

        public static BlueprintFeature GetCantrips(Cantrips cantripsData, BlueprintCharacterClass characterClass, BlueprintSpellbook spellbook)
        {
            _logger.Log($"Creating cantrips from JSON data {cantripsData.Guid}");

            BlueprintFeature cantrips = _featureFactory.CreateCantrips(
                cantripsData.Name, cantripsData.Guid, cantripsData.DisplayName, cantripsData.Description,
                getIconFeature(cantripsData.Icon).Icon, characterClass, spellbook);
            
            _logger.Log("DONE: Creating cantrips");
            return cantrips;
        }
        
        private static BlueprintFeature getIconFeature(String value) =>
            _featuresRepository.GetFeature(
                Features.INSTANCE.GetGuidFor(
                    value.Replace("ref:","")));
    }
}
