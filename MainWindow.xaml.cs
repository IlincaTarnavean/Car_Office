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

namespace Car_Office
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource driverViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("driverViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // driverViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource carViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("carViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // carViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource bookingViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("bookingViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // bookingViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource permissionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("permissionViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // permissionViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource registrationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("registrationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // registrationViewSource.Source = [generic data source]
        }
    }
}
