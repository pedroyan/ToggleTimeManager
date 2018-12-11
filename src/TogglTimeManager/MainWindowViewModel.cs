using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TogglTimeManager.Annotations;
using Microsoft.Win32;

namespace TogglTimeManager
{
    public class MainWindowViewModel : BaseViewModel
    {
        //TODO: Implement filtering for csv only (https://www.wpf-tutorial.com/dialogs/the-openfiledialog/)
        //TODO: Extract the file picking functionality to its own service interface

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

        private ICommand _pickFileCommand;
        public ICommand PickFileCommand => _pickFileCommand ?? (_pickFileCommand = new ButtonCommand(PickFile));

        private void PickFile()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                TextBox = openFileDialog.FileName;
            }
            else
            {
                Console.WriteLine($"Did not return true :( {openFileDialog.FileName}");
            }
        }
    }
}
