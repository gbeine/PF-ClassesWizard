using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Kingmaker.Blueprints;

namespace PF_Core.Extensions.JaethalsMagic
{
    public class CloneComponents
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly Dictionary<string, Action<BlueprintScriptableObject, BlueprintScriptableObject>> Delegates =
            new Dictionary<string, Action<BlueprintScriptableObject, BlueprintScriptableObject>>();

        public static bool CanClone(string component) =>
            Delegates.ContainsKey(component);

        public static void Clone(string component, BlueprintScriptableObject target, BlueprintScriptableObject source) =>
            Delegates[component](target, source);

        static CloneComponents()
        {
            // See RemoveComponents for an explanation of this magic.
            _logger.Log("Jaethal's performing magic... adding clone component delegates");

            Type baseType = typeof(BlueprintComponent);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<Type> types = assembly.GetTypes().Where(t => t.IsSubclassOf(baseType));
                foreach (Type type in types)
                {
                    string typeName = Regex.Replace(type.ToString(), ".*\\.", "");

                    if (Delegates.ContainsKey(typeName))
                    {
                        _logger.Error($"Duplicate component name: {typeName} - not adding clone component for {type}");
                    }
                    else
                    {
                        _logger.Debug($"Add clone component delegate for {typeName}");
                        Delegates.Add(typeName, (target, source) =>
                            typeof(BlueprintScriptableObjectExtensions)
                                .GetMethod("CloneComponents")
                                .MakeGenericMethod(type)
                                .Invoke(target, new object[] { target, source }));
                    }
                }
            }
            // As I said: You can't understand this code without explanations.
            // Got to RemoveComponent and let me explain
        }
    }
}
