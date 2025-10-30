using System.Windows;
using System.Windows.Controls;

namespace AlkhabeerAccountant.CustomControls.SecondaryWindow
{
    public partial class StatusSelector : UserControl
    {
        public StatusSelector()
        {
            InitializeComponent();
        }

        // ✅ Main bindable property
        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register(
                nameof(IsActive),
                typeof(bool),
                typeof(StatusSelector),
                new PropertyMetadata(true, OnIsActiveChanged));

        // Derived property for "غير نشط"
        public bool Inactive
        {
            get => !IsActive;
            set => IsActive = !value;
        }

        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (StatusSelector)d;
            // This ensures both radios stay in sync
            control.RaisePropertyChanged(nameof(Inactive));
        }

        private void RaisePropertyChanged(string propertyName)
        {
            var prop = GetType().GetProperty(propertyName);
            if (prop != null)
            {
                SetValue(IsActiveProperty, IsActive);
            }
        }
    }
}
