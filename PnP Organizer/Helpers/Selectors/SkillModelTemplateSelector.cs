using PnP_Organizer.Models;
using System.Windows;
using System.Windows.Controls;

namespace PnP_Organizer.Helpers.Selectors
{
    public class SkillModelTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = (FrameworkElement)container;

            if (item is RepeatableSkillModel)
                return (DataTemplate)element.FindResource("RepeatableSkillModelTemplate");
            return (DataTemplate)element.FindResource("SkillModelTemplate");
        }
    }
}
