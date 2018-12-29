using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TogglTimeManager.Core.Models;
using TogglTimeManager.Helpers;
using TogglTimeManager.Services;
using TogglTimeManager.Views.TimeOff;

namespace TogglTimeManager.ViewModels
{
    public class TimeOffManagementViewModel : ObservableObject
    {
        private readonly IWindowService _windowService;
        private readonly IUserRepository _userRepository;
        private UserInfo _userInfo;

        public TimeOffManagementViewModel(IWindowService windowService, IUserRepository userRepository)
        {
            _windowService = windowService;
            _userRepository = userRepository;
        }

        #region Properties

        private ObservableCollection<TimeOff> _timeOffs;

        public ObservableCollection<TimeOff> TimeOffs
        {
            get => _timeOffs ?? throw new Exception("View model was not properly prepared before being used");
            set
            {
                if (value == _timeOffs) return;
                _timeOffs = value;
                OnPropertyChanged();
            }
        }

        private ICommand _addTimeOffCommand;
        public ICommand AddTimeOffCommand => _addTimeOffCommand ?? (_addTimeOffCommand = new ButtonCommand(AddTimeOff));

        private ICommand _removeTimeOffCommand;
        public ICommand RemoveTimeOffCommand => _removeTimeOffCommand ?? (_removeTimeOffCommand = new RelayCommand<TimeOff>(RemoveTimeOff));

        #endregion

        /// <summary>
        /// Asynchronously prepares the view model data to be used
        /// </summary>
        /// <returns></returns>
        public async Task PrepareViewModel()
        {
            //This step is not done on the constructor because the constructor cannot execute async methods
            _userInfo = await _userRepository.GetUserInfo();
            TimeOffs = new ObservableCollection<TimeOff>(_userInfo.TimeOffs ?? new List<TimeOff>());
        }

        private void AddTimeOff()
        {
            var vm = IoC.Resolve<NewTimeOffViewModel>();
            var window = new NewTimeOffWindow(vm);

            vm.TimeOffCreated += (s, e) =>
            {
                _userInfo.TimeOffs.Add(e);
                _userRepository.Persist(_userInfo);

                TimeOffs.Add(e);
                _windowService.Close(window);
            };

            _windowService.ShowDialog(window);
        }

        private void RemoveTimeOff(TimeOff timeOff)
        {
            TimeOffs.Remove(timeOff);
            _userInfo.TimeOffs.Remove(timeOff);
            _userRepository.Persist(_userInfo);
        }
    }
}
