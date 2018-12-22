using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace TogglTimeManager.Services
{
    public interface IFilePicker
    {
        /// <summary>
        /// Shows the file picker to the user and gets the path to the file. Returns null if the selection fails
        /// </summary>
        /// <param name="extension">Accepted extension</param>
        /// <returns>A flag indicating if a path was successfully selected</returns>
        string PickFile(string extension = null);
    }

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
