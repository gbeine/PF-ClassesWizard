using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Kingmaker.Blueprints;
using Newtonsoft.Json;

namespace PF_Core
{
    public class SaveCompatibility
    {
        private static readonly Logger _logger = Logger.INSTANCE;
        private static readonly SaveCompatibility __instance = new SaveCompatibility();
        private static readonly Dictionary<Type, List<FieldInfo>> _statefulComponentFields = new Dictionary<Type, List<FieldInfo>>();
        private static readonly StringBuilder _statefulComponentMessage = new StringBuilder();

        private SaveCompatibility() { }

        public static SaveCompatibility INSTANCE
        {
            get { return __instance;  }
        }
        
        internal void CheckComponent(BlueprintScriptableObject blueprintScriptableObject, BlueprintComponent blueprintComponent)
        {
            var type = blueprintComponent.GetType();
            if (IsStatefulComponent(type))
            {
                _statefulComponentMessage.AppendLine($"Warning: in object {blueprintScriptableObject.name}, stateful {type.Name} should be named.");
            }
        } 
        private bool IsStatefulComponent(Type type) => GetStatefulComponentFields(type).Count > 0;
        private List<FieldInfo> GetStatefulComponentFields(Type type)
        {
            List<FieldInfo> fields;
            if (_statefulComponentFields.TryGetValue(type, out fields)) return fields;

            fields = new List<FieldInfo>();
            for (var t = type; t != null; t = t.BaseType)
            {
                fields.AddRange(t.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(
                    f => f.CustomAttributes.Any(a => a.AttributeType == typeof(JsonPropertyAttribute) ||
                                                     a.AttributeType == typeof(SerializableAttribute))));
            }
            _statefulComponentFields.Add(type, fields);
            return fields;
        }

    }
}
