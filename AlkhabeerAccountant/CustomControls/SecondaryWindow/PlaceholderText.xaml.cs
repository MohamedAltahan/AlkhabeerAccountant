using System.Printing;
using System.Windows;
using System.Windows.Controls;

namespace AlkhabeerAccountant.CustomControls.SecondaryWindow
{
    public partial class PlaceholderText : UserControl
    {
        public PlaceholderText()
        {
            InitializeComponent();
        }

        // Placeholder text
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(PlaceholderText),
                new PropertyMetadata(string.Empty));

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        // Target textbox
        public static readonly DependencyProperty TargetTextBoxProperty =
            DependencyProperty.Register(nameof(TargetTextBox), typeof(TextBox), typeof(PlaceholderText),
                new PropertyMetadata(null, OnTargetChanged));

        public TextBox TargetTextBox
        {
            get => (TextBox)GetValue(TargetTextBoxProperty);
            set => SetValue(TargetTextBoxProperty, value);
        }

        private static void OnTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PlaceholderText ph)
            {
                if (e.OldValue is TextBox oldBox)
                    oldBox.TextChanged -= ph.TargetTextBox_TextChanged;

                if (e.NewValue is TextBox newBox)
                {
                    newBox.TextChanged += ph.TargetTextBox_TextChanged;
                    ph.UpdateVisibility(newBox.Text);
                }
            }
        }

        private void TargetTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
                UpdateVisibility(tb.Text);
        }

        private void UpdateVisibility(string text)
        {
            IsVisiblePlaceholder = string.IsNullOrEmpty(text);
        }

        // Internal flag
        public static readonly DependencyProperty IsVisiblePlaceholderProperty =
            DependencyProperty.Register(nameof(IsVisiblePlaceholder), typeof(bool), typeof(PlaceholderText),
                new PropertyMetadata(true));

        public bool IsVisiblePlaceholder
        {
            get => (bool)GetValue(IsVisiblePlaceholderProperty);
            set => SetValue(IsVisiblePlaceholderProperty, value);
        }
    }
}
