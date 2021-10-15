using System;
using System.Linq;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Enums;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;
using PF_Core;
using PF_Core.Extensions;
using PF_Core.Factories;
using PF_Core.Repositories;

namespace PF_Classes.Transformations
{
    public class ProficienciesFromJson
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly FeaturesRepository _featuresRepository = FeaturesRepository.INSTANCE;

        private static readonly FeatureFactory _featureFactory = new FeatureFactory();

        public static BlueprintFeature GetProficiencies(Proficiencies proficienciesData)
        {
            _logger.Log($"Creating proficiencies from JSON data {proficienciesData.Guid}");

            BlueprintFeature proficiencies;
            if (proficienciesData.From != null)
            {
                proficiencies = _featureFactory.CreateFeatureFrom(
                    proficienciesData.Name, proficienciesData.Guid,
                    Features.INSTANCE.GetGuidFor(proficienciesData.From),
                    proficienciesData.DisplayName, proficienciesData.Description);
            }
            else
            {
                proficiencies = _featureFactory.CreateFeature(
                    proficienciesData.Name, proficienciesData.Guid,
                    proficienciesData.DisplayName, proficienciesData.Description);

            }

            foreach (var feature in proficienciesData.AddFeatures)
            {
                proficiencies.AddComponent(
                    _featureFactory.CreateAddFact(getFeature(feature)));
            }
            proficiencies.AddComponent(
                _featureFactory.CreateAddWeaponArmorProficiencies(
                    proficienciesData.AddWeaponProficiencies
                        .Select(weapon => EnumParser.parseWeaponCategory(weapon)),
                    proficienciesData.AddArmorProficiencies
                        .Select(armor => EnumParser.parseArmorProficiency(armor))
                )
            );

            _logger.Log("DONE: Create proficiencies");
            IdentifierRegistry.INSTANCE.Register(proficiencies);
            return proficiencies;
        }

        private static BlueprintFeature getFeature(String value) =>
            _featuresRepository.GetFeature(
                IdentifierLookup.INSTANCE.lookupFeature(value));
    }
}
