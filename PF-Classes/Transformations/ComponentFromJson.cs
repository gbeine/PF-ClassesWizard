using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using PF_Classes.JsonTypes;
using PF_Core.CallOfTheWild.BuffMechanics;
using PF_Core.CallOfTheWild.ConcealementMechanics;
using PF_Core.CallOfTheWild.EncumbranceMechanics;
using PF_Core.CallOfTheWild.HarmlessSaves;
using PF_Core.CallOfTheWild.MetamagicMechanics;
using PF_Core.CallOfTheWild.NewMechanics;
using PF_Core.CallOfTheWild.SpellFailureMechanics;
using PF_Core.CallOfTheWild.UndeadMechanics;
using PF_Core.Extensions;
using PF_Core.Facades;
using PF_Core.Factories;

namespace PF_Classes.Transformations
{
    public class ComponentFromJson : JsonTransformation
    {
        private static readonly ComponentFactory _componentFactory = ComponentFactory.INSTANCE;

        public static void AddComponent(BlueprintScriptableObject target, Component componentData, BlueprintCharacterClass characterClass)
        {
            _logger.Log($"Creating component from JSON data {componentData.Type}");

            if (_createComponentClassDelegates.ContainsKey(componentData.Type))
            {
                _createComponentClassDelegates[componentData.Type](target, componentData, characterClass);
            }
            else if (_createComponentDelegates.ContainsKey(componentData.Type))
            {
                _createComponentDelegates[componentData.Type](target, componentData);
            }
            else
            {
                String message = $"Unknown component type {componentData.Type}";
                _logger.Error(message);
                throw new InvalidOperationException(message);
            }

            _logger.Log($"DONE: Creating component from JSON data {componentData.Type}");
        }

        public static void AddComponent(BlueprintScriptableObject target, Component componentData)
        {
            _logger.Log($"Creating component from JSON data {componentData.Type}");

            if (_createComponentDelegates.ContainsKey(componentData.Type))
            {
                _createComponentDelegates[componentData.Type](target, componentData);
            }
            else
            {
                String message = $"Unknown component type {componentData.Type}";
                _logger.Error(message);
                throw new InvalidOperationException(message);
            }

            _logger.Log($"DONE: Creating component from JSON data {componentData.Type}");
        }

        private static BlueprintAbility getSpell(String value) =>
            _spellbookRepository.GetSpell(_identifierLookup.lookupSpell(value));

        private static readonly Dictionary<String, Action<BlueprintScriptableObject, Component>> _createComponentDelegates =
            new Dictionary<string, Action<BlueprintScriptableObject, Component>>();

        private static readonly Dictionary<String, Action<BlueprintScriptableObject, Component, BlueprintCharacterClass>> _createComponentClassDelegates =
            new Dictionary<string, Action<BlueprintScriptableObject, Component, BlueprintCharacterClass>>();

        static ComponentFromJson()
        {
            // _logger.Debug($"Adding delegate: X");
            // _createComponentDelegates.Add("X",
            //     (target, componentData) => target.AddComponent(
            //         new BlueprintComponent() // TODO: implement
            //     ));
            //
            // components from Pathfinder Kingmaker
            //
            // Character class depending components

            _logger.Debug($"Adding delegate: AddFeatureOnClassLevel");
            _createComponentClassDelegates.Add("AddFeatureOnClassLevel",
                (target, componentData, characterClass) => target.AddComponent(
                    _componentFactory.CreateComponent<AddFeatureOnClassLevel>(c =>
                        {
                            c.Level = componentData.AsInt("Level");
                            c.Feature = _featuresRepository.GetFeature(
                                _identifierLookup.lookupFeature(
                                    componentData.AsString("Feature")));
                            c.name = $"AddFeatureOnClassLevel${c.Feature.name}";
                            c.Class = characterClass;
                            c.BeforeThisLevel = componentData.Exists("Before") && componentData.AsBool("Before");
                            // c.AdditionalClasses = classes.Skip(1).ToArray();
                            // c.Archetypes = new BlueprintArchetype[0];
                        })
                    ));

            _logger.Debug($"Adding delegate: AddKnownSpell");
            _createComponentClassDelegates.Add("AddKnownSpell",
                (target, componentData, characterClass) => target.AddComponent(
                    _componentFactory.CreateComponent<AddKnownSpell>(c =>
                        {
                            c.Spell = getSpell(componentData.AsString("Spell"));
                            c.SpellLevel = componentData.AsInt("SpellLevel");
                            c.CharacterClass = characterClass;
                        })
                    ));

            _logger.Debug($"Adding delegate: ContextRankConfig");
            _createComponentClassDelegates.Add("ContextRankConfig",
                (target, componentData, characterClass) => target.AddComponent(createContextRankConfig(componentData, characterClass)));

            // Regular components
            _logger.Debug($"Adding delegate: AbilityDeliverTouch");
            _createComponentDelegates.Add("AbilityDeliverTouch",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AbilityDeliverTouch>(c =>
                        {
                            c.TouchWeapon = _itemRepository.GetWeapon(
                            _identifierLookup.lookupItem(componentData.AsString("TouchWeapon")));
                        })
                    ));

