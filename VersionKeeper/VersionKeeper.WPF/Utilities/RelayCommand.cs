using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VersionKeeper.WPF
{
    public class RelayCommand : ICommand
    {
        public Action<object> _execute;
        public Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        protected RelayCommand()
        { }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute"/> is null.</exception>
        public static RelayCommand Bind(Action<object> execute, Func<object, bool> canExecute = null)
        {
            #region Argument Validation

            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            #endregion

            return new RelayCommand
            {
                _execute = execute,
                _canExecute = canExecute,
            };
        }

    }
}
