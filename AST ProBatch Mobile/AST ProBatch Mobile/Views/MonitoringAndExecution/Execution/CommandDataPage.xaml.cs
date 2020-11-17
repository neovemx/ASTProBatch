using Xamarin.Forms;

namespace AST_ProBatch_Mobile.Views
{
    public partial class CommandDataPage : ContentPage
    {
        public CommandDataPage()
        {
            InitializeComponent();
        }

        void Module_Start_Clicked(System.Object sender, System.EventArgs e)
        {
            ImageButton imageButton = sender as ImageButton;
            ToolBar.ScrollToAsync(imageButton, ScrollToPosition.Start, true);
        }

        void Module_Visible_Clicked(System.Object sender, System.EventArgs e)
        {
            ImageButton imageButton = sender as ImageButton;
            ToolBar.ScrollToAsync(imageButton, ScrollToPosition.MakeVisible, true);
        }

        void Module_End_Clicked(System.Object sender, System.EventArgs e)
        {
            ImageButton imageButton = sender as ImageButton;
            ToolBar.ScrollToAsync(imageButton, ScrollToPosition.End, true);
        }
    }
}
