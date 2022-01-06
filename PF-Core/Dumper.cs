using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Kingmaker.Blueprints;
using PF_Core.Extensions;

namespace PF_Core
{
    public class Dumper
    {
        private static string m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static void Dump(LibraryScriptableObject library)
        {
            // DumpBlueprintUnitFact(library);
            // DumpComponents();
        }

        public static void DumpBlueprintUnitFact(LibraryScriptableObject library)
        {
            StreamWriter file = File.AppendText(m_exePath + "/" + "BlueprintUnitFacts.dump.txt");
            foreach (var b in library.GetAllBlueprints()
                .Where(bso => bso.GetType().IsSubclassOf(typeof(Kingmaker.Blueprints.Facts.BlueprintUnitFact)))
                .Select(bso => (Kingmaker.Blueprints.Facts.BlueprintUnitFact)bso))
            {
                string message = $"{b.GetType()} public static const string {b.name} = \"{b.AssetGuid}\"; // N:{b.Name}, D:{b.Description}, C:{b.Comment}";
                file.WriteLine(message);
                file.Flush();
            }
        }

        public static void DumpComponents()
        {
            StreamWriter file = File.AppendText(m_exePath + "/" + "BlueprintComponent.types.txt");
            var baseType = typeof(BlueprintComponent);
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in loadedAssemblies)
            {
                var types = assembly.GetTypes().Where(t => t.IsSubclassOf(baseType));
                foreach (var t in types)
                {
                    file.WriteLine(t);
                    file.Flush();
                }
            }
        }
    }
}
