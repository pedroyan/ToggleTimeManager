using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TogglTimeManager.Services
{
    public interface INotificationService
    {
        /// <summary>
        /// Displays an alert on a dialog
        /// </summary>
        /// <param name="message"></param>
        void DisplayAlert(string message);
    }

    public class NotificationService : INotificationService
    {
        public void DisplayAlert(string message)
        {
            MessageBox.Show(message,"Alert");
        }
    }
}
