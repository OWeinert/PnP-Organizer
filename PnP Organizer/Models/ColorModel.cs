using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Wpf.Ui.Common;

namespace PnP_Organizer.Models
{
    /// <summary>
    /// Data for ColorPicker colors
    /// </summary>
    public struct ColorModel
    {
        public string Name { get; set; }
        public Brush Brush { get; set; }
        public string BrushKey { get; set; }
        /// <summary>
        /// Creates a new ColorModel from the given WPFUI <paramref name="paletteBrushKey"/> resource key.
        /// </summary>
        /// <param name="paletteBrushKey"></param>
        public ColorModel(string paletteBrushKey)
        {
            BrushKey = paletteBrushKey;
            Brush = (Brush)Application.Current.Resources[paletteBrushKey];
            Name = BrushKey.Replace("Palette", "").Replace("Brush", "");
        }
    }
}
