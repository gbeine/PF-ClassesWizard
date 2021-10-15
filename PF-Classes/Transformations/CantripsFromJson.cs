using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core;
using PF_Core.Extensions;
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
            if (cantripsData.From != null)
            {
                cantrips = _cantripsFactory.CreateCantripsFrom(
                    cantripsData.Name, cantripsData.Guid, cantripsData.From,
                    cantripsData.DisplayName, cantripsData.Description,
                    getIconFeature(cantripsData.Icon).Icon, characterClass, spellbook);
            }
            else
            {
                cantrips = _cantripsFactory.CreateCantrips(
                    cantripsData.Name, cantripsData.Guid, cantripsData.DisplayName, cantripsData.Description,
                    getIconFeature(cantripsData.Icon).Icon, characterClass, spellbook);
            }

            _logger.Log("DONE: Creating cantrips");
            IdentifierRegistry.INSTANCE.Register(cantrips);
            return cantrips;
        }

        private static BlueprintFeature getIconFeature(String value) =>
            _featuresRepository.GetFeature(
                IdentifierLookup.INSTANCE.lookupFeature(value));
    }
}
