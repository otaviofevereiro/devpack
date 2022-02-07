using System.IO;
using System.Reflection;
using System.Text;

namespace System
{
    public class Embedded
    {
        protected Embedded()
        {
        }

        public static MemoryStream Read(string resourceName)
        {
            if (resourceName == null)
                throw new ArgumentNullException(nameof(resourceName));

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.IsDynamic)
                    continue;

                using var sr = assembly.GetManifestResourceStream(resourceName);

                if (sr is null)
                    continue;

                MemoryStream ms = new ();
                sr.CopyTo(ms);
                ms.Position = 0;

                return ms;
            }

            throw new FileNotFoundException($"Could not find the embedded resource '{ resourceName }'.", resourceName);
        }

        public static byte[] ReadBytes(string resourceName)
        {
            using var memoryStream = Read(resourceName);

            return memoryStream.ToArray();
        }

        public static string ReadSpecificLine(string fileName, int linePosition)
        {
            using StreamReader sr = new(Read(fileName));
            int currentLine = 0;

            while (!sr.EndOfStream)
            {
                currentLine++;
                var line = sr.ReadLine();

                if (linePosition == currentLine)
                    return line;
            }

            return string.Empty;
        }
        public static string ReadString(string resourceName, Encoding encoding)
        {
            return encoding.GetString(ReadBytes(resourceName));
        }

        public static string ReadString(string resourceName)
        {
            return ReadString(resourceName, Encoding.UTF8);
        }
    }
}
