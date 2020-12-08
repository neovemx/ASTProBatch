using Acr.UserDialogs;
//using AST_ProBatch_Mobile.Interfaces;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Utilities;
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
                var result = await this.DisplayAlert("AST●ProBatch®", "Pausar el: " + commandItem.NameCommand + "?", "Sí", "No");
                Toast.ShowMessage("Procesando...");
                //if (result) DependencyService.Get<Toast>().Show("Procesando...");
            }
        }

        private void ImageButtonState_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().StatusInfoExecutionStageThree = new StatusInfoExecutionStageThreeViewModel();
            Application.Current.MainPage.Navigation.PushModalAsync(new StatusInfoExecutionStageThreePage());
        }

        private void ImageButtonAction_1_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("AST●ProBatch®", "Ejecutar para: " + commandItem.NameCommand + "?", "Sí", "No");
                    Toast.ShowMessage("Procesando...");
                    //if (result) DependencyService.Get<Toast>().Show("Procesando...");
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
                    var result = await this.DisplayAlert("AST●ProBatch®", "Ver resultados para: " + commandItem.NameCommand + "?", "Sí", "No");
                    Toast.ShowMessage("Procesando...");
                    //if (result) DependencyService.Get<Toast>().Show("Procesando...");
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
                    var result = await this.DisplayAlert("AST●ProBatch®", "Matar proceso para: " + commandItem.NameCommand + "?", "Sí", "No");
                    Toast.ShowMessage("Procesando...");
                    //if (result) DependencyService.Get<Toast>().Show("Procesando...");
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
                    var result = await this.DisplayAlert("AST●ProBatch®", "Reejecutar: " + commandItem.NameCommand + "?", "Sí", "No");
                    Toast.ShowMessage("Procesando...");
                    //if (result) DependencyService.Get<Toast>().Show("Procesando...");
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
                //Device.BeginInvokeOnMainThread(async () =>
                //{
                //    var result = await this.DisplayAlert("AST●ProBatch®", "Datos del comando: " + commandItem.NameCommand + "?", "Sí", "No");
                //    Toast.ShowMessage("Procesando...");
                //});
                MainViewModel.GetInstance().CommandData = new CommandDataViewModel(true, commandItem);
                Application.Current.MainPage.Navigation.PushAsync(new CommandDataPage());
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
                    var result = await this.DisplayAlert("AST●ProBatch®", "Reconectar: " + commandItem.NameCommand + "?", "Sí", "No");
                    Toast.ShowMessage("Procesando...");
                    //if (result) DependencyService.Get<Toast>().Show("Procesando...");
                });
            }
        }

        private void ImageButtonAction_8_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                LotAndCommandObservation lotAndCommandObservation = new LotAndCommandObservation()
                {
                    HasData = true,
                    IdInstance = commandItem.InstanceItem.IdInstance,
                    IdLot = commandItem.IdLot,
                    IdCommand = commandItem.IdCommand
                };
                MainViewModel.GetInstance().LogObservations = new LogObservationsViewModel(true, lotAndCommandObservation, commandItem.InstanceItem.LogItem);
                Application.Current.MainPage.Navigation.PushAsync(new LogObservationsPage());
            }
        }

        private void ImageButtonAction_9_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                MainViewModel.GetInstance().Dependencies = new DependenciesViewModel(true, commandItem);
                Application.Current.MainPage.Navigation.PushAsync(new DependenciesPage());
                //Device.BeginInvokeOnMainThread(async () =>
                //{
                //    var result = await this.DisplayAlert("AST●ProBatch®", "Dependencias para: " + commandItem.NameCommand + "?", "Sí", "No");
                //    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                //});
            }
        }

        private void ImageButtonAction_10_Clicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var commandItem = imageButton.CommandParameter as CommandItem;
            if (commandItem != null)
            {
                MainViewModel.GetInstance().BatchQuery = new BatchQueryViewModel(true, commandItem);
                Application.Current.MainPage.Navigation.PushAsync(new BatchQueryPage());
                //Device.BeginInvokeOnMainThread(async () =>
                //{
                //    var result = await this.DisplayAlert("AST●ProBatch®", "Datos del Lote para: " + commandItem.NameCommand + "?", "Sí", "No");
                //    if (result) DependencyService.Get<Toast>().Show("Procesando...");
                //});
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

        private void ToolbarSearch_Clicked(Object sender, EventArgs e)
        {
            try
            {
                
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var toolbarItem = (ToolbarItem)sender;
                    var contextPage = toolbarItem.CommandParameter as ExecutionStageThreeViewModel;
                    CommandItem commandItem = new CommandItem();
                    LotAndCommandFinder lotAndCommandFinder = new LotAndCommandFinder();
                    PromptConfig promptConfig = new PromptConfig();
                    promptConfig.SetTitle("Criterios de Búsqueda");
                    promptConfig.SetCancelText("Cancelar");
                    promptConfig.SetOkText("Buscar");
                    promptConfig.InputType = InputType.Default;
                    promptConfig.SetMessage("Puede ingresar el nombre completo o parte del mismo de un lote o comando a buscar así como una combinación de ambos separados por ':'");
                    promptConfig.SetPlaceholder("1 - LOTE : 1 - COMANDO");
                    promptConfig.IsCancellable = true;
                    var criteriaValues = await UserDialogs.Instance.PromptAsync(promptConfig);
                    if (criteriaValues.Ok)
                    {
                        if (criteriaValues.Text.Contains(":"))
                        {
                            if (criteriaValues.Text.Split(':').Length > 2)
                            {
                                Alert.Show("Sólo debe ingresar una vez el separador ':'");
                                return;
                            }
                            if (string.IsNullOrEmpty(criteriaValues.Text.Split(':')[0]))
                            {
                                Alert.Show("Debe ingresar un criterio antes del separador de búsqueda!");
                                return;
                            }
                            if (string.IsNullOrWhiteSpace(criteriaValues.Text.Split(':')[0]))
                            {
                                Alert.Show("Debe ingresar un criterio antes del separador de búsqueda!");
                                return;
                            }
                            if (string.IsNullOrEmpty(criteriaValues.Text.Split(':')[1]))
                            {
                                Alert.Show("Debe ingresar un criterio después del separador de búsqueda!");
                                return;
                            }
                            if (string.IsNullOrWhiteSpace(criteriaValues.Text.Split(':')[1]))
                            {
                                Alert.Show("Debe ingresar un criterio después del separador de búsqueda!");
                                return;
                            }
                            lotAndCommandFinder.BothCriteria = true;
                            lotAndCommandFinder.CriteriaA = criteriaValues.Text.Split(':')[0];
                            lotAndCommandFinder.CriteriaB = criteriaValues.Text.Split(':')[1];
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(criteriaValues.Text))
                            {
                                Alert.Show("Debe ingresar un criterio de búsqueda!");
                                return;
                            }
                            if (string.IsNullOrWhiteSpace(criteriaValues.Text))
                            {
                                Alert.Show("Debe ingresar un criterio de búsqueda!");
                                return;
                            }
                            lotAndCommandFinder.BothCriteria = false;
                            lotAndCommandFinder.Criteria = criteriaValues.Text;
                        }
                        if (lotAndCommandFinder.BothCriteria)
                        {
                            foreach (CommandItem item in contextPage.CommandItems)
                            {
                                if (item.NameLot.ToUpper().Contains(lotAndCommandFinder.CriteriaA.ToUpper()) && item.NameCommand.ToUpper().Contains(lotAndCommandFinder.CriteriaB.ToUpper()))
                                {
                                    commandItem = item;
                                    InstanceItemsListView.ScrollTo(commandItem, ScrollToPosition.Start, true);
                                    return;
                                }
                            }
                            Alert.Show("No se encontro el registro");
                        }
                        else
                        {
                            foreach (CommandItem item in contextPage.CommandItems)
                            {
                                if (item.NameLot.ToUpper().Contains(lotAndCommandFinder.Criteria.ToUpper()) || item.NameCommand.ToUpper().Contains(lotAndCommandFinder.Criteria.ToUpper()))
                                {
                                    commandItem = item;
                                    InstanceItemsListView.ScrollTo(commandItem, ScrollToPosition.Start, true);
                                    return;
                                }
                            }
                            Alert.Show("No se encontro el registro");
                        }
                    }
                });
            }
            catch //(Exception ex)
            {
                Alert.Show("Ocurrió un error", "Aceptar");
            }
        }
    }
}