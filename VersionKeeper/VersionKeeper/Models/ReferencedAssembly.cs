// using System:

using System;
using System.Diagnostics;
using System.IO;

namespace VersionKeeper.Models
{
    public class ReferencedAssembly
    {
        public Project Project { get; set; }

        public Branch Branch { get; set; }

        public string Name { get; set; }

        public string RelativePath { get; set; }

        public FileInfo AssemblyFile { get; set; }

        public Version FileVersion { get; set; }

        public void LoadMetaData()
        {
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(AssemblyFile.FullName);

            if (Version.TryParse(fileVersionInfo.FileVersion, out var fileVersion))
                FileVersion = fileVersion;            
        }
    }
}