using System;
using System.Collections.Generic;
using System.Linq;
using Harmony12;
using JetBrains.Annotations;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using PF_Classes.JsonTypes;
using PF_Core.Extensions;
using PF_Core.Facades;
using PF_Core.Factories;

namespace PF_Classes.Transformations.ComponentDelegates
{
    public class KingmakerComponentDelegates : JsonTransformation
    {
        private static readonly ComponentFactory _componentFactory = ComponentFactory.INSTANCE;

        internal static readonly Dictionary<String, Action<BlueprintScriptableObject, Component>> CreateComponentDelegates =
            new Dictionary<string, Action<BlueprintScriptableObject, Component>>();

        static KingmakerComponentDelegates()
        {

            _logger.Debug($"Adding delegate: AbilityDeliverTouch");
            CreateComponentDelegates.Add("AbilityDeliverTouch",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AbilityDeliverTouch>(c =>
                        {
                            c.TouchWeapon = _itemRepository.GetWeapon(
                            _identifierLookup.lookupItem(componentData.AsString("TouchWeapon")));
                        })
                    ));

            _logger.Debug($"Adding delegate: AbilityResourceLogic");
            CreateComponentDelegates.Add("AbilityResourceLogic",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AbilityResourceLogic>()));

            // This parses JSON components containing actions
            //
            //    "Type": "AbilityEffectRunAction",
            //    "Actions": [
            //        {                                                      // this part is done via ActionFromJson
            //          "Type": "ContextActionSpawnAreaEffect",
            //          "AreaEffect": "loc:WallOfFireSpellAbilityArea",
            //          "BonusValue": "Default",
            //          "Rate": "Rounds",
            //          "DiceType": "Zero",
            //          "DiceCountValue": "0"
            //        }
            //    ]
            _logger.Debug($"Adding delegate: AbilityEffectRunAction");
            CreateComponentDelegates.Add("AbilityEffectRunAction",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AbilityEffectRunAction>(c =>
                        {
                            if (componentData.Exists("Actions"))
                            {
                                List<GameAction> actions = new List<GameAction>();
                                foreach (var action in componentData.AsList<JsonTypes.Action>("Actions"))
                                {
                                    actions.Add(ActionFromJson.CreateAction(action));
                                }

                                c.Actions = new ActionList() {Actions = actions.ToArray()};
                            }
                        })
                    ));

            _logger.Debug($"Adding delegate: AddAdditionalLimb");
            CreateComponentDelegates.Add("AddAdditionalLimb",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddAdditionalLimb>(c =>
                    {
                        c.Weapon = _itemRepository.GetWeapon(
                            _identifierLookup.lookupItem(componentData.AsString("Weapon")));

                    })
                    ));

            _logger.Debug($"Adding delegate: AddConditionImmunity");
            CreateComponentDelegates.Add("AddConditionImmunity",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddConditionImmunity>(c =>
                        {
                            c.Condition = EnumParser.parseUnitCondition(componentData.AsString("Condition"));
                        })
                    ));

            _logger.Debug($"Adding delegate: AddDamageResistancePhysical");
            CreateComponentDelegates.Add("AddDamageResistancePhysical",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddDamageResistancePhysical>(c =>
                        {
                            c.BypassedByMagic = componentData.Exists("BypassedByMagic") && componentData.AsBool("BypassedByMagic");
                            if (componentData.Exists("Value"))
                                c.Value = componentData.AsInt("Value");
                        })
                    ));

            _logger.Debug($"Adding delegate: AddEnergyImmunity");
            CreateComponentDelegates.Add("AddEnergyImmunity",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddEnergyImmunity>(c =>
                    {
                        c.Type = EnumParser.parseDamageEnergyType(componentData.AsString("DamageEnergyType"));
                    })
                ));

            _logger.Debug($"Adding delegate: AddFacts");
            CreateComponentDelegates.Add("AddFacts",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddFacts>(c =>
                    {
                        BlueprintUnitFact[] facts = Array.Empty<BlueprintUnitFact>();
                        String identifier = componentData.AsString("Fact");
                        String name = "AddFacts$";
                        if (_identifierLookup.existsSpellbook(identifier))
                        {
                            BlueprintSpellbook spellbook =
                                _spellbookRepository.GetSpellbook(_identifierLookup.lookupSpellbook(identifier));
                            name += spellbook.name;
                            facts = spellbook.SpellList.SpellsByLevel[0].Spells.ToArray();
                        }
                        else if (_identifierLookup.existsFeature(identifier))
                        {
                            BlueprintFeature feature =
                                _featuresRepository.GetFeature(_identifierLookup.lookupFeature(identifier));
                            name += feature.name;
                            facts.Add(feature);
                        }
                        else
                        {
                            string message = $"Not able to locate Fact: {identifier}";
                            _logger.Error(message);
                            throw new InvalidOperationException(message);
                        }

                        if (facts.Length > 0)
                        {
                            c.name = name;
                            c.Facts = facts;
                        }
                    })
                ));

            _logger.Debug($"Adding delegate: AddMechanicsFeature");
            CreateComponentDelegates.Add("AddMechanicsFeature",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddMechanicsFeature>(c =>
                        {
                            c.SetField("m_Feature", EnumParser.parseMechanicsFeatureType(componentData.AsString("Mechanics")));
                        })
                    ));

            _logger.Debug($"Adding delegate: AddProficiencies");
            CreateComponentDelegates.Add("AddProficiencies",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddProficiencies>(c =>
                        {
                            if (componentData.Exists("WeaponProficiencies"))
                                c.WeaponProficiencies = componentData.AsArray("WeaponProficiencies")
                                    .Select(w => EnumParser.parseWeaponCategory(w)).ToArray();
                            if (componentData.Exists("ArmorProficiencies"))
                                c.ArmorProficiencies = componentData.AsArray("ArmorProficiencies")
                                    .Select(a => EnumParser.parseArmorProficiency(a)).ToArray();
                        })
                    ));

            _logger.Debug($"Adding delegate: AddSpellResistance");
            CreateComponentDelegates.Add("AddSpellResistance",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddSpellResistance>(c =>
                        {
                            c.Value = new ContextValue()
                            {
                                ValueType = ContextValueType.Rank,
                                ValueRank = componentData.Exists("Rank")
                                    ? EnumParser.parseAbilityRankType(componentData.AsString("Rank"))
                                    : AbilityRankType.Default
                            };
                        })
                    ));

            _logger.Debug($"Adding delegate: AddStatBonus");
            CreateComponentDelegates.Add("AddStatBonus",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddStatBonus>(c =>
                        {
                            c.Stat = EnumParser.parseStatType(componentData.AsString("Stat"));
                            c.Value = componentData.AsInt("Bonus");
                            c.Descriptor = componentData.Exists("Descriptor")
                                ? EnumParser.parseModifierDescriptor(componentData.AsString("Descriptor"))
                                : ModifierDescriptor.UntypedStackable;
                        })
                    ));

            _logger.Debug($"Adding delegate: Blindsense");
            CreateComponentDelegates.Add("Blindsense",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<Blindsense>(c =>
                        {
                            c.Range = componentData.AsInt("Range").Feet();
                            c.Blindsight = componentData.Exists("Blindsight") && componentData.AsBool("Blindsight");
                        })
                    ));

            _logger.Debug($"Adding delegate: BuffDescriptorImmunity");
            CreateComponentDelegates.Add("BuffDescriptorImmunity",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<BuffDescriptorImmunity>(c =>
                        {
                            c.Descriptor = EnumParser.parseSpellDescriptor(componentData.AsString("Descriptor"));
                        })
                    ));

            _logger.Debug($"Adding delegate: ContextRankConfig");
            CreateComponentDelegates.Add("ContextRankConfig",
                (target, componentData) => target.AddComponent(createContextRankConfig(componentData)));

            _logger.Debug($"Adding delegate: NoSelectionIfAlreadyHasFeature");
            CreateComponentDelegates.Add("NoSelectionIfAlreadyHasFeature",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<NoSelectionIfAlreadyHasFeature>(c =>
                        {
                            c.AnyFeatureFromSelection = componentData.AsBool("AnyFeatureFromSelection");
                            c.Features = componentData.Exists("Features")
                                ? componentData.AsArray("Features")
                                    .Select(f => _featuresRepository.GetFeature(_identifierLookup.lookupFeature(f)))
                                    .ToArray()
                                : Array.Empty<BlueprintFeature>();
                        })
                    ));

            _logger.Debug($"Adding delegate: PrerequisiteNoFeature");
            CreateComponentDelegates.Add("PrerequisiteNoFeature",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<PrerequisiteNoFeature>(c =>
                        {
                            c.Feature = _featuresRepository.GetFeature(
                                _identifierLookup.lookupFeature(componentData.AsString("Feature"))
                                );
                            c.Group = Prerequisite.GroupType.All;
                        })
                    ));

            _logger.Debug($"Adding delegate: RemoveFeatureOnApply");
            CreateComponentDelegates.Add("RemoveFeatureOnApply",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<RemoveFeatureOnApply>(c =>
                        {
                            c.Feature = _featuresRepository.GetFeature(
                                _identifierLookup.lookupFeature(componentData.AsString("Feature"))
                                );
                        })
                    ));

            _logger.Debug($"Adding delegate: SavingThrowBonusAgainstDescriptor");
            CreateComponentDelegates.Add("SavingThrowBonusAgainstDescriptor",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<SavingThrowBonusAgainstDescriptor>(c =>
                    {
                        c.Bonus = componentData.Exists("Bonus")
                            ? componentData.AsInt("Bonus")
                            : 0;
                        c.Value = componentData.Exists("Value")
                            ? componentData.AsInt("Value")
                            : 0;
                        c.ModifierDescriptor = EnumParser.parseModifierDescriptor(componentData.AsString("ModifierDescriptor"));
                        c.SpellDescriptor = EnumParser.parseSpellDescriptor(componentData.AsString("SpellDescriptor"));
                    })
                ));

            _logger.Debug($"Adding delegate: SavingThrowBonusAgainstSpecificSpells");
            CreateComponentDelegates.Add("SavingThrowBonusAgainstSpecificSpells",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<SavingThrowBonusAgainstSpecificSpells>(c =>
                    {
                        c.Value = componentData.Exists("Value")
                            ? componentData.AsInt("Value")
                            : 0;

                        if (componentData.Exists("ModifierDescriptor"))
                            c.ModifierDescriptor = EnumParser.parseModifierDescriptor(componentData.AsString("ModifierDescriptor"));

                        if (componentData.Exists("Spells"))
                        {
                            c.Spells = Array.Empty<BlueprintAbility>();
                            foreach (var spell in componentData.AsArray("Spells"))
                            {
                                c.Spells.Add(
                                    _spellbookRepository.GetSpell(
                                        _identifierLookup.lookupSpell(spell)));
                            }
                        }
                        else
                        {
                            c.Spells = new BlueprintAbility[0];
                        }
                        if (componentData.Exists("BypassFeatures"))
                        {
                            c.BypassFeatures = Array.Empty<BlueprintFeature>();
                            foreach (var feature in componentData.AsArray("BypassFeatures"))
                            {
                                c.BypassFeatures.Add(
                                    _featuresRepository.GetFeature(
                                        _identifierLookup.lookupFeature(feature)));
                            }
                        }
                        else
                        {
                            c.BypassFeatures = new BlueprintFeature[0];
                        }
                    })
                ));

            _logger.Debug($"Adding delegate: SpecificBuffImmunity");
            CreateComponentDelegates.Add("SpecificBuffImmunity",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<SpecificBuffImmunity>(c =>
                        {
                            c.Buff = _buffRepository.GetBuff(
                                _identifierLookup.lookupBuff(componentData.AsString("Buff"))
                                );
                        })
                    ));

            _logger.Debug($"Adding delegate: SpellComponent");
            CreateComponentDelegates.Add("SpellComponent",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<SpellComponent>(c =>
                        {
                            if (componentData.Exists("School"))
                                c.School = EnumParser.parseSpellSchool(componentData.AsString("School"));
                        })
                    ));

            _logger.Debug($"Adding delegate: SpellImmunityToSpellDescriptor");
            CreateComponentDelegates.Add("SpellImmunityToSpellDescriptor",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<SpellImmunityToSpellDescriptor>(c =>
                        {
                            SpellDescriptor spellDescriptor = SpellDescriptor.None;
                            foreach (var sd in componentData.AsArray("Descriptor"))
                            {
                                spellDescriptor |= EnumParser.parseSpellDescriptor(sd);
                            }
                            c.Descriptor = spellDescriptor;
                        })
                    ));

        }

        internal static ContextRankConfig createContextRankConfig(Component componentData) =>
            createContextRankConfig(componentData, Array.Empty<BlueprintCharacterClass>());

        internal static ContextRankConfig createContextRankConfig(Component componentData,
            [NotNull] BlueprintCharacterClass[] blueprintCharacterClasses)
        {
            AbilityRankType type = componentData.Exists("RankType")
                ? EnumParser.parseAbilityRankType(componentData.AsString("RankType"))
                : AbilityRankType.Default;
            ContextRankBaseValueType baseValueType = componentData.Exists("BaseValueType")
                ? EnumParser.parseContextRankBaseValueType(componentData.AsString("BaseValueType"))
                : ContextRankBaseValueType.CasterLevel;
            ContextRankProgression progression = componentData.Exists("RankProgression")
                ? EnumParser.parseContextRankProgression(componentData.AsString("RankProgression"))
                : ContextRankProgression.AsIs;
            StatType stat = componentData.Exists("Stat")
                ? EnumParser.parseStatType(componentData.AsString("Stat"))
                : StatType.Unknown;

            int? min = componentData.Exists("Min")
                ? componentData.AsInt("Min")
                : (int?) null;
            int? max = componentData.Exists("Max")
                ? componentData.AsInt("Max")
                : (int?) null;
            ;
            int startLevel = componentData.Exists("StartLevel")
                ? componentData.AsInt("StartLevel")
                : 0;
            int stepLevel = componentData.Exists("StepLevel")
                ? componentData.AsInt("StepLevel")
                : 0;

            bool exceptClasses = componentData.Exists("ExceptClasses") && componentData.AsBool("ExceptClasses");

            BlueprintCharacterClass[] classes = blueprintCharacterClasses;
            BlueprintFeature feature = componentData.Exists("Feature")
                ? _featuresRepository.GetFeature(
                    _identifierLookup.lookupFeature(componentData.AsString("Feature")))
                : null;

            BlueprintUnitProperty customProperty = null;
            BlueprintArchetype archetype = null;
            BlueprintFeature[] featureList = Array.Empty<BlueprintFeature>();
            (int, int)[] customProgression = null;

            return _componentFactory.CreateComponent<ContextRankConfig>(c =>
            {
                c.SetType(type);
                c.SetBaseValueType(baseValueType);
                c.SetProgression(progression);
                c.SetStat(stat);
                c.SetUseMin(min.HasValue);
                c.SetMin(min.GetValueOrDefault());
                c.SetUseMax(max.HasValue);
                c.SetMax(max.GetValueOrDefault());
                c.SetStartLevel(startLevel);
                c.SetStepLevel(stepLevel);
                c.SetExceptClasses(exceptClasses);

                c.SetFeature(feature);
                c.SetCustomProperty(customProperty);
                c.SetClass(classes);
                c.SetArchetype(archetype);
                c.SetFeatureList(featureList);

                if (customProgression != null)
                {
                    Type customProgressionItemType = c.GetTypeOf("CustomProgressionItem");
                    var items = Array.CreateInstance(customProgressionItemType, customProgression.Length);
                    for (int i = 0; i < items.Length; i++)
                    {
                        var item = Activator.CreateInstance(customProgressionItemType);
                        var p = customProgression[i];

                        item.SetField("BaseValue", p.Item1);
                        item.SetField("ProgressionValue", p.Item2);
                        items.SetValue(item, i);
                    }

                    c.SetCustomProgression(items);
                }
            });

        }
    }
}
