using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using VersionKeeper.WPF.Utilities;

namespace VersionKeeper.WPF.Models
{
    public class SearchAssembliesViewModel : BaseViewModel
    {
        private readonly Keeper _keeper;

        private string _projectName;
        private string _branchName;
        private string _assemblyName;

        private ObservableCollection<string> _projectNames;
        private ObservableCollection<string> _branchNames;
        private ObservableCollection<string> _assemblyNames;

        private ObservableCollection<AssemblyViewModel> _searchResults = new ObservableCollection<AssemblyViewModel>();

        public SearchAssembliesViewModel(Keeper keeper)
        {
            #region Argument Validation

            if (keeper == null)
            {
                throw new ArgumentNullException(nameof(keeper));
            }

            #endregion

            _keeper = keeper;

            Init();

            PropertyChangeRelay.Bind(this, i => i.ProjectName, OnSearchFiltersChanged);
            PropertyChangeRelay.Bind(this, i => i.BranchName, OnSearchFiltersChanged);
            PropertyChangeRelay.Bind(this, i => i.AssemblyName, OnSearchFiltersChanged);

            SearchCommand = RelayCommand.Bind(OnSearchCommandExecuted);
        }

        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; RaisePropertyChanged(); }
        }

        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; RaisePropertyChanged(); }
        }

        public string AssemblyName
        {
            get { return _assemblyName; }
            set { _assemblyName = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<string> ProjectNames
        {
            get { return _projectNames; }
            set { _projectNames = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<string> BranchNames
        {
            get { return _branchNames; }
            set { _branchNames = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<string> AssemblyNames
        {
            get { return _assemblyNames; }
            set { _assemblyNames = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<AssemblyViewModel> SearchResults
        {
            get { return _searchResults; }
            set { _searchResults = value; RaisePropertyChanged(); }
        }

        public ProgressTargetViewModel ProgressTarget { get; set; } = new ProgressTargetViewModel();

        public ICommand SearchCommand { get; private set; }


        private async void Init()
        {
            SearchResults.Clear();

            var assemblies = await _keeper.GetAllAssemblies(new AssemblySearchFilter(), ProgressTarget);

            ProjectNames = new ObservableCollection<string>(assemblies.Select(p => p.Project.Name).Distinct().OrderBySelf());
            BranchNames = new ObservableCollection<string>(assemblies.Select(p => p.Branch.Name).Distinct().OrderBySelf());
            AssemblyNames = new ObservableCollection<string>(assemblies.Select(p => p.Name).Distinct().OrderBySelf());

            SearchResults = new ObservableCollection<AssemblyViewModel>(assemblies.Select(p => new AssemblyViewModel(p)));
        }

        private void Clear()
        {
            SearchResults.Clear();
        }

        private async void Search()
        {
            SearchResults.Clear();

            var assemblies = await _keeper.GetAllAssemblies(new AssemblySearchFilter
            {
                ProjectName = ProjectName,
                BranchName = BranchName,
                AssemblyName = AssemblyName
            }, ProgressTarget);

            SearchResults = new ObservableCollection<AssemblyViewModel>(assemblies.Select(p => new AssemblyViewModel(p)));
        }

        private void OnSearchCommandExecuted(object parameter)
        {
            Search();
        }
        private void OnSearchFiltersChanged()
        {
            Clear();
        }
    }
}
