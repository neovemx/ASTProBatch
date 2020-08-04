using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AST_ProBatch_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInquiriesPage : TabbedPage
    {
        public LogInquiriesPage()
        {
            InitializeComponent();
        }

        private void LVLogInquiriesResult_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            var resultLogInquirieItem = e.SelectedItem as ResultLogInquirieItem;
            if (resultLogInquirieItem == null)
            {
                return;
            }
            else
            {
                MainViewModel.GetInstance().LogInquirieDetail = new LogInquirieDetailViewModel(resultLogInquirieItem);
                Application.Current.MainPage.Navigation.PushModalAsync(new LogInquireDetailPage());
                LVLogInquiriesResult.SelectedItem = null;
            }
        }
    }
}