using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TogglTimeManager.Services
{
    public interface INavigationService
    {
        void GoBack();
        void GoForward();
        void Navigate(Page page);
    }
}
