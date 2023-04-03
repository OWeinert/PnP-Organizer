using PnP_Organizer.Models;
using System.Windows;
using System.Windows.Controls;

namespace PnP_Organizer.Helpers.Selectors
{
    public class InventoryItemModelTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = (FrameworkElement)container;

            if (item is InventoryWeaponModel)
                return (DataTemplate)element.FindResource("InventoryWeaponModelTemplate");
            if (item is InventoryArmorModel)
                return (DataTemplate)element.FindResource("InventoryArmorModelTemplate");
            if (item is InventoryShieldModel)
                return (DataTemplate)element.FindResource("InventoryShieldModelTemplate");
            return (DataTemplate)element.FindResource("InventoryItemModelTemplate");
        }
    }
}
