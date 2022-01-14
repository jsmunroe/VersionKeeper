using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersionKeeper.Models;

namespace VersionKeeper.WPF.Models
{
    public class BranchViewModel : BaseViewModel
    {
        private readonly Keeper _keeper;
        private readonly Branch _branch;

        public string Name => _branch.Name;

        public Version Version => _branch.Version;


        public BranchViewModel(Keeper keeper, Branch branch)
        {
            _keeper = keeper;
            _branch = branch;
        }


    }
}
