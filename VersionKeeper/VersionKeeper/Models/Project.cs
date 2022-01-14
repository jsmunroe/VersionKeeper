using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace VersionKeeper.Models
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class Project
    {
        public string Name { get; set; }

        public DirectoryInfo Root { get; set; }

        public IEnumerable<Branch> Branches { get; set; }

    }
}