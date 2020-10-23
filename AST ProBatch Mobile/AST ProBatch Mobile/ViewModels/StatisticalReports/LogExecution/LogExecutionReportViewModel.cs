using System.Collections.ObjectModel;
using System.Windows.Input;
using AST_ProBatch_Mobile.Models;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class LogExecutionReportViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<LogExecutionReportItem> logexecutionresult;
        #endregion

        #region Properties
        public ObservableCollection<LogExecutionReportItem> LogExecutionResult
        {
            get { return logexecutionresult; }
            set { SetValue(ref logexecutionresult, value); }
        }
        #endregion

        #region Constructors
        public LogExecutionReportViewModel(bool IsReload, ObservableCollection<LogExecutionReportItem> LogExecutionResult)
        {
            if (IsReload)
            {
                this.LogExecutionResult = LogExecutionResult;
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
    }
}
