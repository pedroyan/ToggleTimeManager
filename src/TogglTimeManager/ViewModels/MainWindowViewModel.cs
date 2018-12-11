using System;
using System.Windows.Input;
using Microsoft.Win32;
using TogglTimeManager.Services;

namespace TogglTimeManager.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IFilePicker _filePicker;
        //TODO: Implement filtering for csv only (https://www.wpf-tutorial.com/dialogs/the-openfiledialog/)

        public MainWindowViewModel(IFilePicker filePicker)
        {
            _filePicker = filePicker;
        }

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
            var filePath = _filePicker.PickFile();
            if (!string.IsNullOrEmpty(filePath))
            {
                TextBox = filePath;
            }
        }
    }
}
