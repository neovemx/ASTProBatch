using AST_ProBatch_Mobile.Interfaces;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AST_ProBatch_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExecutionStageTwoPage : ContentPage
    {
        public ExecutionStageTwoPage()
        {
            InitializeComponent();
        }

        private void ImageButtonNotification_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var instanceItem = imageButton.CommandParameter as InstanceItem;
            if (instanceItem != null)
            {
                MainViewModel.GetInstance().InstanceNotifications = new InstanceNotificationsViewModel(instanceItem);
                Application.Current.MainPage.Navigation.PushModalAsync(new InstanceNotificationsPage());
            }
        }

        private void ImageButtonState_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().StatusInfoExecutionStageTwo = new StatusInfoExecutionStageTwoViewModel();
            Application.Current.MainPage.Navigation.PushModalAsync(new StatusInfoExecutionStageTwoPage());
        }

        private void ImageButtonLogAction_1_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var instanceItem = imageButton.CommandParameter as InstanceItem;
            if (instanceItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas ejecutar la: " + instanceItem.NameInstance + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Ejecutando...");
                });
            }
        }

        private void ImageButtonLogAction_2_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var instanceItem = imageButton.CommandParameter as InstanceItem;
            if (instanceItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas detener la: " + instanceItem.NameInstance + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Deteniendo...");
                });
            }
        }

        private void ImageButtonLogAction_3_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var instanceItem = imageButton.CommandParameter as InstanceItem;
            if (instanceItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas matar el proceso para: " + instanceItem.NameInstance + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Finalizando...");
                });
            }
        }

        //private void ImageButtonLogAction_4_Clicked(object sender, EventArgs e)
        //{
        //    var imageButton = (ImageButton)sender;
        //    var instanceItem = imageButton.CommandParameter as InstanceItem;
        //    if (instanceItem != null)
        //    {
        //        Device.BeginInvokeOnMainThread(async () =>
        //        {
        //            var result = await this.DisplayAlert("AST●ProBatch®", "Deseas reejecutar comando para: " + instanceItem.Title + "?", "Sí", "No");
        //            if (result) DependencyService.Get<Toast>().Show("Reejecutando...");
        //        });
        //    }
        //}

        //private void ImageButtonLogAction_4_Clicked(object sender, EventArgs e)
        //{
        //    var imageButton = (ImageButton)sender;
        //    var instanceItem = imageButton.CommandParameter as InstanceItem;
        //    if (instanceItem != null)
        //    {
        //        Device.BeginInvokeOnMainThread(async () =>
        //        {
        //            var result = await this.DisplayAlert("AST●ProBatch®", "Deseas agregar comando para: " + instanceItem.NameInstance + "?", "Sí", "No");
        //            if (result) DependencyService.Get<Toast>().Show("Procesando...");
        //        });
        //    }
        //}

        //ELIMINAR
        //private void ImageButtonLogAction_5_Clicked(object sender, EventArgs e)
        //{
        //    var imageButton = (ImageButton)sender;
        //    var instanceItem = imageButton.CommandParameter as InstanceItem;
        //    if (instanceItem != null)
        //    {
        //        Device.BeginInvokeOnMainThread(async () =>
        //        {
        //            var result = await this.DisplayAlert("AST●ProBatch®", "Deseas buscar lote comando para: " + instanceItem.NameInstance + "?", "Sí", "No");
        //            if (result) DependencyService.Get<Toast>().Show("Procesando...");
        //        });
        //    }
        //}

        private void ImageButtonLogAction_6_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var instanceItem = imageButton.CommandParameter as InstanceItem;
            if (instanceItem != null)
            {
                MainViewModel.GetInstance().LogObservations = new LogObservationsViewModel(true, new LotAndCommandObservation(), instanceItem.LogItem);
                Application.Current.MainPage.Navigation.PushAsync(new LogObservationsPage());
            }
        }

        private void ImageButtonLogToolBarAction_1_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var instanceItem = imageButton.CommandParameter as ExecutionStageTwoViewModel;
            int instamceitemcount = 0;
            if (instanceItem != null)
            {
                foreach (InstanceItem item in instanceItem.InstanceItems)
                {
                    if (item.IsChecked) { instamceitemcount += 1; }
                }
                if (instamceitemcount <= 1)
                {
                    Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más instancias", "Aceptar");
                    return;
                }
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas ejecutar las: " + instamceitemcount + " instancias seleccionadas?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Ejecutando...");
                });
            }
        }

        private void ImageButtonLogToolBarAction_2_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var instanceItem = imageButton.CommandParameter as ExecutionStageTwoViewModel;
            int instamceitemcount = 0;
            if (instanceItem != null)
            {
                foreach (InstanceItem item in instanceItem.InstanceItems)
                {
                    if (item.IsChecked) { instamceitemcount += 1; }
                }
                if (instamceitemcount <= 1)
                {
                    Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más bitácoras", "Aceptar");
                    return;
                }
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas detener las: " + instamceitemcount + " instancias seleccionadas?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Deteniendo...");
                });
            }
        }

        private void ImageButtonLogToolBarAction_3_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var instanceItem = imageButton.CommandParameter as ExecutionStageTwoViewModel;
            int instamceitemcount = 0;
            if (instanceItem != null)
            {
                foreach (InstanceItem item in instanceItem.InstanceItems)
                {
                    if (item.IsChecked) { instamceitemcount += 1; }
                }
                if (instamceitemcount <= 1)
                {
                    Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más bitácoras", "Aceptar");
                    return;
                }
                MainViewModel.GetInstance().LogObservations = new LogObservationsViewModel(true, new LotAndCommandObservation(), instanceItem.LogItem);
                Application.Current.MainPage.Navigation.PushAsync(new LogObservationsPage());
            }
        }

        private void InstanceItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var instanceitem = e.SelectedItem as InstanceItem;
            if (instanceitem == null)
            {
                return;
            }
            else
            {
                MainViewModel.GetInstance().ExecutionStageThree = new ExecutionStageThreeViewModel(true, instanceitem);
                //ExecutionStageThreePage executionStageThreePage = new ExecutionStageThreePage();
                //executionStageThreePage.Title = instanceitem.NameInstance;
                Application.Current.MainPage.Navigation.PushAsync(new ExecutionStageThreePage());
                InstanceItemsListView.SelectedItem = null;
            }
        }

        private void CompactInstanceItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var instanceitem = e.SelectedItem as InstanceItem;
            if (instanceitem == null)
            {
                return;
            }
            else
            {
                MainViewModel.GetInstance().ExecutionStageThree = new ExecutionStageThreeViewModel(true, instanceitem);
                //ExecutionStageThreePage executionStageThreePage = new ExecutionStageThreePage();
                //executionStageThreePage.Title = instanceitem.NameInstance;
                Application.Current.MainPage.Navigation.PushAsync(new ExecutionStageThreePage());
                CompactInstanceItemsListView.SelectedItem = null;
            }
        }
    }
}