using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace AlkhabeerAccountant.CustomControls.SecondaryWindow
{
    public partial class TitleBar : UserControl
    {
        private Point _startPoint;

        public TitleBar()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleTextProperty =
        DependencyProperty.Register(nameof(TitleText), typeof(string), typeof(TitleBar),
        new PropertyMetadata(string.Empty));
        public string TitleText
        {
            get => (string)GetValue(TitleTextProperty);
            set => SetValue(TitleTextProperty, value);
        }

        // 🔹 Let the parent window handle drag
        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Window.GetWindow(this)?.DragMove();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.WindowState = window.WindowState == WindowState.Maximized
                ? WindowState.Normal : WindowState.Maximized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }

        private void move_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Window.GetWindow(this)?.DragMove();
            }
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window == null) return;

            if (e.ClickCount == 2)
            {
                // ✅ Double-click to toggle maximize/restore
                window.WindowState = window.WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
            }
            else if (e.ButtonState == MouseButtonState.Pressed && window.WindowState == WindowState.Maximized)
            {
                // ✅ Start "pull down" from maximized window
                //_startPoint = e.GetPosition(window);

                // Restore window manually
                //window.WindowState = WindowState.Normal;

                //// Move window under cursor (so it feels natural)
                //var mousePosition = e.GetPosition(window);
                //var screenPoint = window.PointToScreen(mousePosition);

                //window.Left = screenPoint.X - window.Width / 2;
                //window.Top = screenPoint.Y - 20;

                //// Begin drag move
                //Window.GetWindow(this)?.DragMove();
            }
        }

    }
}
