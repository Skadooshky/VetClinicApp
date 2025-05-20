using System.Windows;
using System.Windows.Input;
using Assignment2.App.BusinessLayer;
using Assignment2.App.Views;
using Assignment2.App.ViewModels;

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

            // Add new customer
            AddCustomerCommand = new RelayCommand(() =>
            {
                var view = new CustomerEditorView(customerService);
                view.ShowDialog();
            });

            // Edit existing customer
            EditCustomerCommand = new RelayCommand(() =>
            {
                var customerSearch = new SearchForCustomerView(customerService)
                {
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                if (customerSearch.ShowDialog() == true)
                {
                    var view = new CustomerEditorView(customerService, customerSearch.Customer);
                    view.ShowDialog();
                }
            });

            // Add new animal
            AddAnimalCommand = new RelayCommand(() =>
            {
                var view = new AnimalEditorView(animalService, customerService);
                view.ShowDialog();
            });

            // Edit existing animal
            EditAnimalCommand = new RelayCommand(() =>
            {
                var customerSearch = new SearchForCustomerView(customerService)
                {
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                if (customerSearch.ShowDialog() != true) return;

                var animalSearch = new SearchForAnimalView(animalService)
                {
                    SelectedCustomer = customerSearch.Customer,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                if (animalSearch.ShowDialog() == true)
                {
                    var view = new AnimalEditorView(animalService, customerService, customerSearch.Customer, animalSearch.Animal);
                    view.ShowDialog();
                }
            });

            ExitCommand = new RelayCommand(() => Application.Current.Shutdown());
        }
    }
}
