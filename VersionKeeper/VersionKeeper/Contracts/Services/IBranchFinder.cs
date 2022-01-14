using System.Collections.Generic;
using System.IO;
using VersionKeeper.Models;

namespace VersionKeeper.Contracts.Services
{
    public interface IBranchFinder
    {
        IEnumerable<Branch> Find(Project project);
    }
}