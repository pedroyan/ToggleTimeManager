﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TogglTimeManager.Core.Models;
using TogglTimeManager.Services;

namespace TogglTimeManager.ViewModels
{
    public class DateSelectionViewModel
    {
        private readonly TimeSheet _timeSheet;
        private readonly IPageNavigationService _navigationService;

        public DateSelectionViewModel(TimeSheet timeSheet, IPageNavigationService navigationService)
        {
            _timeSheet = timeSheet;
            _navigationService = navigationService;
        }

        //TODO: Implement binded properties that handles the date input
        //TODO: Implement implement date range validation before attributing it to the time sheet
    }
}
