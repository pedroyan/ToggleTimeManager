using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TogglTimeManager.Core.Models;
using TogglTimeManager.Helpers;
using TogglTimeManager.Services;
using TogglTimeManager.Views;
using TogglTimeManager.Views.NewSheet;

namespace TogglTimeManager.ViewModels
{
    

    /// <summary>
    /// View model that serves as aggregate root to the sheet parsing process. This class will be responsible for
    /// handling the navigation between the child vms and also handle the communication with the components outside
    /// the bounded context
    /// </summary>
    public class NewSheetWindowViewModel
    {
        private IPageNavigationService _navigationService;

        public FileSelectionViewModel FileSelectionViewModel { get; }
        public CompleteInformationViewModel DateSelectionViewModel { get; private set; }

        #region Events
        public class TimeSheetCreatedEventArgs
        {
            public TimeSheetCreatedEventArgs(TimeSheet timeSheet, TimeSpan workContract)
            {
                TimeSheet = timeSheet;
                WorkContract = workContract;
            }

            public TimeSheet TimeSheet { get; }
            public TimeSpan WorkContract { get; }
        }
        /// <summary>
        /// Raised when the workflow of this window is completed and the <see cref="TimeSheet"/> is completely filled
        /// </summary>
        public event EventHandler<TimeSheetCreatedEventArgs> TimeSheetCreated;
        protected virtual void OnTimeSheetCreated(TimeSheetCreatedEventArgs e)
        {
            TimeSheetCreated?.Invoke(this, e);
        }

        #endregion



        public NewSheetWindowViewModel()
        {
            FileSelectionViewModel = new FileSelectionViewModel(IoC.Resolve<IFilePicker>());
            FileSelectionViewModel.TimeSheetParsed += OnSheetParsed;
        }

        /// <summary>
        /// Displays the file selection window, beginning the selection flow
        /// </summary>
        /// <param name="navigationService">The navigation service instance</param>
        public void DisplayFileSelection(IPageNavigationService navigationService)
        {
            _navigationService = navigationService;
            navigationService.Navigate(new FileSelectionPage(FileSelectionViewModel));
        }

        private void OnSheetParsed(object sender, TimeSheet timeSheet)
        {
            var goBackCommand = new ButtonCommand(_navigationService.GoBack);

            DateSelectionViewModel = new CompleteInformationViewModel(timeSheet, goBackCommand);
            DateSelectionViewModel.TimeSheetCompleted += (s, ea) =>
            {
                if (ea.DateRange.HasValue)
                {
                    timeSheet.Period = ea.DateRange;
                }
                OnTimeSheetCreated(new TimeSheetCreatedEventArgs(timeSheet, ea.WorkContract));
            };

            _navigationService.Navigate(new DateSelectionPage(DateSelectionViewModel));
        }
    }
}
