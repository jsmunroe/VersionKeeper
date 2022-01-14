using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;
using VersionKeeper.Contracts;
using VersionKeeper.Contracts.Services;
using VersionKeeper.Services;

namespace VersionKeeper.WPF.App_Start
{
    public static class ContainerConfig
    {
        public static void Bootstrap(IUnityContainer container)
        {
            container.RegisterType<IMessenger, Messenger>(new ContainerControlledLifetimeManager())
                .RegisterType<IProjectFinder, DirectoryProjectFinder>()
                .RegisterType<IBranchFinder, IoaBranchFinder>()
                .RegisterType<IAssemblyFinder, IoaAssemblyFinder>()
                .RegisterType<IVersionReader, AssemblyVersionReader>();
        }
    }}
