using PnP_Organizer.Controls.Events;
using PnP_Organizer.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PnP_Organizer.Controls
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        #region Events
        public event SelectedBrushChangedEventHandler? SelectedBrushChanged;
        public delegate void SelectedBrushChangedEventHandler (object sender, SelectedBrushChangedEventArgs e);
        #endregion Events

        #region Control Properties
        public static readonly DependencyProperty SelectedBrushProperty = DependencyProperty.Register(nameof(SelectedBrush), typeof(SolidColorBrush), 
            typeof(ColorPicker), new PropertyMetadata((SolidColorBrush)Application.Current.Resources["PaletteDefaultBrush"]));
        public SolidColorBrush SelectedBrush
        {
            get => (SolidColorBrush)GetValue(SelectedBrushProperty);
            set
            {
                SetValue(SelectedBrushProperty, value);
                SelectedBrushChanged?.Invoke(this, new SelectedBrushChangedEventArgs(value));
            }
        }

        /// <summary>
        /// Content of the button which opens the ColorPicker popup.
        /// </summary>
        public static readonly DependencyProperty ButtonContentProperty = DependencyProperty.Register(nameof(ButtonContent), typeof(object), typeof(ColorPicker));
        public object ButtonContent
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        /// <summary>
        /// Collection of resource keys for colors which the color picker will show as possible choices.
        /// Defaults to the WPFUI palette colors.
        /// </summary>
        public static readonly DependencyProperty ColorsSourceProperty = DependencyProperty.Register(nameof(ColorsSource), typeof(ObservableCollection<ColorModel>), typeof(ColorPicker));
        public ObservableCollection<ColorModel> ColorsSource
        {
            get => (ObservableCollection<ColorModel>)GetValue(ColorsSourceProperty);
            set => SetValue(ColorsSourceProperty, value);
        }

        private string _selectedBrushName = string.Empty;
        public string SelectedBrushName
        {
            get => _selectedBrushName;
            set => _selectedBrushName = value;
        }
        #endregion Control Properties

        #region Colors

        /// <summary>
        /// Default colors
        /// </summary>
        private static readonly string[] _paletteResources =
        {
            "PalettePrimaryBrush",
            "PaletteRedBrush",
            "PalettePinkBrush",
            "PalettePurpleBrush",
            "PaletteDeepPurpleBrush",
            "PaletteIndigoBrush",
            "PaletteBlueBrush",
            "PaletteLightBlueBrush",
            "PaletteCyanBrush",
            "PaletteTealBrush",
            "PaletteGreenBrush",
            "PaletteLightGreenBrush",
            "PaletteLimeBrush",
            "PaletteYellowBrush",
            "PaletteAmberBrush",
            "PaletteOrangeBrush",
            "PaletteDeepOrangeBrush",
            "PaletteBrownBrush",
            "PaletteGreyBrush",
            "PaletteBlueGreyBrush"
        };
        #endregion Colors

        public ColorPicker()
        {
            InitializeComponent();
            ObservableCollection<ColorModel> colorCollection = new();
            foreach (var colorKey in _paletteResources)
            {
                colorCollection.Add(new ColorModel(colorKey));
            }
            ColorsSource = colorCollection;
        }

        private void ColorPickerButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Wpf.Ui.Controls.Button)sender;
            var colorModel = (ColorModel)button.DataContext; 
            SelectedBrushName = colorModel.Name;
            SelectedBrush = (SolidColorBrush)colorModel.Brush;
        }
    }
}
