using CommunityToolkit.Mvvm.Input;
using PnP_Organizer.Views;
using System.Windows.Input;

namespace PnP_Organizer.Core
{
    public static class Commands
    {
        public static readonly RoutedUICommand SaveCharacter = new("Initiates Character saving process", "SaveCharacter", typeof(Commands));
        public static readonly RoutedUICommand LoadCharacter = new("Initiates Character loading process", "LoadCharacter", typeof(Commands));
        public static readonly RoutedUICommand NewCharacter = new("Initiates new Character creation", "NewCharacter", typeof(Commands));
    }
}
