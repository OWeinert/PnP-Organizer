using PnP_Organizer.Models;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace PnP_Organizer.Helpers
{
    public class SkillModelTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = (FrameworkElement)container;

            if (item is RepeatableSkillModel) 
                return (DataTemplate)element.FindResource("RepeatableSkillModelTemplate");
            return (DataTemplate)element.FindResource("SkillModelTemplate");
        }
    }
}
