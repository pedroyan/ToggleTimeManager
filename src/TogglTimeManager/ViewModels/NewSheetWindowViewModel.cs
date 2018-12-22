using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TogglTimeManager.Core.Models;
using TogglTimeManager.Helpers;
using TogglTimeManager.Services;
using TogglTimeManager.Views;

namespace TogglTimeManager.ViewModels
{
    /// <summary>
    /// View model that serves as aggregate root to the sheet parsing process. This class will be responsible for
    /// handling the navigation between the child vms and also handle the communication with the components outside
    /// the bounded context
    /// </summary>
    public class NewSheetWindowViewModel
    {
        private readonly IPageNavigationService _navigationService;
        private TimeSheet _timeSheet;

        public FileSelectionViewModel FileSelectionViewModel { get; }
        public DateRangeSelectionViewModel DateSelectionViewModel { get; private set; }

        public event EventHandler<TimeSheet> TimeSheetCompleted;
        protected virtual void OnTimeSheetCompleted(TimeSheet e)
        {
            TimeSheetCompleted?.Invoke(this, e);
        }


        public NewSheetWindowViewModel(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;

            FileSelectionViewModel = new FileSelectionViewModel(IoC.Resolve<IFilePicker>());
            FileSelectionViewModel.TimeSheetParsed += OnSheetParsed;

            _navigationService.Navigate(new FileSelectionPage(FileSelectionViewModel));
        }

        private void OnSheetParsed(object sender, TimeSheet e)
        {
            _timeSheet = e;
            if (e.Period == null)
            {
                SelectDate(e);
                return;
            }

            OnTimeSheetCompleted(_timeSheet);
        }

        private void SelectDate(TimeSheet e)
        {
            var goBackCommand = new ButtonCommand(_navigationService.GoBack);

            DateSelectionViewModel = new DateRangeSelectionViewModel(e, goBackCommand);
            DateSelectionViewModel.OnDateSelected += (s, ea) =>
            {
                _timeSheet.Period = ea;
                OnTimeSheetCompleted(_timeSheet);
            };

            _navigationService.Navigate(new DateSelectionPage(DateSelectionViewModel));
        }
    }
}
