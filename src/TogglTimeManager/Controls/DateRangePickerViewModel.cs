using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TogglTimeManager.Core.Models;
using TogglTimeManager.ViewModels;

namespace TogglTimeManager.Controls
{
    public class DatePickerViewModel : ObservableObject
    {
        #region Events

        public event EventHandler<DateRange?> DateRangeChanged;
        protected virtual void OnDateRangeChanged(DateRange? e)
        {
            DateRangeChanged?.Invoke(this, e);
        }

        #endregion

        private DateTime _from;
        public DateTime From
        {
            get => _from;
            set
            {
                if (value == _from) return;
                _from = value;
                OnPropertyChanged();
                RaiseDateRangeChanged();
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
                RaiseDateRangeChanged();
            }
        }

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
        private bool ValidateDates()
        {
            if (From > To)
            {
                PeriodErrorMessage = "Invalid range. Please ensure that the end date comes after the initial date";
                return false;
            }

            return true;
        }

        private void RaiseDateRangeChanged()
        {
            PeriodErrorMessage = "";
            if (!ValidateDates())
            {
                OnDateRangeChanged(null);
                return;
            }

            OnDateRangeChanged(new DateRange(From, To));
        }
    }
    
}
