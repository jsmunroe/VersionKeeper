// using System:

using VersionKeeper.Contracts.Services;

namespace VersionKeeper.WPF.Models
{
    public class ProgressTargetViewModel : BaseViewModel, IProgressNotificationTarget
    {
        private string _message;
        private int _maximum;
        private int _value;
        private bool _isVisible;

        public string Message
        {
            get { return _message; }
            set { _message = value; RaisePropertyChanged(); }
        }

        public int Maximum
        {
            get { return _maximum; }
            set { _maximum = value; RaisePropertyChanged(); }
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; RaisePropertyChanged(); }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; RaisePropertyChanged(); }
        }

        public void Start(string initialMessage, int total)
        {
            Message = initialMessage;

            Maximum = total;
            Value = 0;

            IsVisible = true;
        }

        public void Update(string newMessage, int current)
        {
            Message = newMessage;

            Value = current;
        }

        public void Update(int current)
        {
            Value = current;
        }

        public void Increment()
        {
            Value++;
        }

        public void Close()
        {
            IsVisible = false;
        }
    }
}