using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TogglTimeManager.Annotations;

namespace TogglTimeManager
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _textBox;
        public string TextBox
        {
            get => _textBox;
            set
            {
                if (value == _textBox) return;
                _textBox = value;
                OnPropertyChanged();
            }
        }

        private ICommand _buttonCommand;
        public ICommand ButtonCommand => _buttonCommand ?? (_buttonCommand = new ButtonCommand(ButtonClicked));

        private void ButtonClicked()
        {
            TextBox = "MVVM Rules";
        }
    }
}
