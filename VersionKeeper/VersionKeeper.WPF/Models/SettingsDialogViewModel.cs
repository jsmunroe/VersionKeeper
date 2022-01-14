using System.Windows;
using System.Windows.Input;
using VersionKeeper.WPF.Properties;

namespace VersionKeeper.WPF.Models
{
    public class SettingsDialogViewModel : BaseViewModel
    {
        private readonly Settings _settings;
        private string _workspaceRootPath;

        public string WorkspaceRootPath
        {
            get => _workspaceRootPath;
            set { _workspaceRootPath = value; RaisePropertyChanged(); }
        }

        public ICommand SaveCommand { get; }

        public SettingsDialogViewModel(Properties.Settings settings)
        {
            _settings = settings;
            WorkspaceRootPath = settings.WorkspaceRootPath;

            SaveCommand = RelayCommand.Bind(OnSaveCommandExecuted);
        }

        private void OnSaveCommandExecuted(object parameter)
        {
            _settings.WorkspaceRootPath = WorkspaceRootPath;
            _settings.Save();

            (parameter as Window)?.Close();

        }
    }
}