using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DiliveryApp2._0.Model;

namespace DiliveryApp2._0.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<customers> _customers;
        private int _customerId;
        private string _customerName;
        private string _customerAddress;
        private string _customerPhone;
        private customers _selectedCustomer;
        private customers _newCustomer;

        private ObservableCollection<couriers> _couriers;
        private int _courierId;
        private string _courierName;
        private string _courierPhone;
        private string _vehicleType;
        private couriers _selectedCouriers;
        private couriers _newCouriers;

        public string Name
        {
            get => _customerName;
            set => SetPropertyChanged(ref _customerName, value, nameof(Name));
        }

        public string Address
        {
            get => _customerAddress;
            set => SetPropertyChanged(ref _customerAddress, value, nameof(Address));
        }
        public string Phone
        {
            get => _customerPhone;
            set => SetPropertyChanged(ref _customerPhone, value, nameof(Phone));
        }

        public ObservableCollection<customers> Customers
        {
            get => _customers;
            set => SetPropertyChanged(ref _customers, value, nameof(Customers));
        }

        public customers SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetPropertyChanged(ref _selectedCustomer, value, nameof(Customers));
        }

        public customers NewCustomer
        {
            get => _newCustomer;
            set => SetPropertyChanged(ref _newCustomer, value, nameof(Customers));
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
        public bool EditCustomer()
        {
            using (var context = new Dilivery2DBEntities())
            {
                var findEntity = context.customers.FirstOrDefault(s => s.customer_id == NewCustomer.customer_id);
                if (findEntity != null)
                {
                    context.customers.AddOrUpdate(NewCustomer);
                    context.SaveChanges();
                    return true;
                }
                else { return false; }
            }
        }
    }
}