            _logger.Debug($"Adding delegate: AbilityResourceLogic");
            _createComponentDelegates.Add("AbilityResourceLogic",
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
            _createComponentDelegates.Add("AbilityEffectRunAction",
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
            _createComponentDelegates.Add("AddAdditionalLimb",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddAdditionalLimb>(c =>
                    {
                        c.Weapon = _itemRepository.GetWeapon(
                            _identifierLookup.lookupItem(componentData.AsString("Weapon")));

                    })
                    ));

            _logger.Debug($"Adding delegate: AddConditionImmunity");
            _createComponentDelegates.Add("AddConditionImmunity",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddConditionImmunity>(c =>
                        {
                            c.Condition = EnumParser.parseUnitCondition(componentData.AsString("Condition"));
                        })
                    ));

            _logger.Debug($"Adding delegate: AddEnergyImmunity");
            _createComponentDelegates.Add("AddEnergyImmunity",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddEnergyImmunity>(c =>
                    {
                        c.Type = EnumParser.parseDamageEnergyType(componentData.AsString("DamageEnergyType"));
                    })
                ));

            _logger.Debug($"Adding delegate: AddFacts");
            _createComponentDelegates.Add("AddFacts",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddFacts>(c =>
                    {
                        BlueprintUnitFact fact = null;
                        String identifier = componentData.AsString("Fact");
                        if (_identifierLookup.existsFeature(identifier))
                            fact = _featuresRepository.GetFeature(_identifierLookup.lookupFeature(identifier));

                        if (fact != null)
                        {
                            c.name = $"AddFacts${fact.name}";
                            c.Facts = new BlueprintUnitFact[] { fact };
                        }
                        else
                        {
                            _logger.Error($"Not able to locate Fact: {identifier}");
                        }
                    })
                ));

            _logger.Debug($"Adding delegate: AddMechanicsFeature");
            _createComponentDelegates.Add("AddMechanicsFeature",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddMechanicsFeature>(c =>
                        {
                            c.SetField("m_Feature", EnumParser.parseMechanicsFeatureType(componentData.AsString("Mechanics")));
                        })
                    ));

            _logger.Debug($"Adding delegate: AddSpellResistance");
            _createComponentDelegates.Add("AddSpellResistance",
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
            _createComponentDelegates.Add("AddStatBonus",
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
            _createComponentDelegates.Add("Blindsense",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<Blindsense>(c =>
                        {
                            c.Range = componentData.AsInt("Range").Feet();
                            c.Blindsight = componentData.Exists("Blindsight")
                                ? componentData.AsBool("Blindsight")
                                : false;
                        })
                    ));

            _logger.Debug($"Adding delegate: BuffDescriptorImmunity");
            _createComponentDelegates.Add("BuffDescriptorImmunity",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<BuffDescriptorImmunity>(c =>
                        {
                            c.Descriptor = EnumParser.parseSpellDescriptor(componentData.AsString("Descriptor"));
                        })
                    ));

            _logger.Debug($"Adding delegate: ContextRankConfig");
            _createComponentDelegates.Add("ContextRankConfig",
                (target, componentData) => target.AddComponent(createContextRankConfig(componentData)));

            _logger.Debug($"Adding delegate: NoSelectionIfAlreadyHasFeature");
            _createComponentDelegates.Add("NoSelectionIfAlreadyHasFeature",
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
            _createComponentDelegates.Add("PrerequisiteNoFeature",
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
            _createComponentDelegates.Add("RemoveFeatureOnApply",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<RemoveFeatureOnApply>(c =>
                        {
                            c.Feature = _featuresRepository.GetFeature(
                                _identifierLookup.lookupFeature(componentData.AsString("Feature"))
                                );
                        })
                    ));

            _logger.Debug($"Adding delegate: SavingThrowBonusAgainstDescriptor");
            _createComponentDelegates.Add("SavingThrowBonusAgainstDescriptor",
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

            _logger.Debug($"Adding delegate: SpecificBuffImmunity");
            _createComponentDelegates.Add("SpecificBuffImmunity",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<SpecificBuffImmunity>(c =>
                        {
                            c.Buff = _buffRepository.GetBuff(
                                _identifierLookup.lookupBuff(componentData.AsString("Buff"))
                                );
                        })
                    ));

            _logger.Debug($"Adding delegate: SpellImmunityToSpellDescriptor");
            _createComponentDelegates.Add("SpellImmunityToSpellDescriptor",
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

            //
            // components from CallOfTheWild
            //

            _logger.Debug($"Adding delegate: AddOutgoingConcealment");
            _createComponentDelegates.Add("AddOutgoingConcealment",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddOutgoingConcealment>(c =>
                        {
                            c.CheckDistance = componentData.Exists("CheckDistance")
                                ? componentData.AsBool("CheckDistance")
                                : true;
                            c.Descriptor = EnumParser.parseConcealmentDescriptor(componentData.AsString("Descriptor"));
                            c.DistanceGreater = componentData.AsInt("DistanceGreater").Feet();
                            c.Concealment = EnumParser.parseConcealment(componentData.AsString("Concealment"));
                        })
                    ));

            _logger.Debug($"Adding delegate: AddSpeedBonusBasedOnRaceSize");
            _createComponentDelegates.Add("AddSpeedBonusBasedOnRaceSize",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddSpeedBonusBasedOnRaceSize>(c =>
                        {
                            c.small_race_speed_bonus = componentData.AsInt("SmallRaceSpeedBonus");
                            c.normal_race_speed_bonus = componentData.AsInt("NormalRaceSpeedBonus");
                        })
                    ));

            _logger.Debug($"Adding delegate: ApplyMetamagicToPersonalSpell");
            _createComponentDelegates.Add("ApplyMetamagicToPersonalSpell",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<ApplyMetamagicToPersonalSpell>(c =>
                        {
                            c.caster_level_increase = componentData.Exists("CasterLevelIncrease")
                                ? componentData.AsInt("CasterLevelIncrease")
                                : 0;
                            if (componentData.Exists("Metamagic"))
                            {
                                c.metamagic = EnumParser.parseMetamagic(componentData.AsString("Metamagic"));
                            }
                        })
                    ));


            _logger.Debug($"Adding delegate: ConsiderUndeadForHealing");
            _createComponentDelegates.Add("ConsiderUndeadForHealing",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<ConsiderUndeadForHealing>()
                ));

            _logger.Debug($"Adding delegate: ContextIncreaseDescriptorSpellsDC");
            _createComponentDelegates.Add("ContextIncreaseDescriptorSpellsDC",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<ContextIncreaseDescriptorSpellsDC>(c =>
                        {
                            c.Value = componentData.AsInt("Value");
                            c.Descriptor = EnumParser.parseSpellDescriptor(componentData.AsString("Descriptor"));
                        })
                    ));

            _logger.Debug($"Adding delegate: IgnoreEncumbrence");
            _createComponentDelegates.Add("IgnoreEncumbrence",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<IgnoreEncumbrence>()
                ));

            _logger.Debug($"Adding delegate: ItemUseFailure");
            _createComponentDelegates.Add("ItemUseFailure",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<ItemUseFailure>(c =>
                    {
                        c.chance = componentData.AsInt("Chance");
                    })
                ));

            _logger.Debug($"Adding delegate: RunActionOnCombatStart");
            _createComponentDelegates.Add("RunActionOnCombatStart",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<RunActionOnCombatStart>(c =>
                        {
                            if (componentData.Exists("Actions"))
                            {
                                List<GameAction> actions = new List<GameAction>();
                                foreach (var action in componentData.AsList<JsonTypes.Action>("Actions"))
                                {
                                    actions.Add(ActionFromJson.CreateAction(action));
                                }

                                c.actions = new ActionList() {Actions = actions.ToArray()};
                            }
                        })
                    ));

            _logger.Debug($"Adding delegate: SaveAgainstHarmlessSpells");
            _createComponentDelegates.Add("SaveAgainstHarmlessSpells",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<SaveAgainstHarmlessSpells>()
                ));

            _logger.Debug($"Adding delegate: SetVisibilityLimit");
            _createComponentDelegates.Add("SetVisibilityLimit",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<SetVisibilityLimit>(c =>
                        {
                            c.visibility_limit = componentData.AsInt("VisibilityLimit").Feet();
                        })
                    ));

            _logger.Debug($"Adding delegate: Silence");
            _createComponentDelegates.Add("Silence",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<Silence>()));

            _logger.Debug($"Adding delegate: SpellFailureChance");
            _createComponentDelegates.Add("SpellFailureChance",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<SpellFailureChance>(c =>
                        {
                            c.chance = componentData.AsInt("Chance");
                            c.do_not_spend_slot_if_failed = componentData.Exists("DoNotSpendSlotIfFailed") && componentData.AsBool("DoNotSpendSlotIfFailed");
                            c.ignore_psychic = componentData.Exists("IgnorePsychic") && componentData.AsBool("IgnorePsychic");
                        })
                    ));

            _logger.Debug($"Adding delegate: SuppressBuffsCorrect");
            _createComponentDelegates.Add("SuppressBuffsCorrect",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<SuppressBuffsCorrect>(c =>
                        {
                            c.Descriptor = componentData.Exists("Descriptor")
                                ? EnumParser.parseSpellDescriptor(componentData.AsString("Descriptor"))
                                : SpellDescriptor.None;
                            c.Buffs = componentData.Exists("Buffs")
                                ? componentData.AsArray("Buffs")
                                    .Select(b => _buffRepository.GetBuff(_identifierLookup.lookupBuff(b)))
                                    .ToArray()
                                : Array.Empty<BlueprintBuff>();
                        })
                    ));

            _logger.Debug($"Adding delegate: WeaponsOnlyAttackBonus");
            _createComponentDelegates.Add("WeaponsOnlyAttackBonus",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<WeaponsOnlyAttackBonus>(c =>
                        {
                            c.Bonus = componentData.AsInt("Bonus");
                        })
                    ));
        }

        private static ContextRankConfig createContextRankConfig(Component componentData) =>
            createContextRankConfig(componentData, Array.Empty<BlueprintCharacterClass>());

        private static ContextRankConfig createContextRankConfig(Component componentData, BlueprintCharacterClass blueprintCharacterClass) =>
            createContextRankConfig(componentData, new [] {blueprintCharacterClass});

        private static ContextRankConfig createContextRankConfig(Component componentData, [NotNull] BlueprintCharacterClass[] blueprintCharacterClasses)
        {
            AbilityRankType type = componentData.Exists("Type")
                ? EnumParser.parseAbilityRankType(componentData.AsString("Type"))
                : AbilityRankType.Default;
            ContextRankBaseValueType baseValueType = componentData.Exists("BaseValueType")
                ? EnumParser.parseContextRankBaseValueType(componentData.AsString("BaseValueType"))
                : ContextRankBaseValueType.CasterLevel;
            ContextRankProgression progression = componentData.Exists("Progression")
                ? EnumParser.parseContextRankProgression(componentData.AsString("Progression"))
                : ContextRankProgression.AsIs;
            StatType stat = componentData.Exists("Stat")
                ? EnumParser.parseStatType(componentData.AsString("Stat"))
                : StatType.Unknown;

            int? min = componentData.Exists("Min")
                ? componentData.AsInt("Min")
                : (int?) null;
            int? max = componentData.Exists("Max")
                ? componentData.AsInt("Max")
                : (int?) null;;
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
