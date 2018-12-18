using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TogglTimeManager.Core.Models;
using TogglTimeManager.Helpers;
using TogglTimeManager.Properties;
using TogglTimeManager.Services;

namespace TogglTimeManager.ViewModels
{
    public class DateSelectionViewModel : BoundObject
    {
        private readonly TimeSheet _timeSheet;
        private readonly IPageNavigationService _navigationService;

        public DateSelectionViewModel(TimeSheet timeSheet, IPageNavigationService navigationService)
        {
            _timeSheet = timeSheet;
            _navigationService = navigationService;
        }

        #region Bindable properties

        private DateTime? _from;
        public DateTime? From
        {
            get => _from;
            set
            {
                if (value == _from) return;
                _from = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _to;
        public DateTime? To
        {
            get => _to;
            set
            {
                if (value == _to) return;
                _to = value;
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

        private ICommand _nextCommand;
        public ICommand NextCommand => _nextCommand ?? (_nextCommand = new ButtonCommand(Next));

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new ButtonCommand(GoBack));


        #endregion

        private void Next()
        {

        }

        private void GoBack()
        {
            _navigationService.GoBack();
        }
    }
}
