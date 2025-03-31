using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public customers SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetPropertyChanged(ref _selectedCustomer, value, nameof(SelectedCustomer));
        }

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
                var temp = context.customers.ToList();

                foreach (var customer in temp)
                {
                    Customers.Add(customer);
                }
            }
        }

        public void DeleteCustomer()
        {
            try
            {
                using (var context = new Dilivery2DBEntities())
                {
                    var findEntity = context.customers.FirstOrDefault(s => s.customer_id == SelectedCustomer.customer_id);
                    if (findEntity == null)
                        return;
                    var result = context.customers.Remove(findEntity);
                    context.SaveChanges();

                    LoadCustomer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public bool AddNewCustomer()
        {

            using (var context = new Dilivery2DBEntities())
            {
                var newCustomer = context.customers.Add(NewCustomer);
                context.SaveChanges();
                return true;
            }

        }
    }
}
