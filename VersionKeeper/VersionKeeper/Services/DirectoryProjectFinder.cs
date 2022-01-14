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

namespace VersionKeeper.Services
{
    public class DirectoryProjectFinder : IProjectFinder
    {
        public IEnumerable<Project> Find(string workspaceRootPath)
        {
            #region Argument Validation

            if (workspaceRootPath == null)
            {
                throw new ArgumentNullException(nameof(workspaceRootPath));
            }

            #endregion

            if (!Directory.Exists(workspaceRootPath))
            {
                return Enumerable.Empty<Project>();
            }

            var childDirectories = Directory.EnumerateDirectories(workspaceRootPath);

            return childDirectories.Select(CreateProject);
        }

        public IEnumerable<Project> Find(DirectoryInfo workspaceRoot)
        {
            return Find(workspaceRoot.FullName);
        }

        private Project CreateProject(string projectRootPath)
        {
            return new Project
            {
                Name = Path.GetFileName(projectRootPath),
                Root = new DirectoryInfo(projectRootPath),
            };
        }
    }
}
