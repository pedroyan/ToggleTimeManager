using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TogglTimeManager.Properties;

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
}
