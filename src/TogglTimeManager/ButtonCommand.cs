using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TogglTimeManager
{
    public class ButtonCommand : ICommand
    {
        private readonly MainWindowViewModel _mainViewModel;

        public ButtonCommand(MainWindowViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _mainViewModel.TextBox = "MVVM Rules!";
        }

        public event EventHandler CanExecuteChanged;
    }
}
