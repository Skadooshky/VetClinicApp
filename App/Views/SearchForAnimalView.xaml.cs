using VetClinic.App.BusinessLayer;
using VetClinic.App.BusinessLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace VetClinic.App.Views
{
    public partial class SearchForAnimalView : Window
    {
        private readonly AnimalService animalService;

        public SearchForAnimalView(AnimalService animalService)
        {
            InitializeComponent();
            this.animalService = animalService;
        }

        public Animal? Animal { get; private set; }

        public Customer? SelectedCustomer
        {
            get => (Customer?)GetValue(SelectedCustomerProperty);
            set
            {
                SetValue(SelectedCustomerProperty, value);
                LoadAnimalsForCustomer();
            }
        }

        public static readonly DependencyProperty SelectedCustomerProperty =
            DependencyProperty.Register("SelectedCustomer", typeof(Customer), typeof(SearchForAnimalView), new PropertyMetadata(null));

        private void LoadAnimalsForCustomer()
        {
            searchResults.Items.Clear();
            if (SelectedCustomer == null) return;

            var animals = animalService
                .GetAllAnimals()
                .Where(a => a.OwnerId == SelectedCustomer.Id)
                .ToList();

            foreach (var animal in animals)
            {
                searchResults.Items.Add(new ListBoxItem { Content = animal });
            }
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OnSelect(object sender, RoutedEventArgs e)
        {
            if (searchResults.SelectedItem == null) return;

            Animal = ((ListBoxItem)searchResults.SelectedItem).Content as Animal;
            DialogResult = true;
            Close();
        }
    }
}
