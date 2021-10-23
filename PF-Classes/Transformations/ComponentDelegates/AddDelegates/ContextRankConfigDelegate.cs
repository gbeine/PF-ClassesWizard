using System;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Components;
using PF_Classes.JsonTypes;
using PF_Core.Extensions;

namespace PF_Classes.Transformations.ComponentDelegates.AddDelegates
{
    public class ContextRankConfigDelegate : Delegate
    {
        public static ContextRankConfig CreateComponent(Component componentData, BlueprintCharacterClass blueprintCharacterClass) =>
            CreateComponent(componentData, new []{ blueprintCharacterClass });

        public static ContextRankConfig CreateComponent(Component componentData) =>
            CreateComponent(componentData, Array.Empty<BlueprintCharacterClass>());

        public static ContextRankConfig CreateComponent(Component componentData, BlueprintCharacterClass[] blueprintCharacterClasses)
        {
            ContextRankConfig c = _componentFactory.CreateComponent<ContextRankConfig>();

            c.SetType(
                componentData.Exists("RankType")
                    ? EnumParser.parseAbilityRankType(componentData.AsString("RankType"))
                    : AbilityRankType.Default);
            c.SetBaseValueType(
                componentData.Exists("BaseValueType")
                    ? EnumParser.parseContextRankBaseValueType(componentData.AsString("BaseValueType"))
                    : ContextRankBaseValueType.CasterLevel);
            c.SetProgression(
                componentData.Exists("RankProgression")
                    ? EnumParser.parseContextRankProgression(componentData.AsString("RankProgression"))
                    : ContextRankProgression.AsIs);
            c.SetStat(
                componentData.Exists("Stat")
                    ? EnumParser.parseStatType(componentData.AsString("Stat"))
                    : StatType.Unknown);
            c.SetUseMin(componentData.Exists("Min"));
            c.SetMin(
                componentData.Exists("Min")
                    ? componentData.AsInt("Min")
                    : 0);
            c.SetUseMax(componentData.Exists("Max"));
            c.SetMax(
                    componentData.Exists("Max")
                        ? componentData.AsInt("Max")
                        : 20);
            c.SetStartLevel(
                componentData.Exists("StartLevel")
                    ? componentData.AsInt("StartLevel")
                    : 0);
            c.SetStepLevel(
                componentData.Exists("StepLevel")
                    ? componentData.AsInt("StepLevel")
                    : 0);
            c.SetExceptClasses(componentData.Exists("ExceptClasses") && componentData.AsBool("ExceptClasses"));

            c.SetFeature(
                componentData.Exists("Feature")
                    ? getFeature(componentData.AsString("Feature"))
                    : null);
            c.SetClass(blueprintCharacterClasses);

            c.SetCustomProperty(null); // TODO: implement
            c.SetArchetype(null); // TODO: implement
            c.SetFeatureList(Array.Empty<BlueprintFeature>());  // TODO: implement

            // TODO: implement customProgression
            // (int, int)[] customProgression = null;
            // if (customProgression != null)
            // {
            //     Type customProgressionItemType = c.GetTypeOf("CustomProgressionItem");
            //     var items = Array.CreateInstance(customProgressionItemType, customProgression.Length);
            //     for (int i = 0; i < items.Length; i++)
            //     {
            //         var item = Activator.CreateInstance(customProgressionItemType);
            //         var p = customProgression[i];
            //
            //         item.SetField("BaseValue", p.Item1);
            //         item.SetField("ProgressionValue", p.Item2);
            //         items.SetValue(item, i);
            //     }
            //
            //     c.SetCustomProgression(items);
            // }

            return c;
        }
    }
}
