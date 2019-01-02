using Identity_Common.Interfaces.Loggers;
using System;
using System.IO;

namespace Identity_Common.Loggers
{
    public class FileLogger : IPathLogger
    {
        public void Log(string msg, string path)
        {
            string dir =Path.GetDirectoryName(path);
            if (dir!=string.Empty&&!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (StreamWriter file = new StreamWriter(path, true))
            {
                file.WriteLineAsync(DateTime.Now + ": " + msg);
            }
        }

        public void Log(string msg)
        {
            throw new NotImplementedException();
        }
    }
}
