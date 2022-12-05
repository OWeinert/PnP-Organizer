using PnP_Organizer.Models;
using System.Windows;
using System.Windows.Controls;

namespace PnP_Organizer.Helpers.Selectors
{
    public class ProfessionModelTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = (FrameworkElement)container;

            if (item is AddProfessionModel)
                return (DataTemplate)element.FindResource("AddProfessionModelTemplate");
            return (DataTemplate)element.FindResource("ProfessionModelTemplate");
        }
    }
}
