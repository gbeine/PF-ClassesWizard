using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
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

        private static readonly CantripsFactory _cantripsFactory = new CantripsFactory();

        public static BlueprintFeature GetCantrips(Cantrips cantripsData, BlueprintCharacterClass characterClass, BlueprintSpellbook spellbook)
        {
            _logger.Log($"Creating cantrips from JSON data {cantripsData.Guid}");

            BlueprintFeature cantrips;
            if (!String.Empty.Equals(cantripsData.From))
            {
                cantrips = _cantripsFactory.CreateCantripsFrom(
                    cantripsData.Name, cantripsData.Guid, cantripsData.From,
                    cantripsData.DisplayName, cantripsData.Description,
                    SpriteLookup.lookupFor(cantripsData.From), characterClass, spellbook);
            }
            else
            {
                cantrips = _cantripsFactory.CreateCantrips(
                    cantripsData.Name, cantripsData.Guid, cantripsData.DisplayName, cantripsData.Description,
                    SpriteLookup.lookupFor(cantripsData.Icon), characterClass, spellbook);
            }

            _logger.Log("DONE: Creating cantrips");
            IdentifierRegistry.INSTANCE.Register(cantrips);
            return cantrips;
        }
    }
}
