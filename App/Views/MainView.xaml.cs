using VetClinic.App.BusinessLayer;
using VetClinic.App.ViewModels;
using System.Windows;

namespace VetClinic.App.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            var animalRepo = new CsvAnimalRepository("animals.csv");
            var customerRepo = new CsvCustomerRepository("customers.csv");

            var animalService = new AnimalService(animalRepo);
            var customerService = new CustomerService(customerRepo);

            DataContext = new MainViewModel(customerService, animalService);
        }
    }
}
