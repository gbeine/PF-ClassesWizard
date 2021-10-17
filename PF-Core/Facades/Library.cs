using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using PF_Core.Extensions;
using UnityEngine;

namespace PF_Core.Facades
{
    public class Library
    {
        private static Library __instance;

        private LibraryScriptableObject _library;

        public Library(LibraryScriptableObject lsoLibraryScriptableObject)
        {
            this._library = lsoLibraryScriptableObject;
            __instance = this;
        }

        public static Library INSTANCE
        {
            get { return __instance; }
        }

        internal List<BlueprintCharacterClass> GetCharacterClasses()
        {
            // For some reason, Eldritch Scion is a class and an archetype.
            const String eldritchScionClassId = "f5b8c63b141b2f44cbb8c2d7579c34f5";

            return _library.Root.Progression.CharacterClasses
                .Where(
                    c => c.AssetGuid != eldritchScionClassId)
                .ToList();
        }

        internal List<BlueprintArchetype> GetArchtetypes() => _library.GetAll<BlueprintArchetype>();
        internal List<BlueprintFeature> GetFeatures() => _library.GetAll<BlueprintFeature>();
        internal List<BlueprintAbility> GetAbilities() => _library.GetAll<BlueprintAbility>();
        internal List<BlueprintStatProgression> GetStatProgressions() => _library.GetAll<BlueprintStatProgression>();

        internal BlueprintCharacterClass GetCharacterClass(String assetId) => Get<BlueprintCharacterClass>(assetId);
        internal BlueprintArchetype GetArchetype(String assetId) => Get<BlueprintArchetype>(assetId);
        internal BlueprintFeature GetFeature(String assetId) => Get<BlueprintFeature>(assetId);
        internal BlueprintSpellbook GetSpellbook(String assetId) => Get<BlueprintSpellbook>(assetId);
        internal BlueprintAbility GetAbility(String assetId) => Get<BlueprintAbility>(assetId);
        internal BlueprintStatProgression GetStatProgression(String assetId) => Get<BlueprintStatProgression>(assetId);
        internal BlueprintBuff GetBuff(String assetId) => Get<BlueprintBuff>(assetId);

        internal T Get<T>(String assetId) where T : BlueprintScriptableObject
        {
            return _library.Get<T>(assetId);
        }

        internal T Create<T>() where T : ScriptableObject
        {
            return ScriptableObject.CreateInstance<T>();
        }

        internal T Create<T>(Action<T> init) where T : ScriptableObject
        {
            T scriptableObject = Create<T>();
            init(scriptableObject);
            return scriptableObject;
        }

        internal void Add(BlueprintScriptableObject blueprintScriptableObject)
        {
            if (!_library.Exists<BlueprintScriptableObject>(blueprintScriptableObject.AssetGuid))
            {
                _library.AddAsset(blueprintScriptableObject);
            }
        }

        public void RegisterCharacterClass(BlueprintCharacterClass blueprintCharacterClass)
        {
            List<BlueprintCharacterClass> classes = _library.Root.Progression.CharacterClasses.ToList();
            classes.Add(blueprintCharacterClass);
            classes.Sort((x, y) =>
            {
                if (x.PrestigeClass != y.PrestigeClass) return x.PrestigeClass ? 1 : -1;
                return x.Name.CompareTo(y.Name);
            });
            _library.Root.Progression.CharacterClasses = classes.ToArray();
        }
    }
}
