using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Buffs.Blueprints;
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
using PF_Core.Factories;

namespace PF_Classes.Transformations.ComponentDelegates
{
    public class CallOfTheWildComponentDelegates : JsonTransformation
    {
        private static readonly ComponentFactory _componentFactory = ComponentFactory.INSTANCE;

        internal static readonly Dictionary<String, Action<BlueprintScriptableObject, Component>> CreateComponentDelegates =
            new Dictionary<string, Action<BlueprintScriptableObject, Component>>();

        static CallOfTheWildComponentDelegates()
        {
            _logger.Debug($"Adding delegate: AddOutgoingConcealment");
            CreateComponentDelegates.Add("AddOutgoingConcealment",
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
            CreateComponentDelegates.Add("AddSpeedBonusBasedOnRaceSize",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<AddSpeedBonusBasedOnRaceSize>(c =>
                        {
                            c.small_race_speed_bonus = componentData.AsInt("SmallRaceSpeedBonus");
                            c.normal_race_speed_bonus = componentData.AsInt("NormalRaceSpeedBonus");
                        })
                    ));


            _logger.Debug($"Adding delegate: ApplyMetamagicToPersonalSpell");
            CreateComponentDelegates.Add("ApplyMetamagicToPersonalSpell",
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
            CreateComponentDelegates.Add("ConsiderUndeadForHealing",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<ConsiderUndeadForHealing>()
                ));


            _logger.Debug($"Adding delegate: ContextIncreaseDescriptorSpellsDC");
            CreateComponentDelegates.Add("ContextIncreaseDescriptorSpellsDC",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<ContextIncreaseDescriptorSpellsDC>(c =>
                        {
                            c.Value = componentData.AsInt("Value");
                            c.Descriptor = EnumParser.parseSpellDescriptor(componentData.AsString("Descriptor"));
                        })
                    ));


            _logger.Debug($"Adding delegate: IgnoreEncumbrence");
            CreateComponentDelegates.Add("IgnoreEncumbrence",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<IgnoreEncumbrence>()
                ));


            _logger.Debug($"Adding delegate: ItemUseFailure");
            CreateComponentDelegates.Add("ItemUseFailure",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<ItemUseFailure>(c =>
                    {
                        c.chance = componentData.AsInt("Chance");
                    })
                ));


            _logger.Debug($"Adding delegate: RunActionOnCombatStart");
            CreateComponentDelegates.Add("RunActionOnCombatStart",
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
            CreateComponentDelegates.Add("SaveAgainstHarmlessSpells",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<SaveAgainstHarmlessSpells>()
                ));


            _logger.Debug($"Adding delegate: SetVisibilityLimit");
            CreateComponentDelegates.Add("SetVisibilityLimit",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<SetVisibilityLimit>(c =>
                        {
                            c.visibility_limit = componentData.AsInt("VisibilityLimit").Feet();
                        })
                    ));


            _logger.Debug($"Adding delegate: Silence");
            CreateComponentDelegates.Add("Silence",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<Silence>()));


            _logger.Debug($"Adding delegate: SpellFailureChance");
            CreateComponentDelegates.Add("SpellFailureChance",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<SpellFailureChance>(c =>
                        {
                            c.chance = componentData.AsInt("Chance");
                            c.do_not_spend_slot_if_failed = componentData.Exists("DoNotSpendSlotIfFailed") && componentData.AsBool("DoNotSpendSlotIfFailed");
                            c.ignore_psychic = componentData.Exists("IgnorePsychic") && componentData.AsBool("IgnorePsychic");
                        })
                    ));


            _logger.Debug($"Adding delegate: SuppressBuffsCorrect");
            CreateComponentDelegates.Add("SuppressBuffsCorrect",
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
            CreateComponentDelegates.Add("WeaponsOnlyAttackBonus",
                (target, componentData) => target.AddComponent(
                    _componentFactory.CreateComponent<WeaponsOnlyAttackBonus>(c =>
                        {
                            c.Bonus = componentData.AsInt("Bonus");
                        })
                    ));

        }
    }
}
