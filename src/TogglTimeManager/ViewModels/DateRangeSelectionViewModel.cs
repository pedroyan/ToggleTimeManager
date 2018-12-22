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
    /// <summary>
    /// View model responsible for handling the date range selection
    /// </summary>
    public class DateRangeSelectionViewModel : BoundObject
    {

        #region Events

        public event EventHandler<DateRange> OnDateSelected;
        protected virtual void OnOnDateSelected(DateRange e)
        {
            OnDateSelected?.Invoke(this, e);
        }

        #endregion

        public DateRangeSelectionViewModel(TimeSheet timeSheet, ICommand goBack)
        {
            From = DateTime.Today.Date;
            To = From;

            //Lets keep the navigation responsibility with the Aggregate root for now
            GoBackCommand = goBack;
        }

        #region Bindable properties

        private DateTime _from;
        public DateTime From
        {
            get => _from;
            set
            {
                if (value == _from) return;
                _from = value;
                OnPropertyChanged();
            }
        }

        private DateTime _to;
        public DateTime To
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

        public ICommand GoBackCommand { get;  }

        #endregion

        private void Next()
        {
            if (!ValidateDates())
            {
                return;
            }

            OnOnDateSelected(new DateRange(From, To));
        }

        private bool ValidateDates()
        {
            if (From > To)
            {
                ErrorMessage = "Invalid range. Please ensure that the end date comes after the initial date";
                return false;
            }

            return true;
        }


    }
}
