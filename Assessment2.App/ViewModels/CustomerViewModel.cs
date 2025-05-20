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
        private readonly Customer? originalCustomer;

        public ObservableCollection<Customer> Customers { get; set; }

        public int Id => originalCustomer?.Id ?? 0;
        private string firstName = string.Empty;
        private string surname = string.Empty;
        private string phoneNumber = string.Empty;
        private string address = string.Empty;
        private Customer? selectedCustomer;

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

        public Customer? SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                selectedCustomer = value;
                if (value != null)
                {
                    FirstName = value.FirstName ?? string.Empty;
                    Surname = value.Surname ?? string.Empty;
                    PhoneNumber = value.PhoneNumber ?? string.Empty;
                    Address = value.Address ?? string.Empty;
                }
                OnPropertyChanged();
            }
        }

        public bool IsEditing => originalCustomer != null && originalCustomer.Id != 0;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }
        public event Action? CloseRequested;

        public CustomerViewModel(CustomerService service, Customer? customer = null)
        {
            customerService = service;
            originalCustomer = customer;

            Customers = new ObservableCollection<Customer>(customerService.GetAllCustomers());

            if (customer != null)
            {
                SelectedCustomer = customer;
            }

            SaveCommand = new RelayCommand(SaveCustomer);
            CancelCommand = new RelayCommand(() => CloseRequested?.Invoke());
            DeleteCommand = new RelayCommand(DeleteCustomer, () => SelectedCustomer != null);
            OnPropertyChanged(nameof(IsEditing));
        }

        private void SaveCustomer()
        {
            var newCustomer = new Customer
            {
                Id = originalCustomer?.Id ?? 0,
                FirstName = FirstName,
                Surname = Surname,
                PhoneNumber = PhoneNumber,
                Address = Address
            };

            if (!newCustomer.CheckIfValid())
            {
                MessageBox.Show("Missing required customer information.", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (originalCustomer == null)
                customerService.AddCustomer(newCustomer);
            else
                customerService.UpdateCustomer(newCustomer);

            Customers.Clear();
            foreach (var c in customerService.GetAllCustomers()) Customers.Add(c);

            CloseRequested?.Invoke();
        }

        private void DeleteCustomer()
        {
            if (SelectedCustomer == null) return;

            var result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                customerService.DeleteCustomer(SelectedCustomer.Id);
                Customers.Remove(SelectedCustomer);
                SelectedCustomer = null;
                CloseRequested?.Invoke();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null!)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
