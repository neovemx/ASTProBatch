using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AST_ProBatch_Mobile.Models;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class LotAndCommandReportViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<LotAndCommandReportItem> lotandcommandresult;
        #endregion

        #region Properties
        public ObservableCollection<LotAndCommandReportItem> LotAndCommandResult
        {
            get { return lotandcommandresult; }
            set { SetValue(ref lotandcommandresult, value); }
        }
        #endregion

        #region Constructor
        public LotAndCommandReportViewModel(bool IsReload, ObservableCollection<LotAndCommandReportItem> LotAndCommandResult)
        {
            if (IsReload)
            {
                this.LotAndCommandResult = LotAndCommandResult;
            }
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
