using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VersionKeeper.Models;

namespace VersionKeeper.WPF.Models
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly Keeper _keeper;

        public MainWindowViewModel(Keeper keeper)
        {
            #region Argument Validation

            if (keeper == null)
            {
                throw new ArgumentNullException(nameof(_keeper));
            }

            #endregion

            _keeper = keeper;

            Projects = new ObservableCollection<Project>(_keeper.Projects);

            RefreshCommand = RelayCommand.Bind(OnRefreshCommandExecuted);
            GoToSettingsCommand = RelayCommand.Bind(OnGoToSettingsCommandExecuted);
            SearchAssemblies = RelayCommand.Bind(OnSearchReferencedAssemblies);
        }

        public ObservableCollection<Project> Projects { get; set; }

        public ICommand RefreshCommand { get; }
        public ICommand GoToSettingsCommand { get; }
        public ICommand SearchAssemblies { get; }

        public void Refresh()
        {
            _keeper.Refresh();
            Projects.Clear();

            foreach (var project in _keeper.Projects)
            {
                Projects.Add(project);
            }
        }

        public void Refresh(string workspaceRootPath)
        {
            _keeper.WorkspaceRoot = new DirectoryInfo(workspaceRootPath);

            Refresh();
        }

        private void OnRefreshCommandExecuted(object parameter)
        {
            Refresh();
        }

        private void OnGoToSettingsCommandExecuted(object parameter)
        {
            AppState.ShowSettingsDialog();
        }

        private void OnSearchReferencedAssemblies(object parameter)
        {
            AppState.ShowSearchAssembliesWindow();
        }

    }
}
