using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VersionKeeper.Contracts.Services;

namespace VersionKeeper.Services
{
    public class AssemblyVersionReader : IVersionReader
    {
        private static readonly Regex _assemblyVersionRex = new Regex(@"AssemblyVersion\(""(?<version>[\d\.]*)""\)", RegexOptions.Compiled);

        public Version Read(string versionFilePath)
        {
            #region Argument Validation

            if (versionFilePath == null)
            {
                throw new ArgumentNullException(nameof(versionFilePath));
            }

            #endregion

            if (!File.Exists(versionFilePath))
            {
                return null;
            }

            var fileText = File.ReadAllText(versionFilePath);

            var match = _assemblyVersionRex.Match(fileText);

            if (!match.Success)
            {
                return null;
            }

            if (Version.TryParse(match.Groups["version"].Value, out var version))
            {
                return version;
            }

            return null;
        }

        public Version Read(FileInfo versionFile)
        {
            return Read(versionFile?.FullName);
        }
    }
}
