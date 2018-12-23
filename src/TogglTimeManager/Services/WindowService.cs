using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TogglTimeManager.Services
{
    public interface IWindowService
    {
        bool? ShowDialog(Window window);
        void Show(Window window);
        void Close(Window window);
    }

    public class WindowService : IWindowService
    {
        public bool? ShowDialog(Window window)
        {
            return window.ShowDialog();
        }

        public void Show(Window window)
        {
            window.Show();
        }
        public void Close(Window window)
        {
            window.Close();
        }
    }
}
