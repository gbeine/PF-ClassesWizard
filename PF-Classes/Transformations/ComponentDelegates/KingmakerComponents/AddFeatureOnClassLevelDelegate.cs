using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using PF_Classes.JsonTypes;

namespace PF_Classes.Transformations.ComponentDelegates.KingmakerComponents
{
    public class AddFeatureOnClassLevelDelegate : AbstractComponentDelegate
    {
        public static AddFeatureOnClassLevel CreateComponent(Component componentData, BlueprintCharacterClass blueprintCharacterClass)
        {
            AddFeatureOnClassLevel c = _componentFactory.CreateComponent<AddFeatureOnClassLevel>();

            c.name = $"AddFeatureOnClassLevel${c.Feature.name}";
            c.Level = componentData.AsInt("Level");
            c.Feature = getFeature(componentData.AsString("Feature"));
            c.Class = blueprintCharacterClass;
            c.BeforeThisLevel = componentData.Exists("Before") && componentData.AsBool("Before");
            c.Archetypes = Array.Empty<BlueprintArchetype>(); // TODO: implement
            c.AdditionalClasses = Array.Empty<BlueprintCharacterClass>(); // TODO: implement

            return c;
        }
    }
}
