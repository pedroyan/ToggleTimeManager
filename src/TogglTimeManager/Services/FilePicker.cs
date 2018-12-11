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
        ///<inheritdoc/>
        public string PickFile()
        {
            var openFileDialog = new OpenFileDialog();
            return openFileDialog.ShowDialog() != true ? null : openFileDialog.FileName;
        }
    }
}
