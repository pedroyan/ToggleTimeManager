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

namespace TogglTimeManager.Controls
{
    /// <summary>
    /// Interaction logic for DateRangePicker.xaml
    /// </summary>
    public partial class DateRangePicker : UserControl
    {
        public DateRangePicker()
        {
            InitializeComponent();
        }

        #region DependencyPropeties

        public static readonly DependencyProperty ErrorStringProperty = DependencyProperty.Register(
            "ErrorString", typeof(string), typeof(DateRangePicker), new PropertyMetadata(default(string)));

        public string ErrorString
        {
            get => (string) GetValue(ErrorStringProperty);
            set => SetValue(ErrorStringProperty, value);
        }

        #endregion
    }
}
