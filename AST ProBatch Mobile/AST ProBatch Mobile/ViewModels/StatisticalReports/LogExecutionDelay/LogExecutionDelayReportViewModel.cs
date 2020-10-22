using System.Collections.ObjectModel;
using System.Windows.Input;
using AST_ProBatch_Mobile.Models;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class LogExecutionDelayReportViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<LogExecutionDelayReportItem> logexecutiondelayresult;
        #endregion

        #region Properties
        public ObservableCollection<LogExecutionDelayReportItem> LogExecutionDelayResult
        {
            get { return logexecutiondelayresult; }
            set { SetValue(ref logexecutiondelayresult, value); }
        }
        #endregion

        #region Constructor
        public LogExecutionDelayReportViewModel(bool IsReload, ObservableCollection<LogExecutionDelayReportItem> LogExecutionDelayResult)
        {
            if (IsReload)
            {
                this.LogExecutionDelayResult = LogExecutionDelayResult;
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
