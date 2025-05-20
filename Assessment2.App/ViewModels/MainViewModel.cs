using System.Windows;
using System.Windows.Input;
using Assignment2.App.BusinessLayer;
using Assignment2.App.Views;

namespace Assignment2.App.ViewModels
{
    public class MainViewModel
    {
        private readonly AnimalService animalService;
        private readonly CustomerService customerService;

        public ICommand AddCustomerCommand { get; }
        public ICommand EditCustomerCommand { get; }
        public ICommand AddAnimalCommand { get; }
        public ICommand EditAnimalCommand { get; }
        public ICommand ExitCommand { get; }

        public MainViewModel(CustomerService cs, AnimalService asv)
        {
            customerService = cs;
            animalService = asv;

            AddCustomerCommand = new RelayCommand(() => new CustomerEditorWindow(customerService).ShowDialog());
            EditCustomerCommand = new RelayCommand(() =>
            {
                var customerSearch = new SearchForCustomerWindow(customerService) { WindowStartupLocation = WindowStartupLocation.CenterOwner };
                if (customerSearch.ShowDialog() == true)
                    new CustomerEditorWindow(customerService) { Customer = customerSearch.Customer }.ShowDialog();
            });

            AddAnimalCommand = new RelayCommand(() => new AnimalEditorWindow(animalService, customerService).ShowDialog());
            EditAnimalCommand = new RelayCommand(() =>
            {
                var customerSearch = new SearchForCustomerWindow(customerService) { WindowStartupLocation = WindowStartupLocation.CenterOwner };
                if (customerSearch.ShowDialog() != true) return;
                var animalSearch = new SearchForAnimalWindow(animalService) { SelectedCustomer = customerSearch.Customer };
                if (animalSearch.ShowDialog() == true)
                    new AnimalEditorWindow(animalService, customerService) { Animal = animalSearch.Animal }.ShowDialog();
            });

            ExitCommand = new RelayCommand(() => Application.Current.Shutdown());
        }
    }
}
