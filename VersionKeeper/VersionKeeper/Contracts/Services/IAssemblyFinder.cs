using System.Collections.Generic;
using System.IO;
using VersionKeeper.Models;

namespace VersionKeeper.Contracts.Services
{
    public interface IAssemblyFinder
    {
        IEnumerable<ReferencedAssembly> Find(Branch branch);
    }
}