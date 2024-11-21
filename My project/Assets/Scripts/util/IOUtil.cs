using System.IO;

namespace util
{
    public class IOUtil
    {
        public static void CheckPath(string path)
        {
            if (!Directory.Exists(path))
            {
                Logger.Log("PathUtil.CheckPath : Create directory");
                Directory.CreateDirectory(path);
            }
        }
    }
}