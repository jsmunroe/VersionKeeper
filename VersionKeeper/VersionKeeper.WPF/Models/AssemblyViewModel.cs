// using System:

using System.ComponentModel;
using VersionKeeper.Models;

namespace VersionKeeper.WPF.Models
{
    public class AssemblyViewModel : BaseViewModel
    {
        public AssemblyViewModel(ReferencedAssembly assembly)
        {
            ProjectName = assembly.Project.Name;
            BranchName = assembly.Branch.Name;
            AssemblyName = assembly.Name;
            AssemblyVersion = assembly.FileVersion?.ToString();
        }


        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        [DisplayName("Branch Name")]
        public string BranchName { get; set; }

        [DisplayName("Assembly Name")]
        public string AssemblyName { get; set; }

        [DisplayName("Assembly Version")]
        public string AssemblyVersion { get; set; }

    }
}