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
    public class AnimalViewModel : INotifyPropertyChanged
    {
        private readonly AnimalService animalService;
        private readonly CustomerService customerService;
        private readonly Animal? originalAnimal;

        public ObservableCollection<Animal> Animals { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }

        private string name = string.Empty;
        private string type = string.Empty;
        private string breed = string.Empty;
        private string sex = string.Empty;
        private Animal? selectedAnimal;
        private int selectedCustomerId;

        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        public string Type { get => type; set { type = value; OnPropertyChanged(); } }
        public string Breed { get => breed; set { breed = value; OnPropertyChanged(); } }
        public string Sex { get => sex; set { sex = value; OnPropertyChanged(); } }

        public int SelectedCustomerId
        {
            get => selectedCustomerId;
            set
            {
                selectedCustomerId = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedCustomer));
                FilterAnimalsBySelectedCustomer();
            }
        }

        public Customer? SelectedCustomer => Customers.FirstOrDefault(c => c.Id == SelectedCustomerId);

        public Animal? SelectedAnimal
        {
            get => selectedAnimal;
            set
            {
                selectedAnimal = value;
                if (value != null)
                {
                    Name = value.Name ?? string.Empty;
                    Type = value.Type ?? string.Empty;
                    Breed = value.Breed ?? string.Empty;
                    Sex = value.Sex ?? string.Empty;
                    SelectedCustomerId = value.OwnerId;
                }
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }
        public event Action? CloseRequested;

        public AnimalViewModel(AnimalService animalService, CustomerService customerService, Customer? customer = null, Animal? animal = null)
        {
            this.animalService = animalService;
            this.customerService = customerService;
            this.originalAnimal = animal;

            Customers = new ObservableCollection<Customer>(customerService.GetAllCustomers());

            // Set customer ID before filtering
            if (customer != null)
                SelectedCustomerId = customer.Id;

            // Initialize the animal list properly after customer is selected
            FilterAnimalsBySelectedCustomer();

            // Select the animal if provided
            if (animal != null)
                SelectedAnimal = animal;

            SaveCommand = new RelayCommand(SaveAnimal);
            CancelCommand = new RelayCommand(() => CloseRequested?.Invoke());
            DeleteCommand = new RelayCommand(DeleteAnimal, () => SelectedAnimal != null);
            SearchCommand = new RelayCommand<string>(SearchAnimals);
            OnPropertyChanged(nameof(IsEditing));
        }

        private void SaveAnimal()
        {
            if (SelectedCustomer == null)
            {
                MessageBox.Show("Please select an owner.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newAnimal = new Animal
            {
                Id = originalAnimal?.Id ?? 0,
                Name = Name,
                Type = Type,
                Breed = Breed,
                Sex = Sex,
                OwnerId = SelectedCustomer.Id
            };

            if (!newAnimal.CheckIfValid())
            {
                MessageBox.Show("Missing required animal information.", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (originalAnimal == null)
                animalService.AddAnimal(newAnimal);
            else
                animalService.UpdateAnimal(newAnimal);

            FilterAnimalsBySelectedCustomer();
            CloseRequested?.Invoke();
            
        }

        private void DeleteAnimal()
        {
            if (SelectedAnimal == null) return;

            var result = MessageBox.Show("Are you sure you want to delete this animal?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                animalService.DeleteAnimal(SelectedAnimal.Id);
                Animals.Remove(SelectedAnimal);
                SelectedAnimal = null;
                CloseRequested?.Invoke();
            }
        }

        private void SearchAnimals(string searchTerm)
        {
            var filtered = animalService.GetAllAnimals()
                .Where(a => (a.Name ?? string.Empty).Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Animals.Clear();
            foreach (var a in filtered) Animals.Add(a);
        }

        private void FilterAnimalsBySelectedCustomer()
        {
            if (SelectedCustomer == null) return;

            var filtered = animalService.GetAllAnimals()
                .Where(a => a.OwnerId == SelectedCustomer.Id)
                .ToList();

            Animals = new ObservableCollection<Animal>(filtered);
            OnPropertyChanged(nameof(Animals));
        }

        public bool IsEditing => originalAnimal != null && originalAnimal.Id != 0;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null!)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
