﻿using AST_ProBatch_Mobile.Interfaces;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AST_ProBatch_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExecutionStageThreePage : ContentPage
    {
        public ExecutionStageThreePage()
        {
            InitializeComponent();
        }

        private async void ImageButtonPause_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                var result = await this.DisplayAlert("AST●ProBatch®", "Pausar el: " + commandItem.Title + "?", "Sí", "No");
                if (result) DependencyService.Get<Toast>().Show("Procesando...");
            }
        }

        private void ImageButtonState_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().StatusInfo = new StatusInfoViewModel();
            Application.Current.MainPage.Navigation.PushModalAsync(new StatusInfoPage());
        }

        private void ImageButtonAction_1_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Ejecutar para: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                });
            }
        }

        private void ImageButtonAction_2_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Ver resultados para: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                });
            }
        }

        private void ImageButtonAction_3_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Matar proceso para: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                });
            }
        }

        private void ImageButtonAction_4_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Reejecutar: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                });
            }
        }

        private void ImageButtonAction_5_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                MainViewModel.GetInstance().EditParameters = new EditParametersViewModel(commandItem);
                Application.Current.MainPage.Navigation.PushAsync(new EditParametersPage());
            }
        }

        private void ImageButtonAction_6_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Datos del comando para: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                });
            }
        }

        private void ImageButtonAction_7_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Reconectar: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                });
            }
        }

        private void ImageButtonAction_8_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Observaciones: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                });
            }
        }

        private void ImageButtonAction_9_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Dependencias para: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                });
            }
        }

        private void ImageButtonAction_10_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Datos del Lote para: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                });
            }
        }

        private void ImageButtonToolBarAction_1_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as ExecutionStageThreeViewModel;
            int instamceitemcount = 0;
            if (commandItem != null)
            {
                foreach (CommandItem item in commandItem.CommandItems)
                {
                    if (item.IsChecked) { instamceitemcount += 1; }
                }
                if (instamceitemcount <= 1)
                {
                    Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más lotes", "Aceptar");
                    return;
                }
                Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Deseas agregar / eliminar pausa para: " + instamceitemcount + " lotes seleccionados.", "Aceptar");
            }
        }

        private void ImageButtonToolBarAction_2_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as ExecutionStageThreeViewModel;
            int instamceitemcount = 0;
            if (commandItem != null)
            {
                foreach (CommandItem item in commandItem.CommandItems)
                {
                    if (item.IsChecked) { instamceitemcount += 1; }
                }
                if (instamceitemcount <= 1)
                {
                    Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más lotes", "Aceptar");
                    return;
                }
                Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Deseas incluir / omitir comandos para: " + instamceitemcount + " lotes seleccionados.", "Aceptar");
            }
        }

        private void ImageButtonToolBarAction_3_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as ExecutionStageThreeViewModel;
            int instamceitemcount = 0;
            if (commandItem != null)
            {
                foreach (CommandItem item in commandItem.CommandItems)
                {
                    if (item.IsChecked) { instamceitemcount += 1; }
                }
                if (instamceitemcount <= 1)
                {
                    Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más lotes", "Aceptar");
                    return;
                }
                Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Observaciones para: " + instamceitemcount + " lotes seleccionados.", "Aceptar");
            }
        }
    }
}