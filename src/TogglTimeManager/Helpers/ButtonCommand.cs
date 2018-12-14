using System;
using System.Windows.Input;
using TogglTimeManager.Properties;

namespace TogglTimeManager.Helpers
{
    /// <inheritdoc />
    /// <summary>
    /// Generic button command
    /// </summary>
    /// <remarks>Inspired on https://www.codeproject.com/Articles/819294/WPF-MVVM-step-by-step-Basics-to-Advance-Level</remarks>
    public class ButtonCommand : ICommand
    {
        private readonly Action _whatToExecute;
        private readonly Func<bool> _canExecute;
        
        /// <summary>
        /// Creates an instance of a button command
        /// </summary>
        /// <param name="whatToExecute">The action to be executed by the command</param>
        /// <param name="canExecute">The validation function to verify if the command can be executed</param>
        public ButtonCommand([NotNull] Action whatToExecute, Func<bool> canExecute = null)
        {
            _whatToExecute = whatToExecute ?? throw new ArgumentNullException(nameof(whatToExecute));
            _canExecute = canExecute;
        }

        /// <inheritdoc />
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        /// <inheritdoc />
        public void Execute(object parameter)
        {
            _whatToExecute();
        }

        public event EventHandler CanExecuteChanged;
    }
}
