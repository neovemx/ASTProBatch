using AST_ProBatch_Mobile.Droid.Implementations;
using AST_ProBatch_Mobile.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(Toast_Android))]

namespace AST_ProBatch_Mobile.Droid.Implementations
{
    public class Toast_Android : Toast
    {
        public void Show(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, Android.Widget.ToastLength.Long).Show();
        }
    }
}