using Assignment2.App.BusinessLayer;
using Assignment2.App.BusinessLayer.Models;
using System.Linq;
using System.Windows;

namespace Assignment2.App
{
    /// <summary>
    /// Interaction logic for AnimalEditorWindow.xaml
    /// </summary>
    public partial class AnimalEditorWindow : Window
    {
        private readonly AnimalService animalService;
        private readonly CustomerService customerService;
        private Animal? animal;
        private Customer? customer;

        public AnimalEditorWindow(AnimalService animalService, CustomerService customerService)
        {
            InitializeComponent();
            this.animalService = animalService;
            this.customerService = customerService;
        }

        public Animal? Animal
        {
            get => animal;
            set
            {
                customer = null;
                animal = value;
                animalName.Text = animal?.Name ?? string.Empty;
                type.Text = animal?.Type ?? string.Empty;
                sex.Text = animal?.Sex ?? string.Empty;
                breed.Text = animal?.Breed ?? string.Empty;
                owner.Text = string.Empty;

                if (animal == null) return;
                customer = customerService.GetAllCustomers().FirstOrDefault(c => c.Id == animal.OwnerId);
                owner.Text = customer?.ToString() ?? string.Empty;
            }
        }

        private bool AddNewAnimal()
        {
            var newAnimal = new Animal
            {
                Name = animalName.Text,
                Type = type.Text,
                Breed = breed.Text,
                Sex = sex.Text,
                OwnerId = customer?.Id ?? 0
            };

            if (!newAnimal.CheckIfValid())
            {
                MessageBox.Show(
                    "Cannot save animal - some information is missing",
                    "Save error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return false;
            }

            animalService.AddAnimal(newAnimal);
            return true;
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnFindCustomer(object sender, RoutedEventArgs e)
        {
            var window = new SearchForCustomerWindow(customerService)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            if (window.ShowDialog() == true)
            {
                customer = window.Customer;
                owner.Text = customer?.ToString() ?? string.Empty;
            }
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            if (Animal != null)
            {
                if (UpdateAnimal()) Close();
            }
            else
            {
                if (AddNewAnimal()) Close();
            }
        }

        private bool UpdateAnimal()
        {
            Animal!.Name = animalName.Text;
            Animal.Type = type.Text;
            Animal.Breed = breed.Text;
            Animal.Sex = sex.Text;
            Animal.OwnerId = customer?.Id ?? 0;

            if (!Animal.CheckIfValid())
            {
                MessageBox.Show(
                    "Cannot save animal - some information is missing",
                    "Save error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return false;
            }

            animalService.UpdateAnimal(Animal);
            return true;
        }
    }
}
