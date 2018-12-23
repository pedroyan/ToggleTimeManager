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
    public class TimeSheetCompletedEventArgs
    {
        public TimeSheetCompletedEventArgs(TimeSpan workContract, DateRange? dateRange = null)
        {
            WorkContract = workContract;
            DateRange = dateRange;
        }

        public DateRange? DateRange { get; }
        public TimeSpan WorkContract { get; }
    }

    /// <summary>
    /// View model responsible for handling the date range selection
    /// </summary>
    public class CompleteInformationViewModel : BoundObject
    {
        private readonly TimeSheet _timeSheet;

        #region Events

        public event EventHandler<TimeSheetCompletedEventArgs> TimeSheetCompleted;
        protected virtual void OnTimeSheetCompleted(TimeSheetCompletedEventArgs args)
        {
            TimeSheetCompleted?.Invoke(this, args);
        }

        #endregion

        public CompleteInformationViewModel(TimeSheet timeSheet, ICommand goBack)
        {
            _timeSheet = timeSheet;
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

        public bool ShowPeriod => !_timeSheet.Period.HasValue;

        private string _periodErrorMessage;
        public string PeriodErrorMessage
        {
            get => _periodErrorMessage;
            set
            {
                if (value == _periodErrorMessage) return;
                _periodErrorMessage = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _workDuration;
        public TimeSpan WorkDuration
        {
            get => _workDuration;
            set
            {
                if (value == _workDuration) return;
                _workDuration = value;
                OnPropertyChanged();
            }
        }

        private string _durationErrorMessage;
        public string DurationErrorMessage
        {
            get => _durationErrorMessage;
            set
            {
                if (value == _durationErrorMessage) return;
                _durationErrorMessage = value;
                OnPropertyChanged();
            }
        }

        private ICommand _nextCommand;
        public ICommand NextCommand => _nextCommand ?? (_nextCommand = new ButtonCommand(Next));

        public ICommand GoBackCommand { get; }

        #endregion

        private void Next()
        {
            DateRange? range = null;
            if (_timeSheet.Period == null)
            {
                if (!ValidateDates())
                {
                    return;
                }
                range = new DateRange(From, To);
            }

            if (WorkDuration <= TimeSpan.Zero)
            {
                DurationErrorMessage = "Please enter a valid contract time";
                return;
            }



            OnTimeSheetCompleted(new TimeSheetCompletedEventArgs(WorkDuration, range));
        }

        private bool ValidateDates()
        {
            if (From > To)
            {
                PeriodErrorMessage = "Invalid range. Please ensure that the end date comes after the initial date";
                return false;
            }

            return true;
        }
    }
}
