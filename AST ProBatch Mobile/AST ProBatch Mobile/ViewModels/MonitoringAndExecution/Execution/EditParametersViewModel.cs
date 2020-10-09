using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Acr.UserDialogs;
using AST_ProBatch_Mobile.Models;
using AST_ProBatch_Mobile.Utilities;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class EditParametersViewModel : BaseViewModel
    {
        #region Atributes
        private CommandItem commanditem;
        private ObservableCollection<ParameterItem> parameteritems;
        private ParameterItem selectedparameter;
        private string titleparameter;
        private string valueparameter;
        private bool isenabled;
        private bool isloadingdata;
        private string parameterselectedtitleenvironment; //ParameterSelectedTitleEnvironment
        private string parameterselectedtitleUser; //ParameterSelectedTitleUser
        #endregion

        #region Properties
        public CommandItem CommandItem
        {
            get { return commanditem; }
            set { SetValue(ref commanditem, value); }
        }
        public ObservableCollection<ParameterItem> ParameterItems
        {
            get { return parameteritems; }
            set { SetValue(ref parameteritems, value); }
        }
        public ParameterItem SelectedParameter
        {
            get { return selectedparameter; }
            set
            {
                SetValue(ref selectedparameter, value);
                HandleSelectedParameter();
            }
        }
        public string TitleParameter
        {
            get { return titleparameter; }
            set { SetValue(ref titleparameter, value); }
        }
        public string ValueParameter
        {
            get { return valueparameter; }
            set { SetValue(ref valueparameter, value); }
        }
        public bool IsEnabled
        {
            get { return isenabled; }
            set { SetValue(ref isenabled, value); }
        }
        public bool IsLoadingData
        {
            get { return isloadingdata; }
            set { SetValue(ref isloadingdata, value); }
        }
        public string ParameterSelectedTitleEnvironment
        {
            get { return parameterselectedtitleenvironment; }
            set { SetValue(ref parameterselectedtitleenvironment, value); }
        }
        public string ParameterSelectedTitleUser
        {
            get { return parameterselectedtitleUser; }
            set { SetValue(ref parameterselectedtitleUser, value); }
        }
        #endregion

        #region Constructors
        public EditParametersViewModel(CommandItem commadItem)
        {
            this.CommandItem = commadItem;
            this.GetFakeData();
        }
        #endregion

        #region Commands
        public ICommand NewParameterCommand
        {
            get
            {
                return new RelayCommand(New);
            }
        }

        private async void New()
        {
            this.TitleParameter = string.Empty;
            this.ValueParameter = string.Empty;
            this.ParameterSelectedTitleEnvironment = string.Empty;
            this.ParameterSelectedTitleUser = string.Empty;
            this.SelectedParameter = new ParameterItem();
        }

        public ICommand SaveParameterCommand
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
                if (String.IsNullOrWhiteSpace(this.TitleParameter))
                {
                    Alert.Show("Debe ingresar un nombre para el parámetro");
                    return;
                }

                if (String.IsNullOrWhiteSpace(this.ValueParameter))
                {
                    Alert.Show("Debe ingresar el valor para el parámetro");
                    return;
                }

                bool result = await Application.Current.MainPage.DisplayAlert("AST●ProBatch®", "Desea guardar los cambios del parámetro?", "Si", "No");
                if (result)
                {
                    UserDialogs.Instance.ShowLoading("Cargando...", MaskType.Black);
                    IsLoadingData = true;
                    ObservableCollection<ParameterItem> observationItemsTemp = new ObservableCollection<ParameterItem>();
                    foreach (ParameterItem item in ParameterItems)
                    {
                        observationItemsTemp.Add(item);
                    }

                    foreach (ParameterItem item in observationItemsTemp)
                    {
                        //if (item.Id == this.SelectedParameter.Id)
                        //{
                        //    int oldIndexItem = ParameterItems.IndexOf(item);
                        //    ParameterItem tempItem = item;
                        //    tempItem.Title = this.TitleParameter;
                        //    tempItem.Value = this.ValueParameter;
                        //    ParameterItems.Remove(item);
                        //    ParameterItems.Add(tempItem);
                        //    int newIndexItem = ParameterItems.IndexOf(tempItem);
                        //    ParameterItems.Move(newIndexItem, oldIndexItem);
                        //}
                    }
                    this.TitleParameter = string.Empty;
                    this.ValueParameter = string.Empty;
                    Toast.ShowSuccess("Parámetro actualizado!!");
                    this.SelectedParameter = new ParameterItem();
                    IsLoadingData = false;
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Ocurrió un error", "Aceptar");
            }
        }
        #endregion

        #region Helpers
        private void GetFakeData()
        {
            this.ParameterItems = new ObservableCollection<ParameterItem>();
            ParameterItem parameterItem;

            //parameterItem = new ParameterItem();
            //parameterItem.Id = 1;
            //parameterItem.Title = "PARAMETRO PRUEBA";
            //parameterItem.Value = "valor prueba";
            //parameterItem.IdEnvironment = 1;
            //parameterItem.TitleEnvironment = "Windows";
            //parameterItem.IdUser = 1;
            //parameterItem.TitleUser = "ADMINISTRADOR WEB";
            //this.ParameterItems.Add(parameterItem);

            //parameterItem = new ParameterItem();
            //parameterItem.Id = 2;
            //parameterItem.Title = "PARAMETRO PRUEBA";
            //parameterItem.Value = "valor prueba";
            //parameterItem.IdEnvironment = 1;
            //parameterItem.TitleEnvironment = "Windows";
            //parameterItem.IdUser = 1;
            //parameterItem.TitleUser = "ADMINISTRADOR WEB";
            //this.ParameterItems.Add(parameterItem);

            //parameterItem = new ParameterItem();
            //parameterItem.Id = 3;
            //parameterItem.Title = "PARAMETRO PRUEBA";
            //parameterItem.Value = "valor prueba";
            //parameterItem.IdEnvironment = 1;
            //parameterItem.TitleEnvironment = "Windows";
            //parameterItem.IdUser = 1;
            //parameterItem.TitleUser = "ADMINISTRADOR WEB";
            //this.ParameterItems.Add(parameterItem);

            //parameterItem = new ParameterItem();
            //parameterItem.Id = 4;
            //parameterItem.Title = "PARAMETRO PRUEBA";
            //parameterItem.Value = "valor prueba";
            //parameterItem.IdEnvironment = 1;
            //parameterItem.TitleEnvironment = "Windows";
            //parameterItem.IdUser = 1;
            //parameterItem.TitleUser = "ADMINISTRADOR WEB";
            //this.ParameterItems.Add(parameterItem);

            //parameterItem = new ParameterItem();
            //parameterItem.Id = 5;
            //parameterItem.Title = "PARAMETRO PRUEBA";
            //parameterItem.Value = "valor prueba";
            //parameterItem.IdEnvironment = 1;
            //parameterItem.TitleEnvironment = "Windows";
            //parameterItem.IdUser = 1;
            //parameterItem.TitleUser = "ADMINISTRADOR WEB";
            //this.ParameterItems.Add(parameterItem);

            //parameterItem = new ParameterItem();
            //parameterItem.Id = 6;
            //parameterItem.Title = "PARAMETRO PRUEBA";
            //parameterItem.Value = "valor prueba";
            //parameterItem.IdEnvironment = 1;
            //parameterItem.TitleEnvironment = "Windows";
            //parameterItem.IdUser = 1;
            //parameterItem.TitleUser = "ADMINISTRADOR WEB";
            //this.ParameterItems.Add(parameterItem);

            //parameterItem = new ParameterItem();
            //parameterItem.Id = 7;
            //parameterItem.Title = "PARAMETRO PRUEBA";
            //parameterItem.Value = "valor prueba";
            //parameterItem.IdEnvironment = 1;
            //parameterItem.TitleEnvironment = "Windows";
            //parameterItem.IdUser = 1;
            //parameterItem.TitleUser = "ADMINISTRADOR WEB";
            //this.ParameterItems.Add(parameterItem);
        }

        private void HandleSelectedParameter()
        {
            //this.TitleParameter = this.SelectedParameter.Title;
            //this.ValueParameter = this.SelectedParameter.Value;
            //this.ParameterSelectedTitleEnvironment = this.SelectedParameter.TitleEnvironment;
            //this.ParameterSelectedTitleUser = this.SelectedParameter.TitleUser;
            //this.IsEnabled = true;
        }
        #endregion
    }
}
