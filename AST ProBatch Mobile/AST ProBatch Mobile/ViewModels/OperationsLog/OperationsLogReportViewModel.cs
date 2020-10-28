using System.Collections.ObjectModel;
using System.Windows.Input;
using AST_ProBatch_Mobile.Models;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class OperationsLogReportViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<OperationsLogReportItem> operationslogresult;
        #endregion

        #region Properties
        public ObservableCollection<OperationsLogReportItem> OperationsLogResult
        {
            get { return operationslogresult; }
            set { SetValue(ref operationslogresult, value); }
        }
        #endregion

        #region Constructors
        public OperationsLogReportViewModel(bool IsReload, ObservableCollection<OperationsLogReportItem> operationsLogReportItems)
        {
            if (IsReload)
            {
                this.OperationsLogResult = operationsLogReportItems;
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
