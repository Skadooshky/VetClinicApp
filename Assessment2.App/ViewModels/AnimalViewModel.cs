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
        private Customer? selectedCustomer;

        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        public string Type { get => type; set { type = value; OnPropertyChanged(); } }
        public string Breed { get => breed; set { breed = value; OnPropertyChanged(); } }
        public string Sex { get => sex; set { sex = value; OnPropertyChanged(); } }
        public Customer? SelectedCustomer { get => selectedCustomer; set { selectedCustomer = value; OnPropertyChanged(); } }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public event Action? CloseRequested;

        public AnimalViewModel(AnimalService animalService, CustomerService customerService, Animal? animal = null)
        {
            this.animalService = animalService;
            this.customerService = customerService;
            this.originalAnimal = animal;

            Animals = new ObservableCollection<Animal>(animalService.GetAllAnimals());
            Customers = new ObservableCollection<Customer>(customerService.GetAllCustomers());

            if (animal != null)
            {
                Name = animal.Name ?? string.Empty;
                Type = animal.Type ?? string.Empty;
                Breed = animal.Breed ?? string.Empty;
                Sex = animal.Sex ?? string.Empty;
                SelectedCustomer = Customers.FirstOrDefault(c => c.Id == animal.OwnerId);
            }

            SaveCommand = new RelayCommand(SaveAnimal);
            CancelCommand = new RelayCommand(() => CloseRequested?.Invoke());
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

            CloseRequested?.Invoke();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null!)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
