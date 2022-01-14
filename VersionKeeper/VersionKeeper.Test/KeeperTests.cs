using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using VersionKeeper.Services;

namespace VersionKeeper.Test
{
    [TestClass]
    public class KeeperTests
    {

        [TestMethod]
        public void Refresh()
        {
            // Setup
            var versionReader = new AssemblyVersionReader();
            var projectFinder = new DirectoryProjectFinder();
            var branchFinder = new IoaBranchFinder(versionReader);
            var assemblyFinder = new IoaAssemblyFinder();

            var keeper = new Keeper(@"C:\TFS\IOA", projectFinder, branchFinder, assemblyFinder);

            // Execute
            keeper.Refresh();

            // Assert
            Assert.IsTrue(keeper.Projects.Any());
        }

    }
}
