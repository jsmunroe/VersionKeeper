using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersionKeeper.Contracts.Services
{
    public interface IProgressNotificationTarget
    {
        void Start(string initialMessage, int total);
        void Update(string newMessage, int current);
        void Update(int current);
        void Increment();
        void Close();
    }
}
