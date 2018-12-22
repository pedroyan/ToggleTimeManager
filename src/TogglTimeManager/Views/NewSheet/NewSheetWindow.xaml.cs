using System.Windows;
using TogglTimeManager.Services;
using TogglTimeManager.ViewModels;

namespace TogglTimeManager.Views.NewSheet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class NewSheetWindow : Window
    {
        public NewSheetWindow(NewSheetWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            Loaded += (s, e) =>
            {
                viewModel.DisplayFileSelection(new PageNavigationService(MainFrame.NavigationService));
            };
        }
    }
}
