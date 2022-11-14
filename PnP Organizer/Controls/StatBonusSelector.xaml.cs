using PnP_Organizer.Controls.Events;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace PnP_Organizer.Controls
{
    /// <summary>
    /// Interaction logic for StatBonusSelector.xaml
    /// </summary>
    public partial class StatBonusSelector : UserControl
    {
        #region Events
        public event BonusValueChangedEventHandler? BonusValueChanged;
        public delegate void BonusValueChangedEventHandler(object sender, BonusValueChangedArgs e);
        #endregion Events

        #region DependencyProperties
        public static readonly DependencyProperty BonusValueProperty = DependencyProperty.Register(nameof(BonusValue), typeof(int),
            typeof(StatBonusSelector), new PropertyMetadata(0));
        public int BonusValue
        {
            get => (int)GetValue(BonusValueProperty);
            set
            {
                SetValue(BonusValueProperty, value);
                BonusValueChanged?.Invoke(this, new BonusValueChangedArgs(value));
            }
        }
        #endregion DependencyProperties

        public StatBonusSelector()
        {
            InitializeComponent();
        }

        private void NumBox_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            NumberBox numBox = (NumberBox)sender;

            if (numBox.Value > numBox.Max || numBox.Value < numBox.Min || e.Delta == 0)
                return;

            BonusValue = e.Delta > 0 ? (int)(numBox.Value + numBox.Step) : (int)(numBox.Value - numBox.Step);
        }
    }
}
