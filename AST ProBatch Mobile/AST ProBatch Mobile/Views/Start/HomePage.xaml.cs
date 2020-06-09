using Acr.UserDialogs;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AST_ProBatch_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        #region Properties
        private DateTime validTimeMinute;
        private int validTimeSeconds;
        private int backPressCount;

        public DateTime ValidTimeMinute
        {
            get { return validTimeMinute; }
            set { validTimeMinute = value; }
        }
        public int BackPressCount
        {
            get { return backPressCount; }
            set { backPressCount = value; }
        }
        public int ValidTimeSeconds
        {
            get { return validTimeSeconds; }
            set { validTimeSeconds = value; }
        }
        #endregion

        public HomePage()
        {
            InitializeComponent();
            BackPressCount = 0;
            ValidTimeMinute = DateTime.Now;
            ValidTimeSeconds = DateTime.Now.Second;
        }

        protected override bool OnBackButtonPressed()
        {
            bool resultExit = true;
            BackPressCount += 1;
            if (BackPressCount < 3)
            {
                if ((DateTime.Now.Minute - ValidTimeMinute.Minute) == 0)
                {
                    if ((DateTime.Now.Second - ValidTimeSeconds) < 5)
                    {
                        switch (BackPressCount)
                        {
                            case 1:
                                //UserDialogs.Instance.Toast("Presiona dos veces más para salir de la aplicación!!");
                                ShowToast("Presiona dos veces más para salir de la aplicación!!");
                                break;
                            case 2:
                                //UserDialogs.Instance.Toast("Presiona una vez más para salir de la aplicación!!");
                                ShowToast("Presiona una vez más para salir de la aplicación!!");
                                break;
                        }
                    }
                    else
                    {
                        ValidTimeMinute = DateTime.Now;
                        ValidTimeSeconds = DateTime.Now.Second;
                        backPressCount = 1;
                        //UserDialogs.Instance.Toast("Presiona dos veces más para salir de la aplicación!!");
                        ShowToast("Presiona dos veces más para salir de la aplicación!!");
                    }
                }
                else
                {
                    ValidTimeMinute = DateTime.Now;
                    ValidTimeSeconds = DateTime.Now.Second;
                    backPressCount = 1;
                    //UserDialogs.Instance.Toast("Presiona dos veces más para salir de la aplicación!!");
                    ShowToast("Presiona dos veces más para salir de la aplicación!!");
                }
            }
            else
            {
                if (backPressCount == 3)
                {
                    if ((DateTime.Now.Minute - ValidTimeMinute.Minute) == 0)
                    {
                        if ((DateTime.Now.Second - ValidTimeSeconds) < 5)
                        {
                            resultExit = false;
                        }
                        else
                        {
                            ValidTimeMinute = DateTime.Now;
                            ValidTimeSeconds = DateTime.Now.Second;
                            backPressCount = 1;
                            //UserDialogs.Instance.Toast("Presiona dos veces más para salir de la aplicación!!");
                            ShowToast("Presiona dos veces más para salir de la aplicación!!");
                        }
                    }
                    else
                    {
                        ValidTimeMinute = DateTime.Now;
                        ValidTimeSeconds = DateTime.Now.Second;
                        backPressCount = 1;
                        //UserDialogs.Instance.Toast("Presiona dos veces más para salir de la aplicación!!");
                        ShowToast("Presiona dos veces más para salir de la aplicación!!");
                    }
                }
            }

            return resultExit;

            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    var result = await this.DisplayAlert("AST●ProBatch®", "Desea salir de la aplicación?", "Sí", "No");
            //    if (result) await this.Navigation.PopAsync();
            //});

            //return true;
        }

        void ShowToast(string message)
        {
            ToastConfig toastConfig = new ToastConfig(message);
            toastConfig.BackgroundColor = System.Drawing.Color.LightGray;
            toastConfig.MessageTextColor = System.Drawing.Color.Black;
            UserDialogs.Instance.Toast(toastConfig);
        }
    }
}