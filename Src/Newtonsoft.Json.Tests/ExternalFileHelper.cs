using System.IO;
using System.Reflection;

namespace Newtonsoft.Json.Tests
{
    public static class ExternalFileHelper
    {
        public static string GetFilePath(string filename)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename);
        }
    }
}
