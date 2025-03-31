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
    /// Логика взаимодействия для AddCustomerWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {
        public AddCustomerWindow(customers editCustomer)
        {
            InitializeComponent();
            var viewModel = new MainWindowViewModel();
            DataContext = viewModel;
            viewModel.NewCustomer = editCustomer;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel == null)
            {
                MessageBox.Show("Не удалось загрузить данные.", "Ошибка", MessageBoxButton.OK);
                return;
            }

            try
            {
                var result = viewModel.EditCustomer();

                if (result)
                {
                    MessageBox.Show("Запись сохранена!", "Управление товарами", MessageBoxButton.OK);
                    var mainWindowViewModel = (this.Owner as MainWindow)?.DataContext as MainWindowViewModel;
                    mainWindowViewModel?.LoadCustomer();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка при сохранении записи.", "Ошибка", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK);
            }
        }
    }

}
