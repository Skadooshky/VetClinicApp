using Assignment2.App.BusinessLayer;
using Assignment2.App.BusinessLayer.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Assignment2.App.ViewModels
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService customerService;

        public ObservableCollection<Customer> Customers { get; set; }

        private string firstName = string.Empty;
        private string surname = string.Empty;
        private string phoneNumber = string.Empty;
        private string address = string.Empty;

        public string FirstName
        {
            get => firstName;
            set { firstName = value; OnPropertyChanged(); }
        }

        public string Surname
        {
            get => surname;
            set { surname = value; OnPropertyChanged(); }
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set { phoneNumber = value; OnPropertyChanged(); }
        }

        public string Address
        {
            get => address;
            set { address = value; OnPropertyChanged(); }
        }

        public ICommand AddCustomerCommand { get; }

        public CustomerViewModel(CustomerService service)
        {
            customerService = service;
            Customers = new ObservableCollection<Customer>(customerService.GetAllCustomers());
            AddCustomerCommand = new RelayCommand(AddCustomer);
        }

        private void AddCustomer()
        {
            var newCustomer = new Customer
            {
                FirstName = FirstName,
                Surname = Surname,
                PhoneNumber = PhoneNumber,
                Address = Address
            };

            if (!newCustomer.CheckIfValid()) return;

            customerService.AddCustomer(newCustomer);
            Customers.Add(newCustomer);

            // Clear form fields
            FirstName = Surname = PhoneNumber = Address = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null!)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
