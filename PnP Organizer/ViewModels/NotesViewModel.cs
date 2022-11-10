using CommunityToolkit.Mvvm.ComponentModel;
using PnP_Organizer.Models;
using System.Collections.ObjectModel;

namespace PnP_Organizer.ViewModels
{
    public partial class NotesViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<int> _editorFontSizes = new();

        [ObservableProperty]
        private ObservableCollection<ColorModel> _colors = new();

        private static readonly string[] _colorResources =
        {
            "TextFillColorPrimaryBrush",
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

        public NotesViewModel()
        {
            EditorFontSizes = new ObservableCollection<int>()
            {
                1,
                2,
                3,
                4,
                6,
                8,
                10,
                12,
                14,
                16,
                18,
                20,
                24,
                28,
                32
            };

            ObservableCollection<ColorModel> colorsCollection = new();
            foreach(string colorKey in _colorResources)
            {
                colorsCollection.Add(new ColorModel(colorKey));
            }
            Colors = colorsCollection;
        }
    }
}
