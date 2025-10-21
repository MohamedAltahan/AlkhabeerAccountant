using AlkhabeerAccountant.CustomControls.SecondaryWindow;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shell;

namespace AlkhabeerAccountant.Shared
{
    public class BaseWindow : Window
    {
        private DockPanel _rootPanel;
        private TitleBar _titleBar;
        private ContentPresenter _contentHost;

        public BaseWindow()
        {
            //  Always Right-to-Left for Arabic UI
            FlowDirection = FlowDirection.RightToLeft;

            // ✅ Automatically set owner to MainWindow if available
            if (Application.Current?.MainWindow != null && Application.Current.MainWindow != this)
            {
                Owner = Application.Current.MainWindow;
            }

            //  Load shared styles safely
            var dict = new ResourceDictionary
            {
                Source = new Uri("/AlkhabeerAccountant;component/Shared/BaseWindowStyle.xaml", UriKind.RelativeOrAbsolute)
            };
            Resources.MergedDictionaries.Add(dict);

            if (TryFindResource("BaseWindowStyle") is Style style)
                Style = style;

            //Layout container
            _rootPanel = new DockPanel();

            //Title bar (shared for all windows)
            _titleBar = new TitleBar();
            DockPanel.SetDock(_titleBar, Dock.Top);
            _rootPanel.Children.Add(_titleBar);

            //Content placeholder
            _contentHost = new ContentPresenter();
            _rootPanel.Children.Add(_contentHost);

            //Set the root panel as the window content
            base.Content = _rootPanel;
            Loaded += BaseWindow_Loaded;
        }

        //Title text DP (for TitleBar binding)
        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register(nameof(TitleText), typeof(string), typeof(BaseWindow),
                new PropertyMetadata("الخبير المحاسبي", OnTitleTextChanged));

        public string TitleText
        {
            get => (string)GetValue(TitleTextProperty);
            set => SetValue(TitleTextProperty, value);
        }

        private static void OnTitleTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BaseWindow win && win._titleBar != null)
                win._titleBar.TitleText = e.NewValue?.ToString();
        }

        //Center window when loaded
        private void BaseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyCenteredSizing();
        }

        protected virtual void ApplyCenteredSizing()
        {
            var workArea = SystemParameters.WorkArea;
            double widthRatio = 0.9;
            double heightRatio = 0.9;
            double verticalShiftRatio = 0.05;

            Width = workArea.Width * widthRatio;
            Height = workArea.Height * heightRatio;

            Left = workArea.Left + (workArea.Width - Width) / 2;
            Top = workArea.Top + (workArea.Height - Height) / 2 + (workArea.Height * verticalShiftRatio);
        }

        //Override OnContentChanged to redirect content to our ContentPresenter
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            // Only process if the new content is not our root panel
            if (newContent != _rootPanel && _contentHost != null)
            {
                _contentHost.Content = newContent;
                // Prevent the base Window from using the content directly
                if (base.Content == newContent)
                {
                    base.Content = _rootPanel;
                }
            }
        }
    }
}