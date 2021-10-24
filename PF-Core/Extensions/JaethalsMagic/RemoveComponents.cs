using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Kingmaker.Blueprints;

namespace PF_Core.Extensions.JaethalsMagic
{
    public class RemoveComponents
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly Dictionary<string, Action<BlueprintScriptableObject>> Delegates = new Dictionary<string, Action<BlueprintScriptableObject>>();

        public static bool CanRemove(string component) => Delegates.ContainsKey(component);

        public static void Remove(string component, BlueprintScriptableObject target) =>
            Delegates[component](target);

        //
        // What happens here is just as evil and cold as Jaethal
        //
        // But it's a direct way to enable removing of components from BlueprintScriptableObjects withou
        // adding all types line by line - what is unfortunately necessary for adding the components.
        //
        static RemoveComponents()
        {
            // Let's do Jaethals perform some Necromancy magic
            _logger.Log("Jaethal's performing magic... adding remove component delegates");

            // This is what we look for... subtypes of BlueprintComponent
            Type baseType = typeof(BlueprintComponent);

            // We have to search in all assemblies around here...
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // And iterate over all declared types that are a subclass of BlueprintComponent
                IEnumerable<Type> types = assembly.GetTypes().Where(t => t.IsSubclassOf(baseType));
                foreach (Type type in types)
                {
                    // Shorten the name of the component, so that we don't need to know the whole namespace
                    string typeName = Regex.Replace(type.ToString(), ".*\\.", "");

                    // Test if there are any duplicates... and if so we're nice (as Jaethal would be) and print a warning
                    if (Delegates.ContainsKey(typeName))
                    {
                        _logger.Error($"Duplicate component name: {typeName} - not adding remove component for {type}");
                    }
                    else
                    {
                        _logger.Debug($"Add remove component delegate for {typeName}");
                        // This is the trickiest part...
                        // there is a generic method in BlueprintScriptableObjectExtensions:
                        // public static void RemoveComponents<T>(this BlueprintScriptableObject blueprintScriptableObject) where T : BlueprintComponent
                        // We take this method and create an instance with the current type
                        // This instance need to be executed on the target object (the BlueprintScriptableObject)
                        // AND get the same object as the first parameter as declared (it would be the "this")
                        Delegates.Add(typeName, target =>
                            typeof(BlueprintScriptableObjectExtensions)
                                .GetMethod("RemoveComponents")
                                .MakeGenericMethod(type)
                                .Invoke(target, new object[] { target }));
                    }
                }
            }
            // When you read until this point, you may notice that I'm much nicer than Jaethal.
            // I explained the magic ;-)
        }
    }
}
