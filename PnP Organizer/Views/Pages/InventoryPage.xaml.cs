﻿using Microsoft.Win32;
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

        #region InventoryItem Button Event Handlers
        // TODO ItemImageButton_Click: move logic to Model
        private void ItemImageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WpfUiButton button = (WpfUiButton)sender;
            InventoryItemModel inventoryItem = (InventoryItemModel)button!.DataContext;

            OpenFileDialog openImageDialog = new()
            {
                Title = Properties.Resources.Dialog_OpenImage,
                Filter = FileIO.ImageFileExtensionFilter,
                InitialDirectory = Directory.GetCurrentDirectory()
            };
            if (openImageDialog.ShowDialog() == true)
            {
                using FileStream fs = (FileStream)openImageDialog.OpenFile();
                BitmapImage image = new(new Uri(fs.Name, UriKind.Absolute));
                inventoryItem.ItemImage = image;
            }
        }

        // TODO ClearButton_Click: move logic to Model
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            WpfUiButton button = (WpfUiButton)sender;
            InventoryItemModel inventoryItem = (InventoryItemModel)button!.DataContext;
            OpenClearItemDialog(inventoryItem);
        }

        // TODO DeleteButton_Click: move logic to Model
        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WpfUiButton button = (WpfUiButton)sender;
            InventoryItemModel inventoryItem = (InventoryItemModel)button!.DataContext;
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

                    if (index < 0 || index > ViewModel.Items.Count - 1)
                        return;

                    var newInventoryItem = Activator.CreateInstance(inventoryItem.GetType());

                    ViewModel.Items[index] = (InventoryItemModel)newInventoryItem!;
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
            Point point = e.GetPosition(null);
            Vector diff = _dragStartPoint - point;
            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
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
                InventoryItemModel source = (InventoryItemModel)e.Data.GetData(typeof(InventoryItemModel));
                source ??= (InventoryWeaponModel)e.Data.GetData(typeof(InventoryWeaponModel));    // If source is null at this point, try getting data for an InventoryWeaponModel
                source ??= (InventoryArmorModel)e.Data.GetData(typeof(InventoryArmorModel));  // If source is still null, try getting data for an InventoryArmorModel

                var target = (InventoryItemModel)itemBorder.DataContext;

                int sourceIndex = InventoryItemsControl.Items.IndexOf(source);
                int targetIndex = InventoryItemsControl.Items.IndexOf(target);

                if (sourceIndex == targetIndex)
                    return;

                Move(source, sourceIndex, targetIndex);
                ViewModel.ItemsView?.Refresh();
            }
        }

        private void Move(InventoryItemModel source, int sourceIndex, int targetIndex)
        {
            if(sourceIndex < 0 || sourceIndex > ViewModel.Items!.Count - 1
                || targetIndex < 0 || targetIndex > ViewModel.Items.Count - 1)
            {
                return;
            }
                
            if (sourceIndex < targetIndex)
            {
                ViewModel.Items?.Insert(targetIndex + 1, source);
                ViewModel.Items?.RemoveAt(sourceIndex);
            }
            else
            {
                int removeIndex = sourceIndex + 1;
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
            ColorPicker colorPicker = (ColorPicker)sender;
            InventoryItemModel itemModel = (InventoryItemModel)colorPicker.DataContext;
            itemModel.Brush = e.SelectedBrush;

            if (colorPicker.SelectedBrushName is "Red" or "Pink" or "Purple" or "DeepPurple" or "Indigo" or "DeepOrange" or "Brown" or "BlueGrey" or "Primary")
                itemModel.Foreground = (SolidColorBrush)Application.Current.Resources["TextFillColorPrimaryBrush"];
            else 
                itemModel.Foreground = (SolidColorBrush)Application.Current.Resources["TextFillColorInverseBrush"];
            ViewModel.ItemsView?.Refresh();
        }

        #endregion Color Picker

        //TODO NumBox_MouseWheel move to static function 2/2
        private void NumBox_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            NumberBox numBox = (NumberBox)sender;

            if (numBox.Value > numBox.Max || numBox.Value < numBox.Min || e.Delta == 0)
                return;

            numBox.Value = e.Delta > 0 ? numBox.Value + numBox.Step : numBox.Value - numBox.Step;
        }

        private void AddItemPopup_Closed(object sender, EventArgs e) => ItemTypeSelector.SelectedIndex = 0;

        private void AddItemButton_Click(object sender, RoutedEventArgs e) => AddItemPopup.IsOpen = false;
    }
}