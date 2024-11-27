using System;
using System.IO;
using UnityEngine;

namespace util
{
    public class IOUtil
    {
        public static void CheckPath(string path)
        {
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception e)
                {
                    Logger.Error("Create Directory Failed, " + e.Message);
                    throw;
                }
            }
        }

        public static bool CheckFile(string path)
        {
            if (!File.Exists(path) && isPathValid(path))
            {
                try
                {
                    CheckPath(Path.GetDirectoryName(path));
                    File.Create(path).Close();
                }
                catch (Exception e)
                {
                    Logger.Error("Create File Failed, " + e.Message);
                    return false;
                }
            }

            return true;
        }

        public static void WriteContentToFile(string path, string content)
        {
            try
            {
                if (CheckFile(path))
                {
                    using var stream = File.OpenWrite(path);
                    stream.SetLength(0);
                    using (var write = new StreamWriter(stream))
                    {
                        write.Write(content);
                        write.Close();
                    }
                    stream.Close();
                }
            }
            catch (IOException ex)
            {
                Logger.Error($"File access conflicts: {ex.Message}");
            }
            catch (Exception ex)
            {
                Logger.Error($"There was an error writing to the file: {ex.Message}");
            }
        }

        public static bool isPathValid(string path)
        {
            try
            {
                Path.GetFullPath(path);
            }
            catch (Exception e)
            {
                Logger.Warn(path + " is not valid, " + e.Message);
                return false;
            }
            return true;
        }
    }
}