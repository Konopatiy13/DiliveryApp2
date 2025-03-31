using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DiliveryApp2._0.Model;

namespace DiliveryApp2._0.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _customer_name;
        private string _customer_address;
        private string _customer_phone;
        private ObservableCollection<customers> _customers;
        private customers _selectedCustomer;
        private customers _newCustomer;

        public string CustomerName
        {
            get => _customer_name;
            set => SetPropertyChanged(ref _customer_name, value, nameof(CustomerName));
        }

        public string CustomerAddress
        {
            get => _customer_address;
            set => SetPropertyChanged(ref _customer_address, value, nameof(CustomerAddress));
        }

        public string CustomerPhone
        {
            get => _customer_phone;
            set => SetPropertyChanged(ref _customer_phone, value, nameof(CustomerPhone));
        }

        public ObservableCollection<customers> Customers
        {
            get => _customers;
            set => SetPropertyChanged(ref _customers, value, nameof(Customers));
        }

        // Используем SelectedCustomer для редактирования и удаления
        public customers SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetPropertyChanged(ref _selectedCustomer, value, nameof(SelectedCustomer));
        }

        // NewCustomer используется только для добавления нового клиента
        public customers NewCustomer
        {
            get => _newCustomer;
            set => SetPropertyChanged(ref _newCustomer, value, nameof(NewCustomer));
        }

        public MainWindowViewModel()
        {
            Customers = new ObservableCollection<customers>();
            NewCustomer = new customers();
        }

        public void LoadCustomer()
        {
            Customers.Clear();
            using (var context = new Dilivery2DBEntities())
            {
                var customersList = context.customers.ToList();
                foreach (var customer in customersList)
                {
                    Customers.Add(customer);
                }
            }
        }

        public void DeleteCustomer()
        {
            if (SelectedCustomer == null)
            {
                MessageBox.Show("Клиент не выбран для удаления.");
                return;
            }

            try
            {
                using (var context = new Dilivery2DBEntities())
                {
                    var findEntity = context.customers.FirstOrDefault(s => s.customer_id == SelectedCustomer.customer_id);
                    if (findEntity == null)
                    {
                        MessageBox.Show("Клиент не найден в базе данных.");
                        return;
                    }

                    context.customers.Remove(findEntity);
                    context.SaveChanges();
                    LoadCustomer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении клиента: {ex.Message}");
            }
        }

        public bool AddNewCustomer()
        {
            if (NewCustomer == null)
            {
                MessageBox.Show("Необходимо заполнить данные нового клиента.");
                return false;
            }

            try
            {
                using (var context = new Dilivery2DBEntities())
                {
                    context.customers.Add(NewCustomer);
                    context.SaveChanges();
                }

                NewCustomer = new customers(); // создаем новый объект для ввода следующего клиента
                LoadCustomer();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении клиента: {ex.Message}");
                return false;
            }
        }

        public bool EditCustomer()
        {
            if (SelectedCustomer == null)
            {
                MessageBox.Show("Клиент не выбран для редактирования.");
                return false;
            }
            else
            {
                MessageBox.Show($"Выбран клиент: {SelectedCustomer.customer_name}");
            }

            try
            {
                using (var context = new Dilivery2DBEntities())
                {
                    var entity = context.customers.FirstOrDefault(s => s.customer_id == SelectedCustomer.customer_id);
                    if (entity == null)
                    {
                        MessageBox.Show("Клиент не найден в базе данных.");
                        return false;
                    }

                    // Копируем данные из выбранного клиента
                    entity.customer_name = SelectedCustomer.customer_name;
                    entity.customer_address = SelectedCustomer.customer_address;
                    entity.customer_phone = SelectedCustomer.customer_phone;

                    context.SaveChanges();
                }

                LoadCustomer();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании клиента: {ex.Message}");
                return false;
            }
        }
    }
} 
