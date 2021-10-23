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
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates.AddDelegates;
using PF_Core.Extensions;
using PF_Core.Facades;
using PF_Core.Factories;

namespace PF_Classes.Transformations.ComponentDelegates
{
    public class KingmakerCreateComponentDelegates : JsonTransformation
    {
        private static readonly ComponentFactory _componentFactory = ComponentFactory.INSTANCE;

        private static readonly Dictionary<string, Action<BlueprintScriptableObject, Component>> CreateComponentDelegates =
            new Dictionary<string, Action<BlueprintScriptableObject, Component>>();

        public static bool CanAdd(string component) =>
            CreateComponentDelegates.ContainsKey(component);

        public static void Add(Component component, BlueprintScriptableObject target) =>
            CreateComponentDelegates[component.Type](target, component);

        static KingmakerCreateComponentDelegates()
        {

            _logger.Debug($"Adding delegate: AbilityAreaEffectRunAction");
            CreateComponentDelegates.Add("AbilityAreaEffectRunAction",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AbilityAreaEffectRunAction>(c =>
                        {
                            if (componentData.Exists("UnitEnter"))
                            {
                                IEnumerable<GameAction> actions = componentData.AsList<JsonTypes.Action>("UnitEnter")
                                    .Select(a => ActionFromJson.CreateAction(a));

                                c.UnitEnter = new ActionList() { Actions = actions.ToArray() };
                            }
                            if (componentData.Exists("UnitExit"))
                            {
                                IEnumerable<GameAction> actions = componentData.AsList<JsonTypes.Action>("UnitExit")
                                    .Select(a => ActionFromJson.CreateAction(a));

                                c.UnitExit = new ActionList() { Actions = actions.ToArray() };
                            }
                            if (componentData.Exists("UnitMove"))
                            {
                                IEnumerable<GameAction> actions = componentData.AsList<JsonTypes.Action>("UnitMove")
                                    .Select(a => ActionFromJson.CreateAction(a));

                                c.UnitMove = new ActionList() { Actions = actions.ToArray() };
                            }
                            if (componentData.Exists("Round"))
                            {
                                IEnumerable<GameAction> actions = componentData.AsList<JsonTypes.Action>("Round")
                                    .Select(a => ActionFromJson.CreateAction(a));

                                c.Round = new ActionList() { Actions = actions.ToArray() };
                            }
                        })
                    ));

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

            _logger.Debug($"Adding delegate: AbilitySpawnFx");
            CreateComponentDelegates.Add("AbilitySpawnFx",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AbilitySpawnFx>(c =>
                        {
                            if (componentData.Exists("Link"))
                                c.PrefabLink = new PrefabLink()
                                    {AssetId = _identifierLookup.lookupBuff(componentData.AsString("Link"))};

                            c.PositionAnchor = componentData.Exists("PositionAnchor")
                                ? EnumParser.parseAbilitySpawnFxAnchor(componentData.AsString("PositionAnchor"))
                                : AbilitySpawnFxAnchor.None;
                            c.OrientationAnchor = componentData.Exists("OrientationAnchor")
                                ? EnumParser.parseAbilitySpawnFxAnchor(componentData.AsString("OrientationAnchor"))
                                : AbilitySpawnFxAnchor.None;
                            c.Anchor = componentData.Exists("Anchor")
                                ? EnumParser.parseAbilitySpawnFxAnchor(componentData.AsString("Anchor"))
                                : AbilitySpawnFxAnchor.None;
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

            _logger.Debug($"Adding delegate: AddAreaEffect");
            CreateComponentDelegates.Add("AddAreaEffect",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddAreaEffect>(c =>
                        {
                            c.AreaEffect = _areaEffectRepository.GetAreaEffect(
                                _identifierLookup.lookupAbilityAreaEffect(componentData.AsString("AreaEffect")));
                        })
                    ));

            _logger.Debug($"Adding delegate: AddCondition");
            CreateComponentDelegates.Add("AddCondition",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddCondition>(c =>
                        {
                            c.Condition = EnumParser.parseUnitCondition(componentData.AsString("Condition"));
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

            _logger.Debug($"Adding delegate: AuraFeatureComponent");
            CreateComponentDelegates.Add("AuraFeatureComponent",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AuraFeatureComponent>(c =>
                        {
                            c.Buff = _buffRepository.GetBuff(
                                _identifierLookup.lookupBuff(componentData.AsString("Buff")));
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

            _logger.Debug($"Adding delegate: SpellDescriptorComponent");
            CreateComponentDelegates.Add("SpellDescriptorComponent",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<SpellDescriptorComponent>(c =>
                        {
                            SpellDescriptor spellDescriptor = SpellDescriptor.None;
                            foreach (var descriptor in componentData.AsArray("Descriptor"))
                            {
                                spellDescriptor |= EnumParser.parseSpellDescriptor(descriptor);
                            }

                            c.Descriptor = spellDescriptor;
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

        internal static ContextRankConfig createContextRankConfig(JsonTypes.Component componentData) =>
            ContextRankConfigDelegate.CreateComponent(componentData);
    }
}