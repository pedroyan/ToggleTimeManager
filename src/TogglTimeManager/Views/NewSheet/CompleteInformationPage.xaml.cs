using System.Windows.Controls;
using TogglTimeManager.ViewModels;

namespace TogglTimeManager.Views.NewSheet
{
    /// <summary>
    /// Interaction logic for CompleteInformationPage.xaml
    /// </summary>
    public partial class CompleteInformationPage : Page
    {
        public CompleteInformationPage(CompleteInformationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
