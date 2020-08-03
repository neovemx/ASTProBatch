using Acr.UserDialogs;

namespace AST_ProBatch_Mobile.Utilities
{
    public class Toast
    {
        public static void ShowMessage(string message)
        {
            ToastConfig toastConfig = new ToastConfig(message);
            toastConfig.BackgroundColor = System.Drawing.Color.LightGray;
            toastConfig.MessageTextColor = System.Drawing.Color.Black;
            UserDialogs.Instance.Toast(toastConfig);
        }

        public static void ShowAlert(string message)
        {
            ToastConfig toastConfig = new ToastConfig(message);
            toastConfig.BackgroundColor = System.Drawing.Color.Orange;
            toastConfig.MessageTextColor = System.Drawing.Color.White;
            UserDialogs.Instance.Toast(toastConfig);
        }

        public static void ShowSuccess(string message)
        {
            ToastConfig toastConfig = new ToastConfig(message);
            toastConfig.BackgroundColor = System.Drawing.Color.LawnGreen;
            toastConfig.MessageTextColor = System.Drawing.Color.Black;
            UserDialogs.Instance.Toast(toastConfig);
        }

        public static void ShowError(string message)
        {
            ToastConfig toastConfig = new ToastConfig(message);
            toastConfig.BackgroundColor = System.Drawing.Color.Red;
            toastConfig.MessageTextColor = System.Drawing.Color.White;
            UserDialogs.Instance.Toast(toastConfig);
        }
    }
}
