using System.Linq.Expressions;
using System.Windows;
using TogglTimeManager.Services;
using TogglTimeManager.ViewModels;

namespace TogglTimeManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var navigationService = new PageNavigationService(MainFrame.NavigationService);
            navigationService.Navigate(new FileSelectionPage(navigationService));
        }
    }
}
