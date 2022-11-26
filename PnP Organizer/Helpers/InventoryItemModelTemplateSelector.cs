using PnP_Organizer.Models;
using System.Windows;
using System.Windows.Controls;

namespace PnP_Organizer.Helpers
{
    public class InventoryItemModelTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = (FrameworkElement)container;

            if (item is InventoryWeaponModel)
                return (DataTemplate)element.FindResource("InventoryWeaponModelTemplate");
            else if (item is InventoryArmorModel)
                return (DataTemplate)element.FindResource("InventoryArmorModelTemplate");
            return (DataTemplate)element.FindResource("InventoryItemModelTemplate");
        }
    }
}
