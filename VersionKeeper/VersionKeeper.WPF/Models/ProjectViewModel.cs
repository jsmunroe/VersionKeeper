using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersionKeeper.Models;

namespace VersionKeeper.WPF.Models
{
    public class ProjectViewModel : BaseViewModel
    {
        private readonly Keeper _keeper;
        private readonly Project _project;

        public string Name => _project.Name;

        public ObservableCollection<BranchViewModel> Branches { get; private set; }

        public ProjectViewModel(Keeper keeper, Project project)
        {
            _keeper = keeper;
            _project = project;

            Branches = project.Branches
                .OrderBy(p => p.Ordinal)
                .Select(p => new BranchViewModel(keeper, p))
                .ToObservableCollection();
        }

    }
}
