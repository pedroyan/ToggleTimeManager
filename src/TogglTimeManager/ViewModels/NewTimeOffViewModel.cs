using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TogglTimeManager.Core.Models;
using TogglTimeManager.Helpers;
using TogglTimeManager.Services;

namespace TogglTimeManager.ViewModels
{
    public class NewTimeOffViewModel : ObservableObject
    {
        private readonly INotificationService _notificationService;
        public event EventHandler<TimeOff> TimeOffCreated;
        protected virtual void OnTimeOffCreated(TimeOff e)
        {
            TimeOffCreated?.Invoke(this, e);
        }

        public NewTimeOffViewModel(INotificationService notificationService)
        {
            Period = new DateRange(DateTime.Today, DateTime.Today);
            _notificationService = notificationService;
        }

        private DateRange _period;
        public DateRange Period
        {
            get => _period;
            set
            {
                if (value == _period) return;
                _period = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        private ICommand _createTimeOffCommand;
        public ICommand CreateTimeOffCommand => _createTimeOffCommand ?? (_createTimeOffCommand = new ButtonCommand(CreateTimeOff));

        private void CreateTimeOff()
        {
            if (string.IsNullOrEmpty(Description))
            {
                _notificationService.DisplayAlert("Description cannot be empty");
                return;
            }

            OnTimeOffCreated(new TimeOff()
            {
                Description = Description,
                Period = Period
            });
        }
    }
}
