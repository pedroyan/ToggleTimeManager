using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TogglTimeManager.Services
{
    public interface IFilePicker
    {
        /// <summary>
        /// Shows the file picker to the user and gets the path to the file. Returns true if the selection was successful
        /// </summary>
        /// <param name="filePath">The file path selected by the user</param>
        /// <returns>A flag indicating if a path was successfully selected</returns>
        bool PickFile(out string filePath);
    }
}
