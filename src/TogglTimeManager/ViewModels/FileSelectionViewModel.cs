using System;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Win32;
using TogglTimeManager.Core;
using TogglTimeManager.Helpers;
using TogglTimeManager.Services;
using TogglTimeManager.Views;

namespace TogglTimeManager.ViewModels
{
    public class FileSelectionViewModel : BoundObject
    {
        private readonly IFilePicker _filePicker;
        private readonly IPageNavigationService _navigationService;

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

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (value == _errorMessage) return;
                _errorMessage = value;
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
                ProcessFile();
            }
        }

        private void ProcessFile()
        {
            try
            {
                var timeSheet = CsvProcessor.ProcessCsvFile(TextBox);
                if (!timeSheet.Period.HasValue)
                {
                    _navigationService.Navigate(new DateSelectionPage(timeSheet, _navigationService));
                    return;
                }

                //TODO: Use the window orchestrator class to request a new window, which is the dashboard window
                //_navigationService.Navigate(new MainDashboard());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }
    }
}
