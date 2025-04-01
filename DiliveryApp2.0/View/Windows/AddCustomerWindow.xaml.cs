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
using System.Windows.Shapes;
using DiliveryApp2._0.Model;
using DiliveryApp2._0.ViewModel;

namespace DiliveryApp2._0.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {
        private customers isNewCustomer;
        public AddCustomerWindow(customers editCustomer)
        {
                InitializeComponent();
            DataContext = new MainWindowViewModel();
            (DataContext as MainWindowViewModel).NewCustomer = editCustomer;

            isNewCustomer = editCustomer;

            if (isNewCustomer == null)
            {
                (DataContext as MainWindowViewModel).NewCustomer = new customers();
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var result = (DataContext as MainWindowViewModel).AddNewCustomer();

            if (result)
            {
                MessageBox.Show("Объект записан");
                ((this.Owner as MainWindow).DataContext as MainWindowViewModel).EditCustomer();
                this.Close();
            }
        }
    }
}