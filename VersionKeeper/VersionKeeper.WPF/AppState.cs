using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using VersionKeeper.Contracts;
using VersionKeeper.Contracts.Services;
using VersionKeeper.WPF.Messages;
using VersionKeeper.WPF.Models;
using VersionKeeper.WPF.Properties;

namespace VersionKeeper.WPF
{
    public static class AppState
    {
        private static Keeper _keeper;
        private static MainWindowViewModel _mainWindow;
        private static SettingsDialogViewModel _settingsDialog;
        private static SearchAssembliesViewModel _searchAssemblies;

        private static object _messengerTarget = new object();
        private static Settings _settings;

        public static MainWindowViewModel MainWindow
        {
            get { return _mainWindow ?? BuildMainWindow(); }
        }

        public static SettingsDialogViewModel SettingsDialog
        {
            get { return _settingsDialog ?? BuildSettings(); }
        }

        public static SearchAssembliesViewModel SearchAssemblies
        {
            get { return _searchAssemblies ?? BuildSearchAssemblies(); }
        }

        public static IMessenger Messenger { get; private set; }

        public static void Bootstrap(Properties.Settings settings, IUnityContainer container)
        {
            _settings = settings;

            Messenger = container.Resolve<IMessenger>();
            Messenger.Subscribe<RefreshMessage>(_messengerTarget, OnRefreshMessageRecieved);

            var projectFinder = container.Resolve<IProjectFinder>();
            var branchFinder = container.Resolve<IBranchFinder>();
            var assemblyFinder = container.Resolve<IAssemblyFinder>();

            _keeper = new Keeper(settings.WorkspaceRootPath, projectFinder, branchFinder, assemblyFinder);
            _keeper.Refresh();
        }

        public static void ShowSettingsDialog()
        {
            var dialog = new SettingsDialog
            {
                DataContext = SettingsDialog
            };

            dialog.ShowDialog();

            MainWindow.Refresh(SettingsDialog.WorkspaceRootPath);
        }

        public static void ShowSearchAssembliesWindow()
        {
            var window = new SearchAssembliesWindow
            {
                DataContext = SearchAssemblies
            };

            window.Show();
        }

        private static MainWindowViewModel BuildMainWindow()
        {
            _mainWindow = new MainWindowViewModel(_keeper);
            return _mainWindow;
        }

        private static SettingsDialogViewModel BuildSettings()
        {
            _settingsDialog = new SettingsDialogViewModel(_settings);
            return _settingsDialog;
        }

        private static SearchAssembliesViewModel BuildSearchAssemblies()
        {
            _searchAssemblies = new SearchAssembliesViewModel(_keeper);
            return _searchAssemblies;
        }

        private static void OnRefreshMessageRecieved(RefreshMessage message)
        {
            _mainWindow.Refresh();
        }
    }
}
