using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace VersionKeeper.Models
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class Branch
    {
        public Project Project { get; set; }

        public string Name { get; set; }

        public int Ordinal { get; set; }

        public DirectoryInfo Root { get; set; }

        public FileInfo VersionFile { get; set; }

        public Version Version { get; set; }

        public IEnumerable<ReferencedAssembly> ReferencedAssemblies { get; set; }
    }
}