using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PF_Classes.JsonTypes
{
    public class Spell : JsonType
    {
        public Spell(JObject jObject) : base(jObject)
        {
            DisplayName = SelectString(jObject, "DisplayName");
            Description = SelectString(jObject, "Description", DisplayName);
            Icon = SelectString(jObject, "Icon");
            From = SelectString(jObject, "From");

            Type = SelectString(jObject, "Type");
            ActionType = SelectString(jObject, "ActionType");
            Range = SelectString(jObject, "Range");
            Duration = SelectString(jObject, "Duration");
            SavingThrow = SelectString(jObject, "SavingThrow");
            SpellResistance = SelectBool(jObject, "SpellResistance");
            AddSpell = SelectBool(jObject, "AddSpell");
            AddScroll = SelectBool(jObject, "AddScroll");
            SpellResistance = SelectBool(jObject, "SpellResistance");
            CanTargetFriends = SelectBool(jObject, "CanTargetFriends");
            CanTargetEnemies = SelectBool(jObject, "CanTargetEnemies");
            CanTargetSelf = SelectBool(jObject, "CanTargetSelf");
            CanTargetPoint = SelectBool(jObject, "CanTargetPoint");
            EffectOnEnemy = SelectString(jObject, "EffectOnEnemy");
            EffectOnAlly = SelectString(jObject, "EffectOnAlly");
            Animation = SelectString(jObject, "Animation");
            AnimationStyle = SelectString(jObject, "AnimationStyle");

            AvailableMetamagic = SelectStringList(jObject, "AvailableMetamagic");

            SelectSpellLists(jObject);
            SelectReplaceComponents(jObject);
            SelectReplaceProgression(jObject);
        }

        private void SelectSpellLists(JObject jObject)
        {
            JToken jSpellLists = jObject.SelectToken("SpellLists");
            SpellLists = new Dictionary<string, int>();
            if (jSpellLists != null)
            {
                foreach (var entry in jSpellLists)
                {
                    string spellList = entry.SelectToken("SpellList", true).Value<string>();
                    int spellLevel = entry.SelectToken("Level", true).Value<int>();
                    SpellLists.Add(spellList, spellLevel);
                }
            }
        }


        private void SelectReplaceComponents(JObject jObject)
        {
            JToken jComponents = jObject.SelectToken("ReplaceComponents");
            ReplaceComponents = Array.Empty<Component>().ToList();
            if (jComponents != null)
            {
                ReplaceComponents = new List<Component>();
                foreach (var jComponent in jComponents.Value<JArray>())
                {
                    ReplaceComponents.Add(new Component(jComponent.Value<JObject>()));
                }
            }
        }

        private void SelectReplaceProgression(JObject jObject)
        {
            JToken jComponents = jObject.SelectToken("ReplaceProgession");
            ReplaceProgression = new Dictionary<string, int>();
            if (jComponents != null)
            {
                foreach (var jComponent in jComponents.Value<JArray>())
                {
                    ReplaceProgression.Add(
                        jComponent.SelectToken("Progression").Value<string>(),
                        jComponent.SelectToken("Level").Value<int>());
                }
            }
        }

        public string DisplayName { get; }
        public string Description { get; }
        public string Icon { get; }
        public string From { get; }
        public string Type { get; }
        public string ActionType { get; }
        public string Range { get; }
        public string Duration { get; }
        public string SavingThrow { get; }
        public string EffectOnEnemy { get; }
        public string EffectOnAlly { get; }
        public string Animation { get; }
        public string AnimationStyle { get; }
        public bool? SpellResistance { get; }
        public bool? AddSpell { get; }
        public bool? AddScroll { get; }
        public bool? CanTargetFriends { get; }
        public bool? CanTargetEnemies { get; }
        public bool? CanTargetSelf { get; }
        public bool? CanTargetPoint { get; }
        public List<string> AvailableMetamagic { get; }
        public Dictionary<string,int> SpellLists { get; private set; }
        public List<Component> ReplaceComponents { get; private set; }
        public Dictionary<string, int> ReplaceProgression { get; private set; }
    }
}
