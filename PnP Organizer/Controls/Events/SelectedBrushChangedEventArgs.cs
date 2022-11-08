using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PnP_Organizer.Controls.Events
{
    /// <summary>
    /// EventArgs for the SelectedBrushChangedEventHandler
    /// </summary>
    public class SelectedBrushChangedEventArgs
    {
        /// <summary>
        /// The now selected Brush after the event is invoked
        /// </summary>
        public SolidColorBrush SelectedBrush { get; set; }

        public SelectedBrushChangedEventArgs(SolidColorBrush selectedBrush)
        {
            SelectedBrush = selectedBrush;
        }
    }
}
