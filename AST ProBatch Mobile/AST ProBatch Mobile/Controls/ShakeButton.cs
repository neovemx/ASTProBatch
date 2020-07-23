using Xamarin.Forms;

namespace AST_ProBatch_Mobile.Controls
{
    public class ShakeButton : Xamarin.Forms.TriggerAction<Button>
    {
        protected async override void Invoke(Button button)
        {
            uint timeout = 50;
            await button.TranslateTo(-15, 0, timeout);
            await button.TranslateTo(15, 0, timeout);
            await button.TranslateTo(-10, 0, timeout);
            await button.TranslateTo(10, 0, timeout);
            await button.TranslateTo(-5, 0, timeout);
            await button.TranslateTo(5, 0, timeout);
            button.TranslationX = 0;
        }
    }
}
