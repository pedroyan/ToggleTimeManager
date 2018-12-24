using System;
using System.Windows;
using System.Windows.Controls;
using TogglTimeManager.Core.Models;

namespace TogglTimeManager.Controls
{
    /// <summary>
    /// Interaction logic for DateRangePicker.xaml
    /// </summary>
    public partial class DateRangePicker : UserControl
    {
        private DatePickerViewModel ViewModel => (DatePickerViewModel)RootGrid.DataContext;

        public DateRangePicker()
        {
            InitializeComponent();
            ViewModel.DateRangeChanged += (s, e) => { DateRange = e; };
        }

        #region DependencyPropeties

        public static readonly DependencyProperty DateRangeProperty = DependencyProperty.Register(
            "DateRange", typeof(DateRange?), typeof(DateRangePicker), new PropertyMetadata(default(DateRange?)));

        public DateRange? DateRange
        {
            get => (DateRange?) GetValue(DateRangeProperty);
            set => SetValue(DateRangeProperty, value);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == DateRangeProperty)
            {
                var dateRange = (DateRange?) e.NewValue;
                if (!dateRange.HasValue)
                {
                    //If needed, in the future I will create a dependency property where consuming code can specify the default value in case of null
                    ViewModel.From = DateTime.Today;
                    ViewModel.To = DateTime.Today;
                    return;
                }

                ViewModel.From = dateRange.Value.StartDate;
                ViewModel.To = dateRange.Value.EndDate;
            }
        }

        #endregion
    }
}
