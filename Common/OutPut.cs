using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class OutPut
    {
        private static object falg = new object();

        public static void WriteFile(string content)
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd-HH") + ".txt";
            WriteFile(fileName, content);
        }

        public static void WriteFile(string fileName, string content)
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");

                if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(fileName))
                    return;

                path = path.Replace("/", @"\");
                if (!path.EndsWith(@"\"))
                    path += @"\";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                fileName = Path.Combine(path, fileName);

                lock (OutPut.falg)
                {
                    File.AppendAllText(fileName, content, Encoding.Default);
                }
            }
            catch
            {
            }
        }
    }

}
