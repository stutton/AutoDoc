using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AppExtensionHoster
{
    public class NotifyBase : INotifyPropertyChanged
    {
        private static readonly Dictionary<string, PropertyChangedEventArgs> EventArgCache = new Dictionary<string, PropertyChangedEventArgs>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, GetPropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Equals(oldValue, newValue))
            {
                return false;
            }
            oldValue = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }

        public static PropertyChangedEventArgs GetPropertyChangedEventArgs(string propertyName)
        {
            lock (typeof(NotifyBase))
            {
                if (!EventArgCache.TryGetValue(propertyName, out var args))
                {
                    EventArgCache.Add(propertyName, args = new PropertyChangedEventArgs(propertyName));
                }
                return args;
            }
        }
    }
}
