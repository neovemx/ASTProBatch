using AST_ProBatch_Mobile.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class LogInquirieDetailViewModel : BaseViewModel
    {
        #region Atributes
        private ResultLogInquirieItem resultloginquirieitem;
        private string logtitle;
        #endregion

        #region Properties
        public ResultLogInquirieItem ResultLogInquirieItem
        {
            get { return resultloginquirieitem; }
            set { SetValue(ref resultloginquirieitem, value); }
        }
        public string LogTitle
        {
            get { return logtitle; }
            set { SetValue(ref logtitle, value); }
        }
        #endregion

        #region Constructors
        public LogInquirieDetailViewModel(string logTitle, ResultLogInquirieItem resultLogInquirieItem)
        {
            this.ResultLogInquirieItem = resultLogInquirieItem;
            this.LogTitle = logTitle;
        }
        #endregion

        #region Commands
        public ICommand CloseCommand
        {
            get
            {
                return new RelayCommand(Close);
            }
        }

        private async void Close()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        #endregion

        #region Helpers

        #endregion
    }
}
