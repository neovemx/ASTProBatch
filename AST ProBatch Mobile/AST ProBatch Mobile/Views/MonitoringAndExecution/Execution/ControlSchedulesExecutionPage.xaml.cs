using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AST_ProBatch_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlSchedulesExecutionPage : ContentPage
    {
        public ControlSchedulesExecutionPage()
        {
            InitializeComponent();
        }

        private void CommandsToControlListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            var commandToControlItem = e.SelectedItem as CommandsToControl;
            if (commandToControlItem == null)
            {
                return;
            }
            else
            {
                MainViewModel.GetInstance().ControlSchedulesExecutionDetail = new ControlSchedulesExecutionDetailViewModel(commandToControlItem);
                Application.Current.MainPage.Navigation.PushModalAsync(new ControlSchedulesExecutionDetailPage());
                CommandsToControlListView.SelectedItem = null;
            }
        }
    }
}