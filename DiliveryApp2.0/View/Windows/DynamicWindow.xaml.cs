using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DiliveryApp2._0.Model;

namespace DiliveryApp2._0.View.Windows
{
    public partial class DynamicWindow : Window
    {
        //public ObservableCollection<TableDataViewModel> TableData { get; set; }

        //public GPTWindow()
        //{
        //    InitializeComponent();
        //    TableData = new ObservableCollection<TableDataViewModel>();
        //    this.DataContext = this;

        //    LoadData(); // Вызов метода загрузки данных
        //}

        //private async void LoadData()
        //{
        //    using (var context = new Dilivery2DBEntities())
        //    {
        //        var tableNames = new List<string> { "Couriers", "Customers", "Restaurants", "Deliveries", "MenuItems", "Reviews", "Orders", "OrderItems" };
        //        foreach (var tableName in tableNames)
        //        {
        //            var entityType = Type.GetType($"DiliveryApp2._0.Model.{tableName}");
        //            if (entityType != null)
        //            {
        //                var data = await context.Set(entityType).ToListAsync();
        //                TableData.Add(new TableDataViewModel { TableName = tableName, Data = data });
        //                Console.WriteLine($"Loaded {tableName}: {data.Count} records."); // Выводим количество загруженных записей
        //            }
        //        }
        //    }
        //}
        public DynamicWindow()
        {
            InitializeComponent();
        }

        private async void LoadTablesButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> tableNames = new List<string> { "Couriers", "Customers", "Restaurants", "Deliveries", "MenuItems", "Reviews", "Orders", "OrderItems" };
            await LoadMultipleTablesAsync(tableNames);
        }

        private async Task LoadMultipleTablesAsync(List<string> tableNames)
        {
            using (var context = new Dilivery2DBEntities())
            {
                foreach (var tableName in tableNames)
                {
                    TabItem tabItem = new TabItem
                    {
                        Header = tableName
                    };

                    DataGrid dataGrid = new DataGrid();

                    switch (tableName)
                    {
                        case "Couriers":
                            dataGrid.ItemsSource = await context.couriers.ToListAsync();
                            break;
                        case "Customers":
                            dataGrid.ItemsSource = await context.customers.ToListAsync();
                            break;
                        case "Restaurants":
                            dataGrid.ItemsSource = await context.restaurants.ToListAsync();
                            break;
                        case "MenuItems":
                            dataGrid.ItemsSource = await context.menu_items.ToListAsync();
                            break;
                        case "Reviews":
                            dataGrid.ItemsSource = await context.reviews.ToListAsync();
                            break;
                        case "Orders":
                            dataGrid.ItemsSource = await context.orders.ToListAsync();
                            break;
                        case "OrderItems":
                            dataGrid.ItemsSource = await context.order_items.ToListAsync();
                            break;
                        case "Deliveries":
                            dataGrid.ItemsSource = await context.deliveries.ToListAsync();
                            break;
                        default:
                            MessageBox.Show($"Таблица {tableName} не найдена.");
                            continue;
                    }

                    tabItem.Content = dataGrid;
                    tabControl.Items.Add(tabItem); // Добавление вкладки в TabControl
                }
            }
        }
        private async void AddCustomer(customers newCustomer)
        {
            using (var context = new Dilivery2DBEntities())
            {
                context.customers.Add(newCustomer);
                await context.SaveChangesAsync();
                await LoadMultipleTablesAsync(new List<string> { "Customers" }); // Перезагрузить данные
            }
        }
        private async void DeleteCustomer(int customerId)
        {
            using (var context = new Dilivery2DBEntities())
            {
                var customerToDelete = await context.customers.FindAsync(customerId);
                if (customerToDelete != null)
                {
                    context.customers.Remove(customerToDelete);
                    await context.SaveChangesAsync();
                    await LoadMultipleTablesAsync(new List<string> { "Customers" }); // Перезагрузить данные
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newCustomer = new customers { customer_name = "New Customer", customer_address = "new. customer" };
            AddCustomer(newCustomer);
        }

    }
}

    

