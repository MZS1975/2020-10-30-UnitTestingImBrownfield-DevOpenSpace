namespace ConferenceDude.UI.Commands
{
    using System;
    using System.Windows.Input;

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _command;
        private readonly Func<object, bool>? _executionGuard;

        public RelayCommand(Action<object> command, Func<object, bool>? executionGuard = null)
        {
            _command = command;
            _executionGuard = executionGuard;
        }

        public bool CanExecute(object parameter)
        {
            return _executionGuard == null || _executionGuard(parameter);
        }

        public void Execute(object parameter)
        {
            _command(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
