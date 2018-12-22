using System.Windows.Controls;
using TogglTimeManager.ViewModels;

namespace TogglTimeManager.Views.NewSheet
{
    /// <summary>
    /// Interaction logic for FileSelectionPage.xaml
    /// </summary>
    public partial class FileSelectionPage : Page
    {
        public FileSelectionPage(FileSelectionViewModel fileSelectionViewModel)
        {
            InitializeComponent();
            DataContext = fileSelectionViewModel;
        }
    }
}
