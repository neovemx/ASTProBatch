using AST_ProBatch_Mobile.Interfaces;
using AST_ProBatch_Mobile.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class LogObservationsViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<ObservationItem> observationitems;
        private string logtitle;
        private string name;
        private string details;
        private ObservationItem selectedobservation;
        private bool isloadingdata;
        #endregion

        #region Properties
        public ObservationItem SelectedObservation
        {
            get { return selectedobservation; }
            set
            {
                SetValue(ref selectedobservation, value);
                HandleSelectedObservation();
            }
        }
        public ObservableCollection<ObservationItem> ObservationItems
        {
            get { return observationitems; }
            set { SetValue(ref observationitems, value); }
        }

        public string LogTitle
        {
            get { return logtitle; }
            set { SetValue(ref logtitle, value); }
        }

        public string Name
        {
            get { return name; }
            set { SetValue(ref name, value); }
        }

        public string Details
        {
            get { return details; }
            set { SetValue(ref details, value); }
        }

        public bool IsLoadingData
        {
            get { return isloadingdata; }
            set { SetValue(ref isloadingdata, value); }
        }
        #endregion

        #region Constructors
        public LogObservationsViewModel(LogItem logitem)
        {
            this.LogTitle = logitem.Title;
            SelectedObservation = new ObservationItem();
            GetFakeData();
        }
        #endregion

        #region Commands
        public ICommand NewCommand
        {
            get
            {
                return new RelayCommand(New);
            }
        }

        private async void New()
        {
            this.Name = string.Empty;
            this.Details = string.Empty;
            this.SelectedObservation = new ObservationItem();
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            try
            {
                if (IsLoadingData)
                {
                    return;
                }
                if (String.IsNullOrWhiteSpace(this.Name))
                {
                    await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe ingresar un nombre para la observación", "Aceptar");
                    return;
                }

                if (String.IsNullOrWhiteSpace(this.Details))
                {
                    await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe ingresar un detalle para la observación", "Aceptar");
                    return;
                }

                bool result = await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Desea guardar los cambios de la observación", "Si", "No");
                if (result)
                {
                    IsLoadingData = true;
                    if (this.SelectedObservation.Id != 0)
                    {
                        ObservableCollection<ObservationItem> observationItemsTemp = new ObservableCollection<ObservationItem>();
                        foreach (ObservationItem item in ObservationItems)
                        {
                            observationItemsTemp.Add(item);
                        }

                        foreach (ObservationItem item in observationItemsTemp)
                        {
                            if (item.Id == this.SelectedObservation.Id)
                            {
                                int oldIndexItem = ObservationItems.IndexOf(item);
                                ObservationItem tempItem = item;
                                tempItem.Name = this.Name;
                                tempItem.Detail = this.Details;
                                tempItem.Date = DateTime.Now.ToString("dd/MM/yyyy");
                                ObservationItems.Remove(item);
                                ObservationItems.Add(tempItem);
                                int newIndexItem = ObservationItems.IndexOf(tempItem);
                                ObservationItems.Move(newIndexItem, oldIndexItem);
                            }
                        }
                        this.Name = string.Empty;
                        this.Details = string.Empty;
                        DependencyService.Get<Toast>().Show("Cambios a observación guardados!");
                        this.SelectedObservation = new ObservationItem();
                    }
                    else
                    {
                        ObservationItem observationItem;
                        observationItem = new ObservationItem();
                        observationItem.Id = ObservationItems.Count + 1;
                        observationItem.Name = this.Name;
                        observationItem.Detail = this.Details;
                        observationItem.Operator = "ADMINISTRADOR";
                        observationItem.Date = DateTime.Now.ToString("dd/MM/yyyy");
                        ObservationItems.Add(observationItem);

                        this.Name = string.Empty;
                        this.Details = string.Empty;
                        DependencyService.Get<Toast>().Show("Observación guardada!");
                        this.SelectedObservation = new ObservationItem();
                    }
                    IsLoadingData = false;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Ocurrió un error.", "Aceptar");
            }

        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }

        private async void Delete()
        {
            try
            {
                if (IsLoadingData)
                {
                    return;
                }
                if (this.SelectedObservation.Id == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Debe seleccionar una observación para eliminar.", "Aceptar");
                    return;
                }
                bool result = await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Desea eliminar la observación", "Si", "No");
                if (result)
                {
                    IsLoadingData = true;
                    ObservableCollection<ObservationItem> observationItemsTemp = new ObservableCollection<ObservationItem>();
                    foreach (ObservationItem item in ObservationItems)
                    {
                        observationItemsTemp.Add(item);
                    }

                    foreach (ObservationItem item in observationItemsTemp)
                    {
                        if (item.Id == this.SelectedObservation.Id)
                        {
                            ObservationItems.Remove(item);
                        }
                    }

                    this.Name = string.Empty;
                    this.Details = string.Empty;
                    DependencyService.Get<Toast>().Show("Observación eliminada!");
                    this.SelectedObservation = new ObservationItem();
                    IsLoadingData = false;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Ocurrió un error.", "Aceptar");
            }

        }
        #endregion

        #region Helpers
        private void HandleSelectedObservation()
        {
            this.Name = this.SelectedObservation.Name;
            this.Details = this.SelectedObservation.Detail;
        }
        #endregion

        #region Fake Data
        private void GetFakeData()
        {
            ObservationItems = new ObservableCollection<ObservationItem>();
            ObservationItem observationItem;

            observationItem = new ObservationItem();
            observationItem.Id = 1;
            observationItem.Name = "PRUEBA DE TITULO DE OBSERVACION 1";
            observationItem.Detail = "Contenido de la observación a detalle, texto texto datos datos valores valores.";
            observationItem.Operator = "ADMINISTRADOR";
            observationItem.Date = DateTime.Now.ToString("dd/MM/yyyy");
            ObservationItems.Add(observationItem);

            observationItem = new ObservationItem();
            observationItem.Id = 2;
            observationItem.Name = "PRUEBA DE TITULO DE OBSERVACION 2";
            observationItem.Detail = "Contenido de la observación a detalle, texto texto datos datos valores valores.";
            observationItem.Operator = "WEB APLLICATION";
            observationItem.Date = DateTime.Now.ToString("dd/MM/yyyy");
            ObservationItems.Add(observationItem);

            observationItem = new ObservationItem();
            observationItem.Id = 3;
            observationItem.Name = "PRUEBA DE TITULO DE OBSERVACION 3";
            observationItem.Detail = "Contenido de la observación a detalle, texto texto datos datos valores valores.";
            observationItem.Operator = "TEST AND BUILD";
            observationItem.Date = DateTime.Now.ToString("dd/MM/yyyy");
            ObservationItems.Add(observationItem);

            observationItem = new ObservationItem();
            observationItem.Id = 4;
            observationItem.Name = "PRUEBA DE TITULO DE OBSERVACION 4";
            observationItem.Detail = "Contenido de la observación a detalle, texto texto datos datos valores valores.";
            observationItem.Operator = "DEVELOPER";
            observationItem.Date = DateTime.Now.ToString("dd/MM/yyyy");
            ObservationItems.Add(observationItem);

            observationItem = new ObservationItem();
            observationItem.Id = 5;
            observationItem.Name = "PRUEBA DE TITULO DE OBSERVACION 5";
            observationItem.Detail = "Contenido de la observación a detalle, texto texto datos datos valores valores.";
            observationItem.Operator = "ADMINISTRADOR";
            observationItem.Date = DateTime.Now.ToString("dd/MM/yyyy");
            ObservationItems.Add(observationItem);

            observationItem = new ObservationItem();
            observationItem.Id = 6;
            observationItem.Name = "PRUEBA DE TITULO DE OBSERVACION 6";
            observationItem.Detail = "Contenido de la observación a detalle, texto texto datos datos valores valores.";
            observationItem.Operator = "ADMINISTRADOR";
            observationItem.Date = DateTime.Now.ToString("dd/MM/yyyy");
            ObservationItems.Add(observationItem);

            observationItem = new ObservationItem();
            observationItem.Id = 7;
            observationItem.Name = "PRUEBA DE TITULO DE OBSERVACION 7";
            observationItem.Detail = "Contenido de la observación a detalle, texto texto datos datos valores valores.";
            observationItem.Operator = "ADMINISTRADOR";
            observationItem.Date = DateTime.Now.ToString("dd/MM/yyyy");
            ObservationItems.Add(observationItem);
        }
        #endregion
    }
}
