using System.Threading.Tasks;
using Acr.UserDialogs;

namespace AST_ProBatch_Mobile.Utilities
{
    public class Alert
    {
        public static async void Show(string message)
        {
            await UserDialogs.Instance.AlertAsync(message, "AST●ProBatch®", "Cerrar");
        }

        public static async void Show(string message, string buttontext)
        {
            await UserDialogs.Instance.AlertAsync(message, "AST●ProBatch®", buttontext);
        }
    }

    public class Confirm
    {
        public static Task<bool> Show(string message)
        {
            return UserDialogs.Instance.ConfirmAsync(message, "AST●ProBatch®", "Si", "No");
        }

        public static Task<bool> Show(string message, string okbuttonmessage, string cancelbuttonmessage)
        {
            return UserDialogs.Instance.ConfirmAsync(message, "AST●ProBatch®", okbuttonmessage, cancelbuttonmessage);
        }
    }

    public static class StatusColor
    {
        public static string Green { get { return "#33CC33"; } }
        public static string Red { get { return "#FE6347"; } }
        public static string Orange { get { return "#FDA600"; } }
        public static string Yellow { get { return "#FFFF00"; } }
        public static string White { get { return "#FFFFFF"; } }
        public static string Grey { get { return "#7F7F7F"; } }
        public static string Purple { get { return "#B9908E"; } }
        public static string Blue { get { return "#87CEFA"; } }
    }
}
