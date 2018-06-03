using System.Windows;
using Telerik.Windows.Controls;

namespace ActionSpawtz {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RadWindow {
        public MainWindow() {
            InitializeComponent();
        }

        private void RadWindow_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            if (this.Parent is Window) {
                Window thisWindow = this.Parent as Window;
                thisWindow.ShowInTaskbar = true;
                thisWindow.Title = this.Header.ToString();
            }
        }
    }
}