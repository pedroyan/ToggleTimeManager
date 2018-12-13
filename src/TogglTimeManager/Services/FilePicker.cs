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
        public string PickFile(string extension = null)
        {
            var openFileDialog = new OpenFileDialog();
            var filter = string.IsNullOrWhiteSpace(extension) ? "All files (*.*)|*.*" : extension;
            openFileDialog.Filter = filter;
            return openFileDialog.ShowDialog() != true ? null : openFileDialog.FileName;
        }
    }
}
