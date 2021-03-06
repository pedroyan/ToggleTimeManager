﻿using System;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Win32;
using TogglTimeManager.Core;
using TogglTimeManager.Core.Models;
using TogglTimeManager.Helpers;
using TogglTimeManager.Services;
using TogglTimeManager.Views;

namespace TogglTimeManager.ViewModels
{
    /// <summary>
    /// View model responsible for handling the file selection
    /// </summary>
    public class FileSelectionViewModel : ObservableObject
    {
        private readonly IFilePicker _filePicker;

        #region Events

        /// <summary>
        /// Raised whenever a time sheet is successfully parsed
        /// </summary>
        public event EventHandler<TimeSheet> TimeSheetParsed;
        protected virtual void OnTimeSheetParsed(TimeSheet e)
        {
            TimeSheetParsed?.Invoke(this, e);
        }

        #endregion

        public FileSelectionViewModel(IFilePicker filePicker)
        {
            _filePicker = filePicker;
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
        public ICommand ProcessCommand => _processCommand ?? (_processCommand = new ButtonCommand(() => ProcessFile(TextBox)));

        #endregion

        private void PickFile()
        {
            var filePath = _filePicker.PickFile("Csv files (*.csv)|*.csv");
            if (!string.IsNullOrEmpty(filePath))
            {
                TextBox = filePath;
                ProcessFile(TextBox);
            }
        }

        private void ProcessFile(string textBox)
        {
            try
            {
                var timeSheet = CsvParser.ParseCsvFile(TextBox);
                OnTimeSheetParsed(timeSheet);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
