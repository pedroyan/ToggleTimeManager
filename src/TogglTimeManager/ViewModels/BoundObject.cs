using System.ComponentModel;
using System.Runtime.CompilerServices;
using TogglTimeManager.Properties;

namespace TogglTimeManager.ViewModels
{
    public class BoundObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
