using AST_ProBatch_Mobile.Controls;
using AST_ProBatch_Mobile.iOS.Renderers;
using System.ComponentModel;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(StandardEntry), typeof(StandardEntryRenderer))]
namespace AST_ProBatch_Mobile.iOS.Renderers
{
    public class StandardEntryRenderer : EntryRenderer
    {
        public StandardEntry ElementV2 => Element as StandardEntry;
        public UITextFieldPadding ControlV2 => Control as UITextFieldPadding;

        protected override UITextField CreateNativeControl()
        {
            var control = new UITextFieldPadding(RectangleF.Empty)
            {
                Padding = ElementV2.Padding,
                BorderStyle = UITextBorderStyle.RoundedRect,
                ClipsToBounds = true
            };

            UpdateBackground(control);

            return control;
        }

        protected void UpdateBackground(UITextField control)
        {
            if (control == null) return;
            control.Layer.CornerRadius = ElementV2.CornerRadius;
            control.Layer.BorderWidth = ElementV2.BorderThickness;
            control.Layer.BorderColor = ElementV2.BorderColor.ToCGColor();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == StandardEntry.PaddingProperty.PropertyName)
            {
                UpdatePadding();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        protected void UpdatePadding()
        {
            if (Control == null)
                return;

            ControlV2.Padding = ElementV2.Padding;
        }
    }
}