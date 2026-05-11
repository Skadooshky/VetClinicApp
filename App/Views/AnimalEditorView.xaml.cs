using System.Windows;
using VetClinic.App.BusinessLayer;
using VetClinic.App.BusinessLayer.Models;
using VetClinic.App.ViewModels;

namespace VetClinic.App.Views
{
    public partial class AnimalEditorView : Window
    {
        public AnimalEditorView(AnimalService animalService, CustomerService customerService, Customer? selectedCustomer = null, Animal? existingAnimal = null)
        {
            InitializeComponent();
            var vm = new AnimalViewModel(animalService, customerService, selectedCustomer, existingAnimal);
            vm.CloseRequested += () => Close();
            DataContext = vm;
        }
    }
}
