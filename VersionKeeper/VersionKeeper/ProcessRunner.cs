// using System:

using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using VersionKeeper.Contracts.Services;

namespace VersionKeeper
{
    public class ProcessRunner
    {
        private readonly Dispatcher _updateDispatcher = Dispatcher.CurrentDispatcher;

        private readonly Action<ProcessRunner> _processAction;

        public ProcessRunner(Action<ProcessRunner> processAction, IProgressNotificationTarget progressNotification = null)
        {
            #region Argument Validation

            if (processAction == null)
            {
                throw new ArgumentNullException(nameof(processAction));
            }

            #endregion

            _processAction = processAction;

            if (progressNotification != null)
            {
                Progress = new ProcessRunnerProgressNotificationTarget(progressNotification, _updateDispatcher);
            }
        }


        public IProgressNotificationTarget Progress { get; set; }
        public void Update(Action updateAction)
        {
            _updateDispatcher.Invoke(updateAction);
        }

        public Task Run()
        {
            return Task.Run(() => _processAction(this));
        }

        public static Task Run(Action<ProcessRunner> processAction, IProgressNotificationTarget progressNotification = null)
        {
            var processRunner = new ProcessRunner(processAction, progressNotification);

            return processRunner.Run();
        }
    }

    class ProcessRunnerProgressNotificationTarget : IProgressNotificationTarget
    {
        private readonly IProgressNotificationTarget _updateTarget;
        private readonly Dispatcher _updateDispatcher;

        public ProcessRunnerProgressNotificationTarget(IProgressNotificationTarget updateTarget, Dispatcher _updateDispatcher)
        {
            _updateTarget = updateTarget;
            this._updateDispatcher = _updateDispatcher;
        }

        public void Start(string initialMessage, int total)
        {
            _updateDispatcher.Invoke(() => _updateTarget.Start(initialMessage, total));
        }

        public void Update(string newMessage, int current)
        {
            _updateDispatcher.Invoke(() => _updateTarget.Update(newMessage, current));
        }

        public void Update(int current)
        {
            _updateDispatcher.Invoke(() => _updateTarget.Update(current));
        }

        public void Increment()
        {
            _updateDispatcher.Invoke(() => _updateTarget.Increment());
        }

        public void Close()
        {
            _updateDispatcher.Invoke(() => _updateTarget.Close());
        }
    }
}