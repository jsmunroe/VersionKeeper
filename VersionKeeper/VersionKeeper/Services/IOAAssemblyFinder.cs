using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class IoaAssemblyFinder : IAssemblyFinder
    {
        public IEnumerable<ReferencedAssembly> Find(Branch branch)
        {
            #region Argument Validation

            if (branch == null)
            {
                throw new ArgumentNullException(nameof(branch));
            }

            #endregion

            return GetAssemblyFiles(branch.Root.Combine("ReferencedAssemblies"))
                .Select(p => CreateReferencedAssembly(p, branch))
                .WhereNotNull();
        }

        private ReferencedAssembly CreateReferencedAssembly(FileInfo file, Branch branch)
        {
            if (!file.Exists)
            {
                return null;
            }

            return new ReferencedAssembly
            {
                Project = branch.Project,
                Branch = branch,
                Name = file.Name,
                RelativePath = branch.Root.ToRelative(file),
                AssemblyFile = file,
                FileVersion = null,
            };
        }

        private IEnumerable<FileInfo> GetAssemblyFiles(DirectoryInfo directory)
        {
            var files = new List<FileInfo>();

            foreach (var subdirectory in directory.EnumerateDirectories())
            {
                files.AddRange(GetAssemblyFiles(subdirectory));
            }

            files.AddRange(directory.EnumerateFiles("*.dll"));

            return files;
        }
    }
}
