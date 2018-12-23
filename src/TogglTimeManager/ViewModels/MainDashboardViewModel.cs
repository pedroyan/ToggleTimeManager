using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TogglTimeManager.Core;
using TogglTimeManager.Core.Models;
using TogglTimeManager.Helpers;
using TogglTimeManager.Services;
using TogglTimeManager.Views.NewSheet;

namespace TogglTimeManager.ViewModels
{
    public class MainDashboardViewModel : BoundObject
    {
        private readonly IUserRepository _repository;
        private readonly IWindowService _windowService;
        private WorkHoursSummary _summary;

        public WorkHoursSummary Summary
        {
            get => _summary;
            set
            {
                if (value == _summary) return;
                _summary = value;
                OnPropertyChanged();

                OnPropertyChanged(nameof(TimeWorked));
                OnPropertyChanged(nameof(ExpectedWork));
                OnPropertyChanged(nameof(WorkTimeBalance));
                OnPropertyChanged(nameof(AnalyzedPeriod));
                OnPropertyChanged(nameof(WorkTimeBalanceColor));
            }
        }

        public MainDashboardViewModel(WorkHoursSummary summary, IUserRepository repository, IWindowService windowService)
        {
            _repository = repository;
            _windowService = windowService;
            Summary = summary ?? throw new ArgumentNullException(nameof(summary), "Summary cannot be null");
        }

        #region Binded Properties

        public string TimeWorked => Summary.TimeWorked.ToHoursString();

        public string ExpectedWork => Summary.PlannedWork.ToHoursString();

        public string WorkTimeBalance => Summary.WorkTimeBalance.ToHoursString();

        public string AnalyzedPeriod => $"Analyzed period: {Summary.Period.StartDate:d} - {Summary.Period.EndDate:d}";

        public Brush WorkTimeBalanceColor
        {
            get
            {

                if (Summary.WorkTimeBalance == TimeSpan.Zero)
                {
                    return new SolidColorBrush(Colors.Black);
                }

                if (Summary.WorkTimeBalance > TimeSpan.Zero)
                {
                    return new SolidColorBrush(Colors.Green);
                }

                return new SolidColorBrush(Colors.Red);
            }
        }

        private ICommand _updateTimeSheetCommand;
        public ICommand UpdateTimeSheetCommand => _updateTimeSheetCommand ?? (_updateTimeSheetCommand = new ButtonCommand(UpdateTimeSheet));

        #endregion

        private async void UpdateTimeSheet()
        {
            var user = await _repository.GetUserInfo();
            if (user == null)
            {
                throw new NullReferenceException("A reference to an user must be available to update the time sheet");
            }

            var viewModel = new NewSheetWindowViewModel();
            var newSheetWindow = new NewSheetWindow(viewModel);

            viewModel.TimeSheetCreated += (s, e) =>
            {
                user.WorkContract = e.WorkContract;
                user.Summary = TimeSummaryCalculator.CalculateHoursSummary(user.WorkContract, e.TimeSheet);
                Summary = user.Summary;

                _repository.Persist(user);
                _windowService.Close(newSheetWindow);
            };

            _windowService.ShowDialog(newSheetWindow);
        }
    }
}
