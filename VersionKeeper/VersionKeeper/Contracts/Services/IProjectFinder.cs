using System.Collections.Generic;
using System.IO;
using VersionKeeper.Models;

namespace VersionKeeper.Contracts.Services
{
    public interface IProjectFinder
    {
        IEnumerable<Project> Find(string workspaceRootPath);
        IEnumerable<Project> Find(DirectoryInfo workspaceRoot);
    }
}