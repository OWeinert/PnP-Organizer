using System.Windows.Input;

namespace PnP_Organizer.Helpers
{
    public class FocusHelper
    {
        public static void ClearKeyboardFocusOnMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }
    }
}
