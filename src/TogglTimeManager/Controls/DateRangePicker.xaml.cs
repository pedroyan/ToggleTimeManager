using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TogglTimeManager.Core.Models;

namespace TogglTimeManager.Controls
{
    /// <summary>
    /// Interaction logic for DateRangePicker.xaml
    /// </summary>
    public partial class DateRangePicker : UserControl
    {
        private DatePickerViewModel ViewModel => (DatePickerViewModel) DataContext;

        public DateRangePicker()
        {
            InitializeComponent();
            DataContext = new DatePickerViewModel();
            ViewModel.DateRangeChanged += (s, e) => { DateRange = e; };
        }

        #region DependencyPropeties

        public static readonly DependencyProperty DateRangeProperty = DependencyProperty.Register(
            "DateRange", typeof(DateRange?), typeof(DateRangePicker), new PropertyMetadata(default(DateRange?), OnRangeChanged));

        public DateRange? DateRange
        {
            get => (DateRange?) GetValue(DateRangeProperty);
            set => SetValue(DateRangeProperty, value);
        }

        private static void OnRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine(d);
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
