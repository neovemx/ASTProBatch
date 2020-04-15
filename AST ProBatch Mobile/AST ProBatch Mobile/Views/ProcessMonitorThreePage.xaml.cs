using AST_ProBatch_Mobile.Interfaces;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AST_ProBatch_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProcessMonitorThreePage : ContentPage
    {
        public ProcessMonitorThreePage()
        {
            InitializeComponent();
        }

        private void ImageButtonNotification_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                //MainViewModel.GetInstance().CommandNotifications = new CommandNotificationsViewModel(commandItem);
                //Application.Current.MainPage.Navigation.PushModalAsync(new CommandNotificationsPage());
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
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas ejecutar el: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Ejecutando...");
                });
            }
        }

        private void ImageButtonLogAction_2_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas detener la: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Deteniendo...");
                });
            }
        }

        private void ImageButtonLogAction_3_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas matar el proceso para: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Finalizando...");
                });
            }
        }

        private void ImageButtonLogAction_4_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas reejecutar comando para: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Reejecutando...");
                });
            }
        }

        private void ImageButtonLogAction_5_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas agregar comando para: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                });
            }
        }

        private void ImageButtonLogAction_6_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas buscar lote comando para: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                });
            }
        }

        private void ImageButtonLogAction_7_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas buscar lote comando para: " + commandItem.Title + "?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                });
            }
        }

        private void ImageButtonLogToolBarAction_1_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as ProcessMonitorThreeViewModel;
            int instamceitemcount = 0;
            if (commandItem != null)
            {
                foreach (CommandItem item in commandItem.CommandItems)
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
            var commandItem = imageButton.CommandParameter as ProcessMonitorThreeViewModel;
            int instamceitemcount = 0;
            if (commandItem != null)
            {
                foreach (CommandItem item in commandItem.CommandItems)
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
            var commandItem = imageButton.CommandParameter as ProcessMonitorThreeViewModel;
            int instamceitemcount = 0;
            if (commandItem != null)
            {
                foreach (CommandItem item in commandItem.CommandItems)
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
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas cambiar el operador para: " + instamceitemcount + " instancias seleccionadas?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                });
            }
        }

        private void ImageButtonLogToolBarAction_4_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as ProcessMonitorThreeViewModel;
            int instamceitemcount = 0;
            if (commandItem != null)
            {
                foreach (CommandItem item in commandItem.CommandItems)
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
                    var result = await this.DisplayAlert("AST●ProBatch®", "Deseas reconectar las: " + instamceitemcount + " instancias seleccionadas?", "Sí", "No");
                    if (result) DependencyService.Get<Toast>().Show("Reconectando...");
                });
            }
        }

        private void ImageButtonLogToolBarAction_5_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as ProcessMonitorThreeViewModel;
            int instamceitemcount = 0;
            if (commandItem != null)
            {
                foreach (CommandItem item in commandItem.CommandItems)
                {
                    if (item.IsChecked) { instamceitemcount += 1; }
                }
                if (instamceitemcount <= 1)
                {
                    Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más bitácoras", "Aceptar");
                    return;
                }
                Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Obervaciones para: " + instamceitemcount + " instacias seleccionadas.", "Aceptar");
            }
        }

        private void ImageButtonLogToolBarAction_6_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as ProcessMonitorThreeViewModel;
            int instamceitemcount = 0;
            if (commandItem != null)
            {
                foreach (CommandItem item in commandItem.CommandItems)
                {
                    if (item.IsChecked) { instamceitemcount += 1; }
                }
                if (instamceitemcount <= 1)
                {
                    Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más bitácoras", "Aceptar");
                    return;
                }
                Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Notificaciones para: " + instamceitemcount + " instancias seleccionadas.", "Aceptar");
            }
        }

        private void ImageButtonLogToolBarAction_7_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as ProcessMonitorThreeViewModel;
            int instamceitemcount = 0;
            if (commandItem != null)
            {
                foreach (CommandItem item in commandItem.CommandItems)
                {
                    if (item.IsChecked) { instamceitemcount += 1; }
                }
                if (instamceitemcount <= 1)
                {
                    Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más bitácoras", "Aceptar");
                    return;
                }
                Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Lote / Comando para: " + instamceitemcount + " instancias seleccionadas.", "Aceptar");
            }
        }

        private void ImageButtonLogToolBarAction_8_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as ProcessMonitorThreeViewModel;
            int instamceitemcount = 0;
            if (commandItem != null)
            {
                foreach (CommandItem item in commandItem.CommandItems)
                {
                    if (item.IsChecked) { instamceitemcount += 1; }
                }
                if (instamceitemcount <= 1)
                {
                    Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más bitácoras", "Aceptar");
                    return;
                }
                Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Lote / Comando para: " + instamceitemcount + " instancias seleccionadas.", "Aceptar");
            }
        }

        private void ImageButtonLogToolBarAction_9_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as ProcessMonitorThreeViewModel;
            int instamceitemcount = 0;
            if (commandItem != null)
            {
                foreach (CommandItem item in commandItem.CommandItems)
                {
                    if (item.IsChecked) { instamceitemcount += 1; }
                }
                if (instamceitemcount <= 1)
                {
                    Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar dos o más bitácoras", "Aceptar");
                    return;
                }
                Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Lote / Comando para: " + instamceitemcount + " instancias seleccionadas.", "Aceptar");
            }
        }
    }
}