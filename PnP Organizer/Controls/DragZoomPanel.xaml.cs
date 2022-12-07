using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PnP_Organizer.Controls
{
    /// <summary>
    /// Interaction logic for DragZoomPanel.xaml
    /// </summary>
    public partial class DragZoomPanel : UserControl
    {
        private int _zoomLevel = 1;

        private ScaleTransform? _scaleTransform;
        private ScrollViewer? _scrollViewer;
        private Grid? _grid;

        private Point? _lastCenterPositionOnTarget;
        private Point? _lastMousePositionOnTarget;
        private Point? _lastDragPoint;

        public DragZoomPanel()
        {
            InitializeComponent();
        }

        private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            _scaleTransform = (ScaleTransform)Template.FindName("scaleTransform", this);
            _scrollViewer = (ScrollViewer)Template.FindName("scrollViewer", this);
            _grid = (Grid)Template.FindName("grid", this);

            _scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            _scrollViewer.MouseLeftButtonUp += OnMouseLeftButtonUp;
            _scrollViewer.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
            _scrollViewer.PreviewMouseWheel += OnPreviewMouseWheel;
            _scrollViewer.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
            _scrollViewer.MouseMove += OnMouseMove;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_lastDragPoint.HasValue)
            {
                var posNow = e.GetPosition(_scrollViewer);

                var dX = posNow.X - _lastDragPoint.Value.X;
                var dY = posNow.Y - _lastDragPoint.Value.Y;

                _lastDragPoint = posNow;

                _scrollViewer?.ScrollToHorizontalOffset(_scrollViewer.HorizontalOffset - dX);
                _scrollViewer?.ScrollToVerticalOffset(_scrollViewer.VerticalOffset - dY);
            }
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition(_scrollViewer);
            if (mousePos.X <= _scrollViewer?.ViewportWidth && mousePos.Y < _scrollViewer.ViewportHeight) //make sure we still can use the scrollbars
            {
                _scrollViewer.Cursor = Cursors.SizeAll;
                _lastDragPoint = mousePos;
                Mouse.Capture(_scrollViewer);
            }
        }

        private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            _lastMousePositionOnTarget = Mouse.GetPosition(_grid);

            if (e.Delta > 0) _zoomLevel++;
            else if (e.Delta < 0) _zoomLevel--;

            _zoomLevel = _zoomLevel < 1 ? 1 : _zoomLevel;

            _scaleTransform!.ScaleX = _zoomLevel;
            _scaleTransform!.ScaleY = _zoomLevel;

            var centerOfViewport = new Point(_scrollViewer!.ViewportWidth / 2, _scrollViewer.ViewportHeight / 2);
            _lastCenterPositionOnTarget = _scrollViewer.TranslatePoint(centerOfViewport, _grid);

            e.Handled = true;
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _scrollViewer!.Cursor = Cursors.Arrow;
            _scrollViewer.ReleaseMouseCapture();
            _lastDragPoint = null;
        }

        private void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0 || e.ExtentWidthChange != 0)
            {
                Point? targetBefore = null;
                Point? targetNow = null;

                if (!_lastMousePositionOnTarget.HasValue)
                {
                    if (_lastCenterPositionOnTarget.HasValue)
                    {
                        var centerOfViewport = new Point(_scrollViewer!.ViewportWidth / 2, _scrollViewer.ViewportHeight / 2);
                        var centerOfTargetNow = _scrollViewer.TranslatePoint(centerOfViewport, _grid);

                        targetBefore = _lastCenterPositionOnTarget;
                        targetNow = centerOfTargetNow;
                    }
                }
                else
                {
                    targetBefore = _lastMousePositionOnTarget;
                    targetNow = Mouse.GetPosition(_grid);

                    _lastMousePositionOnTarget = null;
                }

                if (targetBefore.HasValue)
                {
                    var dXInTargetPixels = targetNow!.Value.X - targetBefore.Value.X;
                    var dYInTargetPixels = targetNow.Value.Y - targetBefore.Value.Y;

                    var multiplicatorX = e.ExtentWidth / _grid!.Width;
                    var multiplicatorY = e.ExtentHeight / _grid.Height;

                    var newOffsetX = _scrollViewer!.HorizontalOffset - dXInTargetPixels * multiplicatorX;
                    var newOffsetY = _scrollViewer.VerticalOffset - dYInTargetPixels * multiplicatorY;

                    if (double.IsNaN(newOffsetX) || double.IsNaN(newOffsetY))
                    {
                        return;
                    }

                    _scrollViewer.ScrollToHorizontalOffset(newOffsetX);
                    _scrollViewer.ScrollToVerticalOffset(newOffsetY);
                }
            }
        }
    }
}
