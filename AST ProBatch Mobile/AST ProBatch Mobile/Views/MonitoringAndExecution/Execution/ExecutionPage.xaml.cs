﻿using AST_ProBatch_Mobile.Interfaces;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AST_ProBatch_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExecutionPage : ContentPage
    {
        public ExecutionPage()
        {
            InitializeComponent();
        }

        private void ImageButtonNotification_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var logItem = imageButton.CommandParameter as LogItem;
            if (logItem != null)
            {
                MainViewModel.GetInstance().Notifications = new NotificationsViewModel(logItem);
                Application.Current.MainPage.Navigation.PushModalAsync(new NotificationsPage());
            }
        }

        private void ImageButtonState_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().StatusInfo = new StatusInfoViewModel();
            Application.Current.MainPage.Navigation.PushModalAsync(new StatusInfoPage());
        }

        private void ImageButtonLogAction_1_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var logItem = imageButton.CommandParameter as LogItem;
            if (logItem != null)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas ejecutar la: " + logItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Ejecutando...");
                });
            }
        }

        private void ImageButtonLogAction_2_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var logItem = imageButton.CommandParameter as LogItem;
            if (logItem != null)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas detener la: " + logItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Deteniendo...");
                });
            }
        }

        private void ImageButtonLogAction_3_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var logItem = imageButton.CommandParameter as LogItem;
            if (logItem != null)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas reliberar la bitácora: " + logItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Reliberando...");
                });
            }
        }

        private void ImageButtonLogAction_4_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var logItem = imageButton.CommandParameter as LogItem;
            if (logItem != null)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas reconectar la bitácora: " + logItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Reconectando...");
                });
            }
        }

        private void ImageButtonLogAction_5_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var logItem = imageButton.CommandParameter as LogItem;
            if (logItem != null)
            {
                MainViewModel.GetInstance().OperatorChange = new  OperatorChangeViewModel(logItem);
                Application.Current.MainPage.Navigation.PushAsync(new OperatorChangePage());
                //Device.BeginInvokeOnMainThread(async () => {
                //    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas cambiar el operador para la bitácora: " + logItem.Title + "?", "Sí", "No");
                //    if (result) DependencyService.Get<Toast>().Show("Cambiando operador...");
                //});
            }
        }

        private void ImageButtonLogAction_6_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var logItem = imageButton.CommandParameter as LogItem;
            if (logItem != null)
            {
                MainViewModel.GetInstance().LogObservations = new LogObservationsViewModel(logItem);
                Application.Current.MainPage.Navigation.PushAsync(new LogObservationsPage());
            }
        }

        private void ImageButtonLogAction_7_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var logItem = imageButton.CommandParameter as LogItem;
            if (logItem != null)
            {
                MainViewModel.GetInstance().ControlSchedulesExecution = new ControlSchedulesExecutionViewModel(logItem);
                Application.Current.MainPage.Navigation.PushAsync(new ControlSchedulesExecutionPage());
            }
        }

        private void ImageButtonLogAction_8_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var logItem = imageButton.CommandParameter as LogItem;
            if (logItem != null)
            {
                Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Consultas para: " + logItem.Title, "Aceptar");
            }
        }

        private void ImageButtonLogToolBarAction_1_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var logItem = imageButton.CommandParameter as ExecutionViewModel;
            int logitemcount = 0;
            if (logItem != null)
            {
                foreach (LogItem item in logItem.LogItems)
                {
                    if (item.IsChecked) { logitemcount += 1; }
                }
                if (logitemcount <= 1)
                {
                    Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más bitácoras", "Aceptar");
                    return;
                }
                Device.BeginInvokeOnMainThread(async () => {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas ejecutar las: " + logitemcount + " bitácoras seleccionadas?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Ejecutando...");
                });
            }
        }

        private void ImageButtonLogToolBarAction_2_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var logItem = imageButton.CommandParameter as ExecutionViewModel;
            int logitemcount = 0;
            if (logItem != null)
            {
                foreach (LogItem item in logItem.LogItems)
                {
                    if (item.IsChecked) { logitemcount += 1; }
                }
                if (logitemcount <= 1)
                {
                    Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más bitácoras", "Aceptar");
                    return;
                }
                Device.BeginInvokeOnMainThread(async () => {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas detener las: " + logitemcount + " bitácoras seleccionadas?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Deteniendo...");
                });
            }
        }

        private void ImageButtonLogToolBarAction_3_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var logItem = imageButton.CommandParameter as ExecutionViewModel;
            int logitemcount = 0;
            if (logItem != null)
            {
                foreach (LogItem item in logItem.LogItems)
                {
                    if (item.IsChecked) { logitemcount += 1; }
                }
                if (logitemcount <= 1)
                {
                    Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más bitácoras", "Aceptar");
                    return;
                }
                Device.BeginInvokeOnMainThread(async () => {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas reliberar las: " + logitemcount + " bitácoras seleccionadas?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Reliberando...");
                });
            }
        }

        private void ImageButtonLogToolBarAction_4_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var logItem = imageButton.CommandParameter as ExecutionViewModel;
            int logitemcount = 0;
            if (logItem != null)
            {
                foreach (LogItem item in logItem.LogItems)
                {
                    if (item.IsChecked) { logitemcount += 1; }
                }
                if (logitemcount <= 1)
                {
                    Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más bitácoras", "Aceptar");
                    return;
                }
                Device.BeginInvokeOnMainThread(async () => {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas reconectar las: " + logitemcount + " bitácoras seleccionadas?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Reconectando...");
                });
            }
        }

        //private void ImageButtonLogToolBarAction_5_Clicked(object sender, EventArgs e)
        //{
        //    var imageButton = (ImageButton)sender;
        //    var logItem = imageButton.CommandParameter as ExecutionViewModel;
        //    int logitemcount = 0;
        //    if (logItem != null)
        //    {
        //        foreach (LogItem item in logItem.LogItems)
        //        {
        //            if (item.IsChecked) { logitemcount += 1; }
        //        }
        //        if (logitemcount <= 1)
        //        {
        //            Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más bitácoras", "Aceptar");
        //            return;
        //        }
        //        Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Obervaciones para: " + logitemcount + " Bitacoras seleccionadas.", "Aceptar");
        //    }
        //}

        //private void ImageButtonLogToolBarAction_6_Clicked(object sender, EventArgs e)
        //{
        //    var imageButton = (ImageButton)sender;
        //    var logItem = imageButton.CommandParameter as ExecutionViewModel;
        //    int logitemcount = 0;
        //    if (logItem != null)
        //    {
        //        foreach (LogItem item in logItem.LogItems)
        //        {
        //            if (item.IsChecked) { logitemcount += 1; }
        //        }
        //        if (logitemcount <= 1)
        //        {
        //            Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más bitácoras", "Aceptar");
        //            return;
        //        }
        //        Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Notificaciones para: " + logitemcount + " Bitacoras seleccionadas.", "Aceptar");
        //    }
        //}

        private void LogsItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var logitem = e.SelectedItem as LogItem;
            if (logitem == null)
            {
                return;
            }
            else
            {
                MainViewModel.GetInstance().ExecutionStageTwo = new ExecutionStageTwoViewModel(logitem);
                ExecutionStageTwoPage executionStageTwoPage = new ExecutionStageTwoPage();
                executionStageTwoPage.Title = logitem.Title;
                Application.Current.MainPage.Navigation.PushAsync(executionStageTwoPage);
                LogsItemsListView.SelectedItem = null;
            }
        }

        private void CompactLogsItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var logitem = e.SelectedItem as LogItem;
            if (logitem == null)
            {
                return;
            }
            else
            {
                MainViewModel.GetInstance().ExecutionStageTwo = new ExecutionStageTwoViewModel(logitem);
                ExecutionStageTwoPage executionStageTwoPage = new ExecutionStageTwoPage();
                executionStageTwoPage.Title = logitem.Title;
                Application.Current.MainPage.Navigation.PushAsync(executionStageTwoPage);
                CompactLogsItemsListView.SelectedItem = null;
            }
        }
    }
}