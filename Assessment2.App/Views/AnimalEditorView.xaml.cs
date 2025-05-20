using System.Windows;
using Assignment2.App.BusinessLayer;
using Assignment2.App.BusinessLayer.Models;
using Assignment2.App.ViewModels;

namespace Assignment2.App.Views
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
