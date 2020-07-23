using Acr.UserDialogs;

namespace ASTProBatchMobile.Utilities
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
}
