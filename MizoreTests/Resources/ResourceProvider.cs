using System.IO;
using System.Reflection;

namespace MizoreTests.Resources
{
    internal static class ResourceProvider
    {
        internal static Assembly CurrentAssembly { get { return Assembly.GetExecutingAssembly(); } }

        internal static Stream GetResourceStream(string resourceName)
        {
            return CurrentAssembly.GetManifestResourceStream(resourceName);
        }
    }
}