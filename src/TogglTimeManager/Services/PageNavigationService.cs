using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace TogglTimeManager.Services
{
    public class PageNavigationService : INavigationService
    {
        private readonly NavigationService _service;

        public PageNavigationService(NavigationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "The page navigation service needs " +
                                                                                   "a reference to the frame navigation service");
        }

        public void GoBack()
        {
            _service.GoBack();
        }

        public void GoForward()
        {
            _service.GoForward();
        }

        public void Navigate(Page page)
        {
            _service.Navigate(page);
        }
    }
}
