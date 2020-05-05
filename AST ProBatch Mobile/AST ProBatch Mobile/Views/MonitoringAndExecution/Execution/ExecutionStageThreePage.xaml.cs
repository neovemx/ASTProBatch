using AST_ProBatch_Mobile.Interfaces;
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

        private void ImageButtonNotification_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                MainViewModel.GetInstance().CommandNotifications = new CommandNotificationsViewModel(commandItem);
                Application.Current.MainPage.Navigation.PushModalAsync(new CommandNotificationsPage());
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
                    var result = await this.DisplayAlert("AST●ProBatch®", "Agregar / eliminar pausa para: " + commandItem.Title + "?", "Sí", "No");
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
                    var result = await this.DisplayAlert("AST●ProBatch®", "Incluir / omitir comandos para: " + commandItem.Title + "?", "Sí", "No");
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
                    var result = await this.DisplayAlert("AST●ProBatch®", "Modificar parámetros para: " + commandItem.Title + "?", "Sí", "No");
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Datos del comando para: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                });
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
                    var result = await this.DisplayAlert("AST●ProBatch®", "Dependencia lote comando para: " + commandItem.Title + "?", "Sí", "No");
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
                    var result = await this.DisplayAlert("AST●ProBatch®", "Datos del lote para: " + commandItem.Title + "?", "Sí", "No");
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
                    Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más Lotes", "Aceptar");
                    return;
                }
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas ejecutar los: " + instamceitemcount + " lotes seleccionados?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Ejecutando...");
                });
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas detener los: " + instamceitemcount + " lotes seleccionados?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Deteniendo...");
                });
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas matar el proceso para: " + instamceitemcount + " lotes seleccionados?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                });
            }
        }

        private void ImageButtonToolBarAction_4_Clicked(object sender, EventArgs e)
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas reejecutar los: " + instamceitemcount + " lotes seleccionados?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Reconectando...");
                });
            }
        }

        private void ImageButtonToolBarAction_5_Clicked(object sender, EventArgs e)
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
                Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Deseas reversar los: " + instamceitemcount + " lotes seleccionados.", "Aceptar");
            }
        }

        private void ImageButtonToolBarAction_6_Clicked(object sender, EventArgs e)
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

        private void ImageButtonToolBarAction_7_Clicked(object sender, EventArgs e)
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

        private void ImageButtonToolBarAction_8_Clicked(object sender, EventArgs e)
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
                Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Deseas reconectar los: " + instamceitemcount + " lotes seleccionados.", "Aceptar");
            }
        }

        private void ImageButtonToolBarAction_9_Clicked(object sender, EventArgs e)
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