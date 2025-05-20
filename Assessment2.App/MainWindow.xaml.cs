using Assignment2.App.BusinessLayer;
using Assignment2.App.BusinessLayer.Models;
using System.Windows;

namespace Assignment2.App
{
    public partial class MainWindow : Window
    {
        private readonly AnimalService animalService;
        private readonly CustomerService customerService;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize services with their repositories
            var animalRepo = new CsvAnimalRepository("animals.csv");
            var customerRepo = new CsvCustomerRepository("customers.csv");

            animalService = new AnimalService(animalRepo);
            customerService = new CustomerService(customerRepo);
        }

        private void EditAnimal(Animal? animal)
        {
            var window = new AnimalEditorWindow(animalService, customerService)
            {
                Animal = animal,
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();
        }

        private void EditCustomer(Customer? customer)
        {
            var window = new CustomerEditorWindow(customerService)
            {
                Customer = customer,
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();
        }

        private void OnAddAnimal(object sender, RoutedEventArgs e)
        {
            EditAnimal(null);
        }

        private void OnAddCustomer(object sender, RoutedEventArgs e)
        {
            EditCustomer(null);
        }

        private void OnEditAnimal(object sender, RoutedEventArgs e)
        {
            var customerSearch = new SearchForCustomerWindow(customerService)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };

            if (customerSearch.ShowDialog() != true) return;

            var animalSearch = new SearchForAnimalWindow(animalService)
            {
                SelectedCustomer = customerSearch.Customer,
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };

            if (animalSearch.ShowDialog() == true)
                EditAnimal(animalSearch.Animal);
        }

        private void OnEditCustomer(object sender, RoutedEventArgs e)
        {
            var customerSearch = new SearchForCustomerWindow(customerService)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            if (customerSearch.ShowDialog() == true)
            {
                EditCustomer(customerSearch.Customer);
            }
        }

        private void OnExitApplication(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
