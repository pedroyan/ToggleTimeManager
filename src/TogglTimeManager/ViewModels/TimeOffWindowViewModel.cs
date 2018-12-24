using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TogglTimeManager.Core.Models;
using TogglTimeManager.Helpers;

namespace TogglTimeManager.ViewModels
{
    public class TimeOffWindowViewModel : ObservableObject
    {
        private DateRange? _range;

        public DateRange? Range
        {
            get => _range;
            set
            {
                if (value == _range) return;
                _range = value;
                OnPropertyChanged();
            }
        }

        private ICommand _changeRangeCommand;
        public ICommand ChangeRangeCommand => _changeRangeCommand ?? (_changeRangeCommand = new ButtonCommand(ChangeRange));

        private int counter = 0;
        private void ChangeRange()
        {
            counter++;
            Range = new DateRange(DateTime.Today, DateTime.Today.AddDays(counter));
        }
    }
}
