using System;
using System.Windows.Input;
using Microsoft.Win32;
using TogglTimeManager.Helpers;
using TogglTimeManager.Services;
using TogglTimeManager.Views;

namespace TogglTimeManager.ViewModels
{
    public class FileSelectionViewModel : BaseViewModel
    {
        private readonly IFilePicker _filePicker;
        private readonly IPageNavigationService _navigationService;
        //TODO: Implement filtering for csv only (https://www.wpf-tutorial.com/dialogs/the-openfiledialog/)

        public FileSelectionViewModel(IFilePicker filePicker, IPageNavigationService navigationService)
        {
            _filePicker = filePicker;
            _navigationService = navigationService;
        }

        #region Binded properties

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

        private ICommand _processCommand;
        public ICommand ProcessCommand => _processCommand ?? (_processCommand = new ButtonCommand(ProcessFile));

        #endregion

        private void PickFile()
        {
            var filePath = _filePicker.PickFile("Csv files (*.csv)|*.csv");
            if (!string.IsNullOrEmpty(filePath))
            {
                TextBox = filePath;
            }
        }

        private void ProcessFile()
        {
            _navigationService.Navigate(new MainDashboard());
        }
    }
}
