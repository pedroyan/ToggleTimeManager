using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace TogglTimeManager.Services
{
    public class FilePicker : IFilePicker
    {
        public bool PickFile(out string filePath)
        {
            filePath = null;

            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                return true;
            }

            return false;
        }
    }
}
