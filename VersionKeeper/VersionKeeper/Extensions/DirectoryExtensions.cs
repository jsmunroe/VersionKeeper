using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersionKeeper.Extensions
{
    public static class DirectoryExtensions
    {
        public static DirectoryInfo Combine(this DirectoryInfo directory, string subdirectory)
        {
            var path = Path.Combine(directory.FullName, subdirectory);

            return new DirectoryInfo(path);
        }

        public static FileInfo File(this DirectoryInfo directory, string fileName)
        {
            var path = Path.Combine(directory.FullName, fileName);

            return new FileInfo(path);
        }

        public static string ToRelative(this DirectoryInfo directory, FileSystemInfo element)
        {
            return element.FullName.Substring(directory.FullName.TrimEnd('\\').TrimEnd('/').Length + 1);
        }
    }
}
