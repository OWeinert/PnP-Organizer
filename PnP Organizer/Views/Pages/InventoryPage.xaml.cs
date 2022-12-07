using Microsoft.Win32;
using PnP_Organizer.Controls;
using PnP_Organizer.IO;
using PnP_Organizer.Models;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using WpfUiButton = Wpf.Ui.Controls.Button;

namespace PnP_Organizer.Views.Pages
{
    /// <summary>
    /// Interaction logic for InventoryPage.xaml
    /// </summary>
    public partial class InventoryPage : INavigableView<ViewModels.InventoryViewModel>
    {
        public ViewModels.InventoryViewModel ViewModel
        {
            get;
        }

        private readonly IDialogControl _dialogControl;

        private Point _dragStartPoint;
      
        public InventoryPage(ViewModels.InventoryViewModel viewModel, IDialogService dialogService)
        {
            ViewModel = viewModel;

            InitializeComponent();

            _dialogControl = dialogService.GetDialogControl();
        }

        #region Page Button Event Handlers
        // TODO AddItemButton_Click: move logic to ViewModel
        private void AddItemButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.Items?.Add(new InventoryItemModel());
        }
        #endregion Page Button Event Handlers

        #region InventoryItem Button Event Handlers
        // TODO ItemImageButton_Click: move logic to Model
        private void ItemImageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = (WpfUiButton)sender;
            var inventoryItem = (InventoryItemModel)button!.DataContext;

            OpenFileDialog openImageDialog = new()
            {
                Title = Properties.Resources.Dialog_OpenImage,
                Filter = FileIO.ImageFileExtensionFilter,
                InitialDirectory = Directory.GetCurrentDirectory()
            };
            if (openImageDialog.ShowDialog() == true)
            {
                using var fs = (FileStream)openImageDialog.OpenFile();
                BitmapImage image = new(new Uri(fs.Name, UriKind.Absolute));
                inventoryItem.ItemImage = image;
            }
        }

        // TODO ClearButton_Click: move logic to Model
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (WpfUiButton)sender;
            var inventoryItem = (InventoryItemModel)button!.DataContext;
            OpenClearItemDialog(inventoryItem);
        }

        // TODO DeleteButton_Click: move logic to Model
        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = (WpfUiButton)sender;
            var inventoryItem = (InventoryItemModel)button!.DataContext;
            OpenDeleteItemDialog(inventoryItem);
        }
        #endregion InventoryItem Button Event Handlers

        #region Dialogs
        // TODO OpenClearItemDialog: move logic to Model
        private async void OpenClearItemDialog(InventoryItemModel inventoryItem)
        {
            var dialog = (Dialog)_dialogControl;
            dialog.Tag = "clearItem";
            dialog.ButtonLeftName = Properties.Resources.Dialog_ButtonClear;
            dialog.ButtonRightName = Properties.Resources.Dialog_ButtonCancel;
            dialog.ButtonLeftAppearance = Wpf.Ui.Common.ControlAppearance.Caution;

            dialog.ButtonLeftClick += (sender, e) =>
            {
                if(((string)dialog.Tag) == "clearItem")
                {
                    var index = ViewModel.Items!.IndexOf(inventoryItem);
                    InventoryItemModel newInventoryItem = new()
                    {
                        Brush = inventoryItem.Brush
                    };
                    ViewModel.Items[index] = newInventoryItem;
                    dialog.Hide();
                }
            };

            var result = await dialog.ShowAndWaitAsync(Properties.Resources.Dialog_ItemClearingTitle, Properties.Resources.Dialog_ItemClearingMessage, true);
        }

        // TODO OpenDeleteItemDialog: move logic to Model
        private async void OpenDeleteItemDialog(InventoryItemModel inventoryItem)
        {
            var dialog = (Dialog)_dialogControl;
            dialog.Tag = "deleteItem";
            dialog.ButtonLeftName = Properties.Resources.Dialog_ButtonDelete;
            dialog.ButtonRightName = Properties.Resources.Dialog_ButtonCancel;
            dialog.ButtonLeftAppearance = Wpf.Ui.Common.ControlAppearance.Danger;

            dialog.ButtonLeftClick += (sender, e) =>
            {
                if (((string)dialog.Tag) == "deleteItem")
                {
                    ViewModel.Items?.Remove(inventoryItem);
                    dialog.Hide();
                }
            };

            var result = await dialog.ShowAndWaitAsync(Properties.Resources.Dialog_ItemDeletionTitle, Properties.Resources.Dialog_ItemDeletionMessage, true);
        }

        #endregion Message Box Button Event Handlers

        #region InventoryItem Drag Drop Control
        private void InventoryItemsControl_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(null);
            var diff = _dragStartPoint - point;
            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                System.Diagnostics.Debug.WriteLine(e.OriginalSource);
                var lbi = FindVisualParent<Border>(((DependencyObject)e.OriginalSource));
                if (lbi != null)
                    DragDrop.DoDragDrop(lbi, lbi.DataContext, DragDropEffects.Move);
            }
        }
        private void InventoryItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => _dragStartPoint = e.GetPosition(null);

        private void InventoryItem_Drop(object sender, DragEventArgs e)
        {
            if (sender is Border itemBorder)
            {
                var source = (InventoryItemModel)e.Data.GetData(typeof(InventoryItemModel));
                var target = (InventoryItemModel)itemBorder.DataContext;

                var sourceIndex = InventoryItemsControl.Items.IndexOf(source);
                var targetIndex = InventoryItemsControl.Items.IndexOf(target);

                Move(source, sourceIndex, targetIndex);
                ViewModel.ItemsView?.Refresh();
            }
        }

        private void Move(InventoryItemModel source, int sourceIndex, int targetIndex)
        {
            if (sourceIndex < targetIndex)
            {
                ViewModel.Items?.Insert(targetIndex + 1, source);
                ViewModel.Items?.RemoveAt(sourceIndex);
            }
            else
            {
                var removeIndex = sourceIndex + 1;
                if (ViewModel.Items?.Count + 1 > removeIndex)
                {
                    ViewModel.Items?.Insert(targetIndex, source);
                    ViewModel.Items?.RemoveAt(removeIndex);
                }
            }
        }

        private T? FindVisualParent<T>(DependencyObject child)
            where T : DependencyObject
        {
            if (child is T t)
                return t;
            var parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;
            if (parentObject is T parent)
                return parent;
            return FindVisualParent<T>(parentObject);
        }

        #endregion InventoryItem Drag Drop Control

        #region Color Picker
        private void ColorPicker_SelectedBrushChanged(object sender, Controls.Events.SelectedBrushChangedEventArgs e)
        {
            var colorPicker = (ColorPicker)sender;
            var itemModel = (InventoryItemModel)colorPicker.DataContext;
            itemModel.Brush = e.SelectedBrush;

            if (colorPicker.SelectedBrushName is "Red" or "Pink" or "Purple" or "DeepPurple" or "Indigo" or "DeepOrange" or "Brown" or "BlueGrey" or "Default")
                itemModel.Foreground = (SolidColorBrush)Application.Current.Resources["TextFillColorPrimaryBrush"];
            else 
                itemModel.Foreground = (SolidColorBrush)Application.Current.Resources["TextFillColorInverseBrush"];
            ViewModel.ItemsView?.Refresh();
        }

        #endregion Color Picker
    }
}