using AST_ProBatch_Mobile.Interfaces;
using AST_ProBatch_Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AST_ProBatch_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecurrenceMonitorPage : ContentPage
    {
        public RecurrenceMonitorPage()
        {
            InitializeComponent();
        }

        private void btnState_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().StatusInfoPlanner = new StatusInfoPlannerViewModel();
            Application.Current.MainPage.Navigation.PushModalAsync(new StatusInfoPlannerPage());
        }

        private void toolbarmodule_action_1_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert("AST●ProBatch®", "Deseas ejecutar la acción?", "Sí", "No");
                if (result) DependencyService.Get<Toast>().Show("Ejecutando...");
            });
        }

        private void toolbarmodule_action_2_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert("AST●ProBatch®", "Deseas ejecutar la acción?", "Sí", "No");
                if (result) DependencyService.Get<Toast>().Show("Ejecutando...");
            });
        }

        private void toolbarmodule_action_3_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert("AST●ProBatch®", "Deseas ejecutar la acción?", "Sí", "No");
                if (result) DependencyService.Get<Toast>().Show("Ejecutando...");
            });
        }

        private void toolbar_action_1_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert("AST●ProBatch®", "Deseas ejecutar la acción?", "Sí", "No");
                if (result) DependencyService.Get<Toast>().Show("Ejecutando...");
            });
        }

        private void toolbar_action_2_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert("AST●ProBatch®", "Deseas ejecutar la acción?", "Sí", "No");
                if (result) DependencyService.Get<Toast>().Show("Ejecutando...");
            });
        }

        private void toolbar_action_3_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert("AST●ProBatch®", "Deseas ejecutar la acción?", "Sí", "No");
                if (result) DependencyService.Get<Toast>().Show("Ejecutando...");
            });
        }
    }
}