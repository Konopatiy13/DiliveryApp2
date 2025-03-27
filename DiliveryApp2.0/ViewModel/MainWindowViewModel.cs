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
        private string _name;
        private int _customer_id;
        private int restaurant_id;
        private ObservableCollection<orders> _orders;
        private orders _selectedOrder;
        private orders _newOrder;

        public string Name
        {
            get => _name;
            set => SetPropertyChanged(ref _name, value, nameof(Name));
        }

        public int Course
        {
            get => _customer_id;
            set => SetPropertyChanged(ref _customer_id, value, nameof(Course));
        }

        public ObservableCollection<orders> Orders
        {
            get => _orders;
            set => SetPropertyChanged(ref _orders, value, nameof(Orders));
        }

        public orders SelectedOrder
        {
            get => _selectedOrder;
            set => SetPropertyChanged(ref _selectedOrder, value, nameof(SelectedOrder));
        }

        public orders NewOrder
        {
            get => _newOrder;
            set => SetPropertyChanged(ref _newOrder, value, nameof(NewOrder));
        }


        public MainWindowViewModel()
        {
            Orders = new ObservableCollection<orders>();
            NewOrder = new orders();
        }

        public void LoadOrder()
        {
            Orders.Clear();
            using (var context = new Dilivery2DBEntities())
            {
                var temp = context.orders.ToList();

                foreach (var student in temp)
                {
                    Orders.Add(student);
                }
            }
        }

        public void DeleteOrder()
        {
            try
            {
                using (var context = new Dilivery2DBEntities())
                {
                    var findEntity = context.orders.FirstOrDefault(s => s.order_id == SelectedOrder.order_id);
                    if (findEntity == null)
                        return;
                    var result = context.orders.Remove(findEntity);
                    context.SaveChanges();

                    LoadOrder();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public bool AddNewOrder()
        {

            using (var context = new Dilivery2DBEntities())
            {
                var newOrder = context.orders.Add(NewOrder);
                context.SaveChanges();
                return true;
            }

        }
    }
}
