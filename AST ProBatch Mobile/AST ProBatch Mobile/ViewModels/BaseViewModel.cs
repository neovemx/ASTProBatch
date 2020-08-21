using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Services
        public ApiService ApiSrv { get; set; }
        public DataHelper DBHelper { get; set; }
        public static PbUser PbUser { get; set; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                return;
            }

            backingField = value;
            OnPropertyChanged(propertyName);
        }
    }
}
