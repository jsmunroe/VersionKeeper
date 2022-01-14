using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersionKeeper.Contracts;
using VersionKeeper.Contracts.Services;
using VersionKeeper.Extensions;
using VersionKeeper.Models;

namespace VersionKeeper
{
    public class IoaBranchFinder : IBranchFinder
    {
        private readonly IVersionReader _versionReader;
        const string versionRelative = "Common\\AssemblyVersion.cs";

        public IoaBranchFinder(IVersionReader versionReader)
        {
            _versionReader = versionReader;
        }

        public IEnumerable<Branch> Find(Project project)
        {
            #region Argument Validation

            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            #endregion

            if (!project.Root.Exists)
            {
                return Enumerable.Empty<Branch>();
            }

            var relatives = new []
            {
                new Relative { Ordinal=1, Path="Dev\\Dev1" },
                new Relative { Ordinal=2, Path="Main" },
                new Relative { Ordinal=1, Path="DevCurrent\\DevMain" },
                new Relative { Ordinal=0, Path="DevNext\\DevMain" },
                new Relative { Ordinal=3, Path="Release\\Hotfix" }
            };

            return relatives.Select(p => CreateBranch(project, p))
                .WhereNotNull()
                .OrderBy(p => p.Ordinal);
        }


        private Branch CreateBranch(Project project, Relative relative)
        {
            var branchRoot = project.Root.Combine(relative.Path);

            if (!branchRoot.Exists)
            {
                return null;
            }

            var versionFile = branchRoot.File(versionRelative);

            if (!versionFile.Exists)
            {
                return null;
            }

            return new Branch
            {
                Project = project,
                Name = relative.Path,
                Ordinal = relative.Ordinal,
                Root = branchRoot,
                VersionFile = versionFile,
                Version = _versionReader.Read(versionFile),
            };
        }

        private class Relative
        {
            public Int32 Ordinal { get; set; }

            public string Path { get; set; }
        }
    }
}
