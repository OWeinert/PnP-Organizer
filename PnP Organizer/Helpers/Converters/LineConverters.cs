using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace PnP_Organizer.Helpers.Converters
{
    public class HorizontalLineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TreeViewItem item = (TreeViewItem)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
            int index = ic.ItemContainerGenerator.IndexFromContainer(item);

            if ((string)parameter == "left")
            {
                // Either left most or single item
                if (index == 0)
                    return 0;
                else
                    return 1;
            }
            else // assume "right"
            {
                // Either right most or single item
                if (index == ic.Items.Count - 1)
                    return 0;
                else
                    return 1;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
    }

    public class VerticalLineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TreeViewItem item = (TreeViewItem)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);

            if ((string)parameter == "top")
            {
                if (ic is TreeView)
                    return 0;
                else
                    return 1;
            }
            else // assume "bottom"
            {
                if (item.HasItems == false)
                    return 0;
                else
                    return 1;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
    }
}
