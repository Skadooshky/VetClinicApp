using Assignment2.App.BusinessLayer;
using Assignment2.App.BusinessLayer.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Assignment2.App.ViewModels
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService customerService;
        private readonly Customer? existingCustomer;

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

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public event Action? CloseRequested;

        public CustomerViewModel(CustomerService service, Customer? customer = null)
        {
            customerService = service;
            existingCustomer = customer;

            Customers = new ObservableCollection<Customer>(customerService.GetAllCustomers());

            // If editing existing customer, populate fields
            if (customer != null)
            {
                FirstName = customer.FirstName;
                Surname = customer.Surname;
                PhoneNumber = customer.PhoneNumber;
                Address = customer.Address;
            }

            SaveCommand = new RelayCommand(SaveCustomer);
            CancelCommand = new RelayCommand(() => CloseRequested?.Invoke());
        }

        private void SaveCustomer()
        {
            var newCustomer = new Customer
            {
                Id = existingCustomer?.Id ?? 0,
                FirstName = FirstName,
                Surname = Surname,
                PhoneNumber = PhoneNumber,
                Address = Address
            };

            if (!newCustomer.CheckIfValid())
            {
                MessageBox.Show("Cannot save customer - missing information.", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (existingCustomer == null)
            {
                customerService.AddCustomer(newCustomer);
                Customers.Add(newCustomer);
            }
            else
            {
                customerService.UpdateCustomer(newCustomer);

                // Optional: update list item in UI
                var existing = Customers.FirstOrDefault(c => c.Id == newCustomer.Id);
                if (existing != null)
                {
                    existing.FirstName = newCustomer.FirstName;
                    existing.Surname = newCustomer.Surname;
                    existing.PhoneNumber = newCustomer.PhoneNumber;
                    existing.Address = newCustomer.Address;
                }
            }

            CloseRequested?.Invoke();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null!)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
