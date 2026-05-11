using VetClinic.App.BusinessLayer;
using VetClinic.App.BusinessLayer.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace VetClinic.App.Views
{
    public partial class SearchForCustomerView : Window
    {
        private readonly CustomerService customerService;

        public SearchForCustomerView(CustomerService customerService)
        {
            InitializeComponent();
            this.customerService = customerService;
        }

        public Customer? Customer { get; set; }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OnSearchChanged(object sender, TextChangedEventArgs e)
        {
            searchResults.Items.Clear();
            var searchText = searchName.Text;

            if (searchText.Length < 3) return;

            var customers = customerService.GetAllCustomers()
                .Where(c => (c.FirstName + " " + c.Surname).ToLower().Contains(searchText.ToLower()))
                .OrderBy(c => c.Surname)
                .ThenBy(c => c.FirstName);

            foreach (var customer in customers)
            {
                searchResults.Items.Add(new ListBoxItem { Content = customer });
            }
        }

        private void OnSelect(object sender, RoutedEventArgs e)
        {
            if (searchResults.SelectedItem == null) return;

            Customer = ((ListBoxItem)searchResults.SelectedItem).Content as Customer;
            DialogResult = true;
            Close();
        }
    }
}
