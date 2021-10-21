using System;
using System.Collections.Generic;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using PF_Classes.JsonTypes;
using PF_Classes.Transformations.ComponentDelegates;
using PF_Core.Extensions;
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
            else if (KingmakerComponentDelegates.CreateComponentDelegates.ContainsKey(componentData.Type))
            {
                KingmakerComponentDelegates.CreateComponentDelegates[componentData.Type](target, componentData);
            }
            else if (CallOfTheWildComponentDelegates.CreateComponentDelegates.ContainsKey(componentData.Type))
            {
                CallOfTheWildComponentDelegates.CreateComponentDelegates[componentData.Type](target, componentData);
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

            if (KingmakerComponentDelegates.CreateComponentDelegates.ContainsKey(componentData.Type))
            {
                KingmakerComponentDelegates.CreateComponentDelegates[componentData.Type](target, componentData);
            }
            else if (CallOfTheWildComponentDelegates.CreateComponentDelegates.ContainsKey(componentData.Type))
            {
                CallOfTheWildComponentDelegates.CreateComponentDelegates[componentData.Type](target, componentData);
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

            _logger.Debug($"Adding delegate: BindAbilitiesToClass");
            _createComponentClassDelegates.Add("BindAbilitiesToClass",
                (target, componentData, characterClass) => target.AddComponent(
                    _componentFactory.CreateComponent<BindAbilitiesToClass>(c =>
                        {
                            c.CharacterClass = characterClass;

                            if (componentData.Exists("Spellbook"))
                            {
                                BlueprintSpellbook spellbook =
                                    _spellbookRepository.GetSpellbook(
                                        _identifierLookup.lookupSpellbook(componentData.AsString("Spellbook")));

                                c.Abilites = spellbook.SpellList.SpellsByLevel[0].Spells.ToArray();
                                c.Stat = spellbook.CastingAttribute;
                            }
                            else
                            {
                                if (characterClass.Spellbook != null)
                                    c.Stat = characterClass.Spellbook.CastingAttribute;
                            }

                            if (componentData.Exists("LevelStep"))
                                c.LevelStep = componentData.AsInt("LevelStep");
                            if (componentData.Exists("Cantrip"))
                                c.Cantrip = componentData.AsBool("Cantrip");

                            c.Archetypes = Array.Empty<BlueprintArchetype>(); // TODO: implement
                            c.AdditionalClasses = Array.Empty<BlueprintCharacterClass>(); // TODO: implement
                        })
                    ));

            _logger.Debug($"Adding delegate: ContextRankConfig");
            _createComponentClassDelegates.Add("ContextRankConfig",
                (target, componentData, characterClass) => target.AddComponent(createContextRankConfig(componentData, characterClass)));

            _logger.Debug($"Adding delegate: LearnSpells");
            _createComponentClassDelegates.Add("LearnSpells",
                (target, componentData, characterClass) => target.AddComponent(
                    _componentFactory.CreateComponent<LearnSpells>(c =>
                        {
                            c.CharacterClass = characterClass;

                            if (componentData.Exists("Spellbook"))
                            {
                                BlueprintSpellbook spellbook =
                                    _spellbookRepository.GetSpellbook(
                                        _identifierLookup.lookupSpellbook(componentData.AsString("Spellbook")));
                                c.Spells = spellbook.SpellList.SpellsByLevel[0].Spells.ToArray();
                            }
                            else
                            {
                                if (characterClass.Spellbook != null)
                                    c.Spells = characterClass.Spellbook.SpellList.SpellsByLevel[0].Spells.ToArray();
                                else
                                    _logger.Error("Invalid LearnSpells created: no spellbook defined and no spellbook exists for character class.");
                            }
                        })
                    ));
        }
        private static ContextRankConfig createContextRankConfig(Component componentData, BlueprintCharacterClass blueprintCharacterClass) =>
            KingmakerComponentDelegates.createContextRankConfig(componentData, new [] {blueprintCharacterClass});

    }
}
