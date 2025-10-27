using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlkhabeerAccountant.CustomControls.SecondaryWindow
{
    /// <summary>
    /// Interaction logic for StatusSelector.xaml
    /// </summary>
    public partial class StatusSelector : UserControl
    {
        public StatusSelector()
        {
            InitializeComponent();
        }
        // ✅ Dependency property for IsActive
        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register(nameof(IsActive), typeof(bool), typeof(StatusSelector),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsActiveChanged));

        // Derived property to handle "Inactive" binding
        public bool IsInactive
        {
            get => !IsActive;
            set => IsActive = !value;
        }

        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Optional: handle changes if you want to react in code-behind later
        }
    }
}

