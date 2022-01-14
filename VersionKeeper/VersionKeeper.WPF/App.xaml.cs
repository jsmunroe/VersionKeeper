using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using CommonServiceLocator;
using Unity;
using Unity.ServiceLocation;
using VersionKeeper.WPF.App_Start;
using VersionKeeper.WPF.Properties;

namespace VersionKeeper.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Bootstrap();

            base.OnStartup(e);
        }

        private static void Bootstrap()
        {
            var container = new UnityContainer();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));

            ContainerConfig.Bootstrap(container);

            var settings = new Settings();

            AppState.Bootstrap(settings, container);
        }
    }
}
