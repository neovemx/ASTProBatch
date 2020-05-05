using AST_ProBatch_Mobile.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class InstanceNotificationsViewModel : BaseViewModel
    {
        #region Atributes
        private ObservableCollection<NotificationItem> notificationitems;
        private InstanceItem instanceitem;
        private string instancetitle;
        #endregion

        #region Properties
        public InstanceItem InstanceItem
        {
            get { return instanceitem; }
            set { SetValue(ref instanceitem, value); }
        }

        public string InstanceTitle
        {
            get { return instancetitle; }
            set { SetValue(ref instancetitle, value); }
        }

        public ObservableCollection<NotificationItem> NotificationItems
        {
            get { return notificationitems; }
            set { SetValue(ref notificationitems, value); }
        }
        #endregion

        #region Constructors
        public InstanceNotificationsViewModel(InstanceItem instanceitem)
        {
            //TODO: Remplazar por el consumo de datos del webservices
            InstanceTitle = instanceitem.Title;
            GetFakeData(instanceitem.Id);
        }
        #endregion

        #region Commands
        public ICommand CloseCommand
        {
            get
            {
                return new RelayCommand(Close);
            }
        }

        private async void Close()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        #endregion

        #region Helpers
        private void GetFakeData(int Id)
        {
            string NotificationContent = "Prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje prueba del texto del mensaje.";
            NotificationItems = new ObservableCollection<NotificationItem>();
            NotificationItem notificationItem;

            switch (Id)
            {
                case 1:
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 1;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje A";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 2;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje B";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 3;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje C";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 4;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje D";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 5;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje E";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 6;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje F";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    break;
                case 2:
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 1;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje A";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 2;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje B";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 3;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje C";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 4;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje D";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 5;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje E";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 6;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje F";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    break;
                case 3:
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 1;
                    notificationItem.IsReaded = false;
                    notificationItem.NotificationTitle = "Título de mensaje A";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification_n";
                    notificationItem.StatusColor = "LightGreen";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 2;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje B";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 3;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje C";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 4;
                    notificationItem.IsReaded = false;
                    notificationItem.NotificationTitle = "Título de mensaje D";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification_n";
                    notificationItem.StatusColor = "LightGreen";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 5;
                    notificationItem.IsReaded = false;
                    notificationItem.NotificationTitle = "Título de mensaje E";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification_n";
                    notificationItem.StatusColor = "LightGreen";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 6;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje F";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    break;
                case 4:
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 1;
                    notificationItem.IsReaded = false;
                    notificationItem.NotificationTitle = "Título de mensaje A";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification_n";
                    notificationItem.StatusColor = "LightGreen";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 2;
                    notificationItem.IsReaded = false;
                    notificationItem.NotificationTitle = "Título de mensaje B";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification_n";
                    notificationItem.StatusColor = "LightGreen";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 3;
                    notificationItem.IsReaded = false;
                    notificationItem.NotificationTitle = "Título de mensaje C";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification_n";
                    notificationItem.StatusColor = "LightGreen";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 4;
                    notificationItem.IsReaded = false;
                    notificationItem.NotificationTitle = "Título de mensaje D";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification_n";
                    notificationItem.StatusColor = "LightGreen";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 5;
                    notificationItem.IsReaded = false;
                    notificationItem.NotificationTitle = "Título de mensaje E";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification_n";
                    notificationItem.StatusColor = "LightGreen";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 6;
                    notificationItem.IsReaded = false;
                    notificationItem.NotificationTitle = "Título de mensaje F";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification_n";
                    notificationItem.StatusColor = "LightGreen";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    break;
                case 5:
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 1;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje A";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 2;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje B";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 3;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje C";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 4;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje D";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 5;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje E";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    //*********
                    notificationItem = new NotificationItem();
                    notificationItem.Id = 6;
                    notificationItem.IsReaded = true;
                    notificationItem.NotificationTitle = "Título de mensaje F";
                    notificationItem.NotificationText = NotificationContent;
                    notificationItem.State = "notification";
                    notificationItem.StatusColor = "LightBlue";
                    notificationItem.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    NotificationItems.Add(notificationItem);
                    break;
            }
        }
        #endregion
    }
}
