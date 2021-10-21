using Kingmaker.Blueprints.Classes;
using PF_Classes.Identifier;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations
{
    public class ProficienciesFromJson : JsonTransformation
    {
        public static BlueprintFeature GetProficiencies(Proficiencies proficienciesData)
        {
            _logger.Log($"Creating proficiencies from JSON data {proficienciesData.Guid}");

            BlueprintFeature proficiencies = FeatureFromJson.GetFeature(proficienciesData);

            _logger.Log("DONE: Create proficiencies");
            return proficiencies;
        }
    }
}
