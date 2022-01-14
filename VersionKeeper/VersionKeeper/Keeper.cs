using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VersionKeeper.Contracts;
using VersionKeeper.Contracts.Services;
using VersionKeeper.Models;

namespace VersionKeeper
{
    public class Keeper
    {
        private readonly IProjectFinder _projectFinder;
        private readonly IBranchFinder _branchFinder;
        private readonly IAssemblyFinder _assemblyFinder;

        public DirectoryInfo WorkspaceRoot { get; set; }


        public IEnumerable<Project> Projects { get; private set; }

        public Keeper(string workspaceRootPath, IProjectFinder projectFinder, IBranchFinder branchFinder, IAssemblyFinder assemblyFinder)
        {
            _projectFinder = projectFinder;
            _branchFinder = branchFinder;
            _assemblyFinder = assemblyFinder;

            WorkspaceRoot = new DirectoryInfo(workspaceRootPath);
        }

        public void Refresh()
        {
            Projects = _projectFinder.Find(WorkspaceRoot)
                .Select(RefreshProject)
                .Where(p => p.Branches.Any())
                .OrderBy(p => p.Name)
                .ToArray();
        }

        private Project RefreshProject(Project project)
        {
            project.Branches = _branchFinder.Find(project)
                .Select(RefershBranch)
                .ToArray();

            return project;
        }

        private Branch RefershBranch(Branch branch)
        {
            branch.ReferencedAssemblies = _assemblyFinder.Find(branch)
                .ToArray();

            return branch;
        }

        public async Task<IEnumerable<ReferencedAssembly>> GetAllAssemblies(AssemblySearchFilter filter, IProgressNotificationTarget progressTarget)
        {
            var assemblies = FilterProjects(Projects, filter)
                .SelectMany(p => FilterBranches(p.Branches, filter))
                .SelectMany(p => FilterAssemblies(p.ReferencedAssemblies, filter))
                .ToArray();

            progressTarget.Start("Loading file meta-data. One moment please...", assemblies.Length);

            await ProcessRunner.Run(processRunner =>
            {
                foreach (var assembly in assemblies)
                {
                    assembly.LoadMetaData();

                    processRunner.Progress.Increment();
                }

                processRunner.Progress.Close();
            }, progressTarget);

            return assemblies;
        }

        private IEnumerable<Project> FilterProjects(IEnumerable<Project> projects, AssemblySearchFilter filter)
        {
            if (filter.ProjectName == null)
                return projects;

            return projects.Where(p => p.Name.IndexOf(filter.ProjectName, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private IEnumerable<Branch> FilterBranches(IEnumerable<Branch> branches, AssemblySearchFilter filter)
        {
            if (filter.BranchName == null)
                return branches;

            return branches.Where(p => p.Name.IndexOf(filter.BranchName, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private IEnumerable<ReferencedAssembly> FilterAssemblies(IEnumerable<ReferencedAssembly> assemblies, AssemblySearchFilter filter)
        {
            if (filter.AssemblyName == null)
                return assemblies;

            return assemblies.Where(p => p.Name.IndexOf(filter.AssemblyName, StringComparison.OrdinalIgnoreCase) >= 0);
        }


    }
}
