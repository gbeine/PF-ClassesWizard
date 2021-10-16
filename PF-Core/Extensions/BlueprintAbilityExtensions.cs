using System.Linq;
using Harmony12;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Designers.Mechanics.Recommendations;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using UnityEngine;

namespace PF_Core.Extensions
{
    public static class BlueprintAbilityExtensions
    {
        public static void AddRecommendNoFeature(this BlueprintAbility blueprintAbility, BlueprintFeature feature)
        {
            var recommendNoFeat = blueprintAbility.GetComponents<RecommendationNoFeatFromGroup>().FirstOrDefault(r => r.GoodIfNoFeature = false);
            if (recommendNoFeat != null)
            {
                recommendNoFeat.Features = recommendNoFeat.Features.AddToArray(feature);
            }
            else
            {
                recommendNoFeat = ScriptableObject.CreateInstance<RecommendationNoFeatFromGroup>();
                recommendNoFeat.Features = new BlueprintUnitFact[] { feature };
                blueprintAbility.AddComponent(recommendNoFeat);
            }
        }
    }
}
