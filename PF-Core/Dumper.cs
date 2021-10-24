using System.IO;
using System.Reflection;
using Kingmaker.Blueprints;

namespace PF_Core
{
    public class Dumper
    {
        private static string m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static void Dump(LibraryScriptableObject library)
        {
        }
    }
}
