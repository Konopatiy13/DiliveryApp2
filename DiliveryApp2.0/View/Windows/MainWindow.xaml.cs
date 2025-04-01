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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DiliveryApp2._0.View.Windows;
using DiliveryApp2._0.ViewModel;

namespace DiliveryApp2._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();

            (DataContext as MainWindowViewModel).LoadCustomer();

        }
        private void getButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).LoadCustomer();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).DeleteCustomer();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerWindow addCustomerWindow = new AddCustomerWindow(null);
            addCustomerWindow.Owner = this;
            addCustomerWindow.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

            AddCustomerWindow addCustomerWindow = new AddCustomerWindow((DataContext as MainWindowViewModel).SelectedCustomer);
            addCustomerWindow.Owner = this;
            addCustomerWindow.ShowDialog();
        }
    }
}
