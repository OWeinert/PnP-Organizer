using PnP_Organizer.Models;
using System.Windows;
using System.Windows.Controls;

namespace PnP_Organizer.Helpers
{
    public class ProfessionModelTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = (FrameworkElement)container;

            if (item is AddProfessionModel)
                return (DataTemplate)element.FindResource("AddProfessionModelTemplate");
            if (item is AttributeTestSkillModel)
                return (DataTemplate)element.FindResource("AttributeTestSkillModelTemplate");
            return (DataTemplate)element.FindResource("ProfessionModelTemplate");
        }
    }
}